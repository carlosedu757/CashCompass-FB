using AutoMapper;
using RestAPI.Models.DTO;

namespace RestAPI.Models.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}
