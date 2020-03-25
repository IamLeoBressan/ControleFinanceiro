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
        private readonly IGravadorLog gravadorLog;
        public GanhosController(IGanhosDAL ganhosDAL, IGravadorLog gravadorLog)
        {
            this.ganhosDAL = ganhosDAL;
            this.gravadorLog = gravadorLog;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                IList<Ganho> ganhos = await ganhosDAL.GetALL();

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
            try
            {
                Ganho ganho = await ganhosDAL.Find(id);

                return Ok(ganho);
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
            try
            {
                await ganhosDAL.Create(ganho);

                if (Uri.TryCreate("/ciclos/" + ganho.Id, UriKind.Relative, out Uri result))
                    return Created(result, ganho);

                return Ok(ganho);

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
            try
            {
                await ganhosDAL.Update(ganho);

                return Ok(ganho);
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
            try
            {
                await ganhosDAL.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
    }
}