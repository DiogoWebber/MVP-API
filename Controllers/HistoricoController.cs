using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mvpAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HistoricoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HistoricoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET api/v1/historico
    [HttpGet("historico/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHistoricos()
    {
        var historicos = await _context.Historicos.ToListAsync();
        return Ok(historicos);
    }
}