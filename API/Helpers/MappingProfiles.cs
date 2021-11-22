using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Material, MaterialDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Group, o => o.MapFrom(s => s.Group.Name))
                .ReverseMap();
            CreateMap<MaterialToInsertDto, Material>().ReverseMap();
        }
    }
}