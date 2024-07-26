using System.Net;
using mvpAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mvpAPI.Controllers;
    [ApiController]
    [Route("api/v1/[controller]")]
public class CepimController : ControllerBase
{
    public readonly ICepimService _CepimService;
    
    public CepimController(ICepimService cepimService)
    {
        _CepimService = cepimService;
    }
    [HttpGet("busca/{cnpj}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuscarCepim([FromRoute] string cnpj)
    {
        var response = await _CepimService.BuscarCepim(cnpj);
            
        if(response.CodigoHttp == HttpStatusCode.OK)
        {
            return Ok(response.DadosRetorno);
        }
        else
        {
            return StatusCode((int) response.CodigoHttp, response.ErroRetorno);
        }
    }
}