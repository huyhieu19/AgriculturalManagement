using AutoMapper;
using Entities;
using Models.Farm;

namespace AgriculturalManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Farm profile
            CreateMap<FarmEntity, FarmDisplayModel>().ReverseMap();
            CreateMap<FarmEntity, FarmCreateModel>().ReverseMap();
            CreateMap<FarmUpdateModel, FarmEntity>().ReverseMap();

        }
    }
}
