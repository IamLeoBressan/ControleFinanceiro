using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Logging;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public PlanosController(IPlanosDAL planosDAL, IGravadorLog gravadorLog)
        {
            this.planosDAL = planosDAL;
            this.gravadorLog = gravadorLog;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                IList<Plano> planos = await planosDAL.GetALL();                
                return Ok(planos);
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
                Plano plano = await planosDAL.Find(id);

                return Ok(plano);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Plano plano)
        {
            try
            {
                await planosDAL.Create(plano);

                if (Uri.TryCreate("/planos/" + plano.Id, UriKind.Relative, out Uri result))                
                    return Created(result, plano);

                return Ok(plano);

            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Atualizar(Plano plano)
        {
            try
            {
                await planosDAL.Update(plano);

                return Ok(plano);
            }
            catch(Exception ex)
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
                await planosDAL.Delete(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }

    }
}
