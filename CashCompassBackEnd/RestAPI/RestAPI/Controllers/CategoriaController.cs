using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Models.DTO;
using RestAPI.Repositories.Interfaces;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    public CategoriaController(IUnitOfWork context, IMapper mapper)
    {
        _uof = context;
        _mapper = mapper;
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> Get([FromRoute] int id)
    {
        try
        {
            var categoria = await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            if (categoria == null)
                return NotFound($"Categoria com id = {id} não encontrada...");

            return Ok(categoriaDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> GetAllCategorias()
    {
        try
        {
            var categorias = _uof.CategoriaRepository.GetAll().OrderBy(on => on.Nome);
            var categoriaDTOs = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

            if (categoriaDTOs == null || !categoriaDTOs.Any())
                return NotFound("Nenhuma categoria encontrada...");

            return Ok(categoriaDTOs);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(CategoriaDTO categoriaDTO)
    {
        try
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            if (categoria is null)
                return BadRequest();

            _uof.CategoriaRepository.Add(categoria);
            await _uof.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDto.CategoriaId }, categoriaDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
}
