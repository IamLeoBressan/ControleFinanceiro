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
    public class GastosController : ControllerBase
    {
        private readonly IGastosDAL gastosDAL;
        private readonly ICiclosDAL ciclosDAL;
        private readonly IGravadorLog gravadorLog;
        public GastosController(IGastosDAL gastosDAL, ICiclosDAL ciclosDAL, IGravadorLog gravadorLog)
        {
            this.gastosDAL = gastosDAL;
            this.ciclosDAL = ciclosDAL;
            this.gravadorLog = gravadorLog;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var usuario = User.Identity.Name;
            try
            {
                IList<Gasto> gastos = await gastosDAL.BuscarGastosUsuario(usuario);

                return Ok(gastos);
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
                if (!await gastosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("Gasto não foi encontrado ou você não tem acesso a ele!");

                Gasto gasto = await gastosDAL.Find(id);

                return Ok(gasto);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - GastoId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Gasto gasto)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await ciclosDAL.ValidaUsuario(usuario, gasto.CicloId))
                    throw new KeyNotFoundException("Ciclo não foi encontrado ou você não tem acesso a ele!");

                await gastosDAL.Create(gasto);

                if (Uri.TryCreate("/gastos/" + gasto.Id, UriKind.Relative, out Uri result))
                    return Created(result, gasto);

                return Ok(gasto);

            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - CicloId: {gasto.CicloId}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(Gasto gasto)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (gasto.Id == null)
                    throw new KeyNotFoundException("Id obrigatorio para atualização!");

                int id = (int)gasto.Id;

                if (!await gastosDAL.ValidaUsuario(usuario, (int)gasto.Id))
                    throw new KeyNotFoundException("Gasto não foi encontrado ou você não tem acesso a ele!");

                await gastosDAL.Update(gasto);

                return Ok(gasto);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - GastoId: {gasto.Id}");
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
                if (!await gastosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("Gasto não foi encontrado ou você não tem acesso a ele!");

                await gastosDAL.Delete(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - GastoId: {id}");
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