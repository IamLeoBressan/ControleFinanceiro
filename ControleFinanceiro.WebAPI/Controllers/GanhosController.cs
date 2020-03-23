using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GanhosController : ControllerBase
    {
        private readonly IGanhosDAL ganhosDAL;
        public GanhosController(IGanhosDAL ganhosDAL)
        {
            this.ganhosDAL = ganhosDAL;
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
                //Gravar Log erro
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
                //Gravar Log erro
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
                //Gravar log
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
                //Gravar log
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
                //Gravar log
                return StatusCode(500);
            }
        }
    }
}