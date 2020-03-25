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
        private readonly IGravadorLog gravadorLog;
        public CiclosController(ICiclosDAL ciclosDAL, IGravadorLog gravadorLog)
        {
            this.gravadorLog = gravadorLog;
            this.ciclosDAL = ciclosDAL;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                IList<Ciclo> ciclos = await ciclosDAL.GetALL();

                return Ok(ciclos);
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
                Ciclo ciclo = await ciclosDAL.Find(id);

                return Ok(ciclo);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Ciclo ciclo)
        {
            try
            {
                await ciclosDAL.Create(ciclo);

                if (Uri.TryCreate("/ciclos/" + ciclo.Id, UriKind.Relative, out Uri result))
                    return Created(result, ciclo);

                return Ok(ciclo);
            }
            catch (Exception ex)
            {
                gravadorLog.GravarLogErro(ex);
                return StatusCode(500);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Atualizar(Ciclo ciclo)
        {
            try
            {
                await ciclosDAL.Update(ciclo);

                return Ok(ciclo);
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
                await ciclosDAL.Delete(id);

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