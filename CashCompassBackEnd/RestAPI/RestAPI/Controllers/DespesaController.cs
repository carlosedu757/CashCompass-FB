using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Models.DTO.Request;
using RestAPI.Models.DTO.Response;
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

    [HttpGet]
    [Route("/")]
    public async Task<ActionResult<List<DespesaResponse>>> GetAll()
    {
        try
        {
            var despesas = await _despesaService
            .GetAllAsync();

            var despesasResponse = despesas
                .Select(x => new DespesaResponseDTO(x))
                .ToList();

            return Ok(despesasResponse);
        }

        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("/{id}")]
    public async Task<ActionResult<DespesaResponseDTO>> GetById([FromRoute] int id)
    {
        /*
        * O service já verifica se é nulo então não precisa se preocupar com isso 
        */

        try
        {
            var despesa = await _despesaService.GetById(id);

            return Ok(despesa);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<DespesaResponseDTO> Create([FromBody]DespesaRequestDTO request)
    {
        try
        {
            var despesa = await _despesaService.Create(request);

            var response = new DespesaResponseDTO(despesa);
        }
    }
}