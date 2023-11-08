using Microsoft.AspNetCore.Mvc;
using RestAPI.Services;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DespesaController : ControllerBase
{
    private readonly DespesaService _despesaService;

    public DespesaController(DespesaService despesaService)
    {
        _despesaService = despesaService;
    }
}