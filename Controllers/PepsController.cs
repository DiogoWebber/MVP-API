using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using mvpAPI.Dtos;
using mvpAPI.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public class PepsController : ControllerBase
{
    private readonly IPepsService _PepsService;
    private readonly ApplicationDbContext _context;

    public PepsController(IPepsService pepsService, ApplicationDbContext context)
    {
        _PepsService = pepsService;
        _context = context;
    }

    [HttpGet("busca/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuscarPeps([FromQuery] DialogData pesquisa)
    {
        var response = await _PepsService.BuscarPeps(pesquisa.Documento);

        if (response.CodigoHttp == HttpStatusCode.OK)
        {

            var teste = JsonSerializer.Serialize(response.DadosRetorno);
            var itemHistorico = new HistoricoModel
            {
                Usuario = "Adm",
                DataAtual = DateOnly.FromDateTime(DateTime.Now).ToShortDateString(),
                Tipo = pesquisa.Tipo,
                Documento = pesquisa.Documento,
                Data = pesquisa.Data,
                Periodo = pesquisa.Periodo,
                Dados = JsonSerializer.Serialize(response.DadosRetorno)
            };

            var teste2 = JsonSerializer.Deserialize<List<PepsResponse>>(teste);
            
            Console.WriteLine(teste2);

            // Salvar o histórico no banco de dados
            _context.Historicos.Add(itemHistorico);
            await _context.SaveChangesAsync();
            return Ok(response.DadosRetorno);
        }
        else
        {
            return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
        }
        
    }
}