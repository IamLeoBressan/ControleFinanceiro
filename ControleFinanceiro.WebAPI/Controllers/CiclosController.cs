using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Logging;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CiclosController : ControllerBase
    {
        private readonly ICiclosDAL ciclosDAL;
        private readonly IPlanosDAL planosDAL;
        private readonly IGravadorLog gravadorLog;
        public CiclosController(ICiclosDAL ciclosDAL, IPlanosDAL planosDAL, IGravadorLog gravadorLog)
        {
            this.ciclosDAL = ciclosDAL;
            this.planosDAL = planosDAL;
            this.gravadorLog = gravadorLog;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var usuario = User.Identity.Name;
            try
            {
                IList<Ciclo> ciclos = await ciclosDAL.BuscarCiclosUsuario(usuario);

                return Ok(ciclos);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await ciclosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("Ciclo não foi encontrado ou você não tem acesso a ele!");

                Ciclo ciclo = await ciclosDAL.Find(id);

                return Ok(ciclo);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - CicloId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }            
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Ciclo ciclo)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await planosDAL.ValidaUsuario(usuario, ciclo.PlanoId))
                    throw new KeyNotFoundException("Plano não foi encontrado ou você não tem acesso a ele!");

                await ciclosDAL.Create(ciclo);

                if (Uri.TryCreate("/ciclos/" + ciclo.Id, UriKind.Relative, out Uri result))
                    return Created(result, ciclo);

                return Ok(ciclo);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - PlanoId: {ciclo.PlanoId}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Atualizar(Ciclo ciclo)
        {
            var usuario = User.Identity.Name;
            try
            {               
                if (ciclo.Id == null)
                    throw new KeyNotFoundException("Id obrigatorio para atualização!");

                int id = (int)ciclo.Id;

                if (!await ciclosDAL.ValidaUsuario(usuario, (int)ciclo.Id))
                    throw new KeyNotFoundException("Ciclo não foi encontrado ou você não tem acesso a ele!");

                await ciclosDAL.Update(ciclo);

                return Ok(ciclo);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - CicloId: {ciclo.PlanoId}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await ciclosDAL.ValidaUsuario(usuario, (int)id))
                    throw new KeyNotFoundException("Ciclo não foi encontrado ou você não tem acesso a ele!");

                await ciclosDAL.Delete(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - CicloId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }
    }
}