using AutoMapper;
using Entities;
using Models;

namespace AgriculturalManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Authentication profile
            CreateMap<UserRegisterationModel, UserEntity>();

            // Farm mapper profile
            CreateMap<FarmEntity, FarmDisplayModel>();
            CreateMap<FarmCreateModel, FarmEntity>();
            CreateMap<FarmUpdateModel, FarmEntity>();
            CreateMap<FarmEntity, FarmFilterNameModel>();

            // Zone mapper profile 
            CreateMap<ZoneEntity, ZoneDisplayModel>();
            CreateMap<ZoneUpdateModel, ZoneEntity>();
            CreateMap<ZoneCreateModel, ZoneEntity>();

            // Image
            CreateMap<ImageEntity, ImageDisplayModel>().ReverseMap();

            // Instrumentation
            CreateMap<InstrumentationEntity, InstrumentationDisplayModel>();
            CreateMap<InstrumentationCreateModel, InstrumentationEntity>();


        }
    }
}
