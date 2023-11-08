using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.DTO.Response;
using RestAPI.Services;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceitaController : ControllerBase
{
    private readonly ReceitaService _receitaService;

    public ReceitaController(ReceitaService receitaService)
    {
        _receitaService = receitaService;
    }

    public async Task<ActionResult<List<ReceitaResponseDTO>>> GetAllReceitas()
    {
        
    }
}