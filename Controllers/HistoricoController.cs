using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvpAPI.Dtos;

namespace mvpAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HistoricoController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HistoricoController> _logger;

    // Construtor com injeção de dependência
    public HistoricoController(ApplicationDbContext context, ILogger<HistoricoController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET api/v1/historico
    [HttpGet("historico/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHistoricos()
    {
        var historicos = await _context.Historicos
            .Select(h => new 
            {
                h.Id,
                h.Usuario,
                h.DataAtual, 
                h.Tipo,
                h.Documento,
                h.Data,
                h.Periodo
            })
            .ToListAsync();
        
        // Usar o logger para imprimir os dados
        foreach (var historico in historicos)
        {
            _logger.LogInformation($"ID: {historico.Id}, Usuario: {historico.Usuario}, DataAtual: {historico.DataAtual}, Tipo: {historico.Tipo}, Documento: {historico.Documento}, Data: {historico.Data}, Periodo: {historico.Periodo}");
        }
        
        return Ok(historicos);
    }
    
    // GET api/v1/historico/{id}
    [HttpGet("historico/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHistoricoById(int id)
    {
        var historico = await _context.Historicos
            .Where(h => h.Id == id)
            .Select(h => new 
            {
                h.Dados
            })
            .FirstOrDefaultAsync();
        
        if (historico == null)
        {
            return NotFound();
        }

        // Usar o logger para imprimir os dados
        _logger.LogInformation($"Dados: {historico.Dados}");
        var obj = JsonSerializer.Deserialize<List<PepsResponse>>(historico.Dados);
        return Ok(obj);
    }
}
