using API.DTO;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class MaterialUrlResolver : IValueResolver<Material, MateriaToReturnlDto, string>
    {
        private readonly IConfiguration _config;

        public MaterialUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Material source, MateriaToReturnlDto destination, string destMember,
            ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Link)) return _config["ApiUrl"] + source.Link;

            return null;
        }
    }
}