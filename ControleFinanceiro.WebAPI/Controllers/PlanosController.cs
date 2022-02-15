using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Logging;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ControleFinanceiro.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanosDAL planosDAL;
        private readonly IGravadorLog gravadorLog;
        public PlanosController(IPlanosDAL planosDAL, IGravadorLog gravadorLog/*, IInitializeDB initializeDB*/)
        {
            this.planosDAL = planosDAL;
            this.gravadorLog = gravadorLog;
            //initializeDB.Initialize();
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                var usuario = User.Identity.Name;

                IList<Plano> planos = await planosDAL.BuscarPlanosUsuario(usuario);
                return Ok(planos);
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
                Plano plano = await planosDAL.Find(id);

                if (plano == null || plano.Usuario != usuario)
                    throw new KeyNotFoundException("Plano não encontrado");

                return Ok(plano);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - PlanoId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Plano plano)
        {
            var usuario = User.Identity.Name;
            try
            {
                plano.Usuario = usuario;
                await planosDAL.Create(plano);

                if (Uri.TryCreate("/planos/" + plano.Id, UriKind.Relative, out Uri result))
                    return Created(result, plano);

                return Ok(plano);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Atualizar(Plano plano)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (plano.Id == null)
                    throw new KeyNotFoundException("Id obrigatorio para atualização!");

                int id = (int)plano.Id;

                if (!await planosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("Plano não foi encontrado ou você não tem acesso a ele!");

                plano.Usuario = usuario;

                await planosDAL.Update(plano);

                return Ok(plano);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - PlanoId: {plano.Id}");
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
                if (!await planosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("Plano não encontrado");

                await planosDAL.Delete(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - PlanoId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }

        [HttpGet("{planoId}/rendimentos/{meses}")]
        public async Task<IActionResult> GerarRendimentos(int planoId, int meses)
        {
            var usuario = User.Identity.Name;
            try
            {
                Plano plano = await planosDAL.BuscarPlanoCompleto(planoId);

                if (plano == null || plano.Usuario != usuario)
                    throw new KeyNotFoundException("Plano não encontrado");

                List<ResumoFinanceiro> rendimentos = plano.PrevisaoRendimentos(meses);

                return Ok(rendimentos);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - PlanoId: {planoId}");
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
