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
        private readonly IGravadorLog gravadorLog;
        public GastosController(IGastosDAL gastosDAL, IGravadorLog gravadorLog)
        {
            this.gastosDAL = gastosDAL;
            this.gravadorLog = gravadorLog;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                IList<Gasto> gastos = await gastosDAL.GetALL();

                return Ok(gastos);
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
                Gasto gasto = await gastosDAL.Find(id);

                return Ok(gasto);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Gasto gasto)
        {
            try
            {
                await gastosDAL.Create(gasto);

                if (Uri.TryCreate("/gastos/" + gasto.Id, UriKind.Relative, out Uri result))
                    return Created(result, gasto);

                return Ok(gasto);

            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Atualizar(Gasto gasto)
        {
            try
            {
                await gastosDAL.Update(gasto);

                return Ok(gasto);
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
                await gastosDAL.Delete(id);

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