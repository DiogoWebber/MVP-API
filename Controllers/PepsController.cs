using System.Net;
using mvpAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet("busca/{cpf}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuscarPeps([FromRoute] string cpf)
    {
        var response = await _PepsService.BuscarPeps(cpf);
            
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