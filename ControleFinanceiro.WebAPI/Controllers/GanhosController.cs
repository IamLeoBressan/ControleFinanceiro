using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.DAL;
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
    public class GanhosController : ControllerBase
    {
        private readonly IGanhosDAL ganhosDAL;
        private readonly ICiclosDAL ciclosDAL;
        private readonly IGravadorLog gravadorLog;
        public GanhosController(IGanhosDAL ganhosDAL, ICiclosDAL ciclosDAL, IGravadorLog gravadorLog)
        {
            this.ganhosDAL = ganhosDAL;
            this.ciclosDAL = ciclosDAL;
            this.gravadorLog = gravadorLog;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var usuario = User.Identity.Name;
            try
            {
                IList<Ganho> ganhos = await ganhosDAL.BuscarGanhosUsuario(usuario);

                return Ok(ganhos);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await ganhosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("Ganho não foi encontrado ou você não tem acesso a ele!");

                Ganho ganho = await ganhosDAL.Find(id);

                return Ok(ganho);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, $"Usuário: {usuario} - GanhoId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Ganho ganho)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await ciclosDAL.ValidaUsuario(usuario, ganho.CicloId))
                    throw new KeyNotFoundException("Ciclo não foi encontrado ou você não tem acesso a ele!");

                await ganhosDAL.Create(ganho);

                if (Uri.TryCreate("/ganhos/" + ganho.Id, UriKind.Relative, out Uri result))
                    return Created(result, ganho);

                return Ok(ganho);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, $"Usuário: {usuario} - CicloId: {ganho.CicloId}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Atualizar(Ganho ganho)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await ganhosDAL.ValidaUsuario(usuario, (int)ganho.Id))
                    throw new KeyNotFoundException("Ganho não foi encontrado ou você não tem acesso a ele!");

                await ganhosDAL.Update(ganho);

                return Ok(ganho);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, $"Usuário: {usuario} - GanhoId: {ganho.Id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await ganhosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("Ganho não foi encontrado ou você não tem acesso a ele!");

                await ganhosDAL.Delete(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, $"Usuário: {usuario} - GanhoId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
    }
}