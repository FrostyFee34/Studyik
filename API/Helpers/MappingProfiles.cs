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
            CreateMap<Material, MaterialToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ReverseMap();
            CreateMap<MaterialToInsertDto, Material>().ReverseMap();
        }
    }
}