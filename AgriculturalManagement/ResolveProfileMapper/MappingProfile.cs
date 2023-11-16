using AutoMapper;
using Common.ResolveProfileMapper;
using Entities;
using Entities.Farm;
using Entities.Image;
using Entities.Module;
using Models;
using Models.Device;
using Models.DeviceTimer;

namespace AgriculturalManagement.ResolveProfileMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Authentication profile
            CreateMap<UserRegisterationModel, UserEntity>();

            // Farms mapper profile
            CreateMap<FarmEntity, FarmDisplayModel>();
            CreateMap<FarmCreateModel, FarmEntity>();
            CreateMap<FarmUpdateModel, FarmEntity>();

            // Zone mapper profile 
            CreateMap<ZoneEntity, ZoneDisplayModel>()
            .ForMember(dest => dest.CountDeviceDriver, opt => opt.MapFrom<DeviceDriverCountResolver>())
            .ForMember(dest => dest.CountInstrumentation, opt => opt.MapFrom<InstrumentationCountResolver>());

            CreateMap<ZoneUpdateModel, ZoneEntity>();
            CreateMap<ZoneCreateModel, ZoneEntity>();

            // Image
            CreateMap<ImageEntity, ImageDisplayModel>().ReverseMap();

            // Value Type
            CreateMap<InstrumentationTypeEntity, InstrumentationTypeDisplayModel>().ReverseMap();
            CreateMap<InstrumentationTypeCreateModel, InstrumentationTypeEntity>();

            // Device Driver profile mapping


            // Timer Device Driver
            CreateMap<TimerDeviceEntity, TimerDeviceDriverDisplayModel>();

            // InstrumentSetThreshold
            CreateMap<ThresholdDeviceEntity, InstrumentSetThresholdDisplayModel>();
            CreateMap<InstrumentSetThresholdUpdateModel, ThresholdDeviceEntity>();
            CreateMap<InstrumentSetThresholdCreateModel, ThresholdDeviceEntity>();

            // User
            CreateMap<UserEntity, ProfileUser>();

            // Module
            CreateMap<ModuleEntity, ModuleDisplayModel>();
            CreateMap<DeviceEntity, DeviceDisplayModel>();
        }
    }
}