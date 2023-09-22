using AutoMapper;
using Entities;
using Models.Farm;
using Models.Zone;

namespace AgriculturalManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Farm mapper profile
            CreateMap<FarmEntity, FarmDisplayModel>();
            CreateMap<FarmCreateModel, FarmEntity>();
            CreateMap<FarmUpdateModel, FarmEntity>();
            CreateMap<FarmEntity, FarmFilterNameModel>();

            // Zone mapper profile 
            CreateMap<ZoneEntity, ZoneDisplayModel>();
            CreateMap<ZoneUpdateModel, ZoneEntity>();
            CreateMap<ZoneCreateModel, ZoneEntity>();

            //

        }
    }
}
