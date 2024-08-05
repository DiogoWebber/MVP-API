using System.Net;
using System.Text.Json;
using mvpAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using mvpAPI.Dtos;

namespace mvpAPI.Controllers;
[ApiController]
[Route("api/v1/[controller]")]


public class PepsController : ControllerBase
{
    public readonly IPepsService _PepsService;
    
    public PepsController(IPepsService pepsService)
    {
        _PepsService = pepsService;
    }
    [HttpGet("busca/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuscarPeps([FromQuery] DialogData pesquisa)
    {
        var response = await _PepsService.BuscarPeps(pesquisa.Documento);
            
        if(response.CodigoHttp == HttpStatusCode.OK)
        {
            var itemHistorico = new
            {
                usuario = "Adm",
                dataAtual = DateOnly.FromDateTime(DateTime.Now).ToShortDateString(),
                tipo = pesquisa.Tipo,
                documento = pesquisa.Documento,
                data = pesquisa.Data,
                periodo = pesquisa.Periodo,
                dados = response.DadosRetorno
            };

            Console.WriteLine(JsonSerializer.Serialize(itemHistorico));
            return Ok(response.DadosRetorno);
        }
        else
        {
            return StatusCode((int) response.CodigoHttp, response.ErroRetorno);
        }
    }
}