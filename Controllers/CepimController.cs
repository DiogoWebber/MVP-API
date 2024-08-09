using System.Net;
using System.Text.Json;
using IntegraBrasilApi.Util;
using Microsoft.AspNetCore.Mvc;
using mvpAPI.Dtos;
using mvpAPI.Interfaces;

namespace mvpAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CepimController : ControllerBase
    {
        private readonly ICepimService _CepimService;
        private readonly ApplicationDbContext _context;

        public CepimController(ICepimService cepimService, ApplicationDbContext context)
        {
            _CepimService = cepimService;
            _context = context;
        }

        [HttpGet("busca/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarCepim([FromQuery] DialogData pesquisa)
        {
            if (!ValidadeCNPJ.IsValid(pesquisa.Documento))
            {
                return BadRequest("CNPJ inválido.");
            }

            var response = await _CepimService.BuscarCepim(pesquisa.Documento);

            if (response.CodigoHttp == HttpStatusCode.OK)
            {
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
}