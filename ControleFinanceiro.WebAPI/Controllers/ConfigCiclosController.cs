using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class ConfigCiclosController : ControllerBase
    {
        private readonly IConfigCiclosDAL configCiclosDAL;
        private readonly IPlanosDAL planosDAL;
        private readonly IGravadorLog gravadorLog;
        public ConfigCiclosController(IConfigCiclosDAL configCiclosDAL, IPlanosDAL planosDAL, IGravadorLog gravadorLog)
        {
            this.configCiclosDAL = configCiclosDAL;
            this.planosDAL = planosDAL;
            this.gravadorLog = gravadorLog;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await configCiclosDAL.ValidaUsuario(usuario, id))
                    throw new KeyNotFoundException("ConfigCiclosId não foi encontrado ou você não tem acesso a ele!");

                ConfigCiclos configCiclos = await configCiclosDAL.Find(id);

                return Ok(configCiclos);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - ConfigCiclosId: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ConfigCiclos configCiclos)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (!await planosDAL.ValidaUsuario(usuario, configCiclos.PlanoId))
                    throw new KeyNotFoundException("Plano não foi encontrado ou você não tem acesso a ele!");

                await configCiclosDAL.Create(configCiclos);

                if (Uri.TryCreate("/configciclos/" + configCiclos.Id, UriKind.Relative, out Uri result))
                    return Created(result, configCiclos);

                return Ok(configCiclos);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - PlanoId: {configCiclos.PlanoId}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex, 500);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(ConfigCiclos configCiclos)
        {
            var usuario = User.Identity.Name;
            try
            {
                if (configCiclos.Id == null)
                    throw new KeyNotFoundException("Id obrigatorio para atualização!");

                int id = (int)configCiclos.Id;

                if (!await configCiclosDAL.ValidaUsuario(usuario, (int)configCiclos.Id))
                    throw new KeyNotFoundException("ConfigCiclosId não foi encontrado ou você não tem acesso a ele!");

                await configCiclosDAL.Update(configCiclos);

                return Ok(configCiclos);
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - ConfigCiclosId: {configCiclos.Id}");
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
                if (!await configCiclosDAL.ValidaUsuario(usuario, (int)id))
                    throw new KeyNotFoundException("ConfigCiclos não foi encontrado ou você não tem acesso a ele!");

                await configCiclosDAL.Delete(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                gravadorLog.GravarLogErro(ex, 400, $"Usuário: {usuario} - ConfigCiclosId: {id}");
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