using AutoMapper;
using Common.ResolveProfileMapper;
using Entities;
using Entities.CommonType;
using Entities.ESP;
using Entities.Farm;
using Entities.Image;
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


            CreateMap<TypeTreeEntity, TypeTreeDisplayModel>().ReverseMap();
            CreateMap<TypeTreeCreateModel, TypeTreeEntity>();

            // Device Driver profile mapping


            // Timer Device Driver
            CreateMap<TimerDeviceDriverDisplayModel, TimerDeviceDriverEntity>().ReverseMap();
            CreateMap<TimerDeviceDriverCreateModel, TimerDeviceDriverEntity>();

            // InstrumentSetThreshold
            CreateMap<DeviceInstrumentThresholdEntity, InstrumentSetThresholdDisplayModel>();
            CreateMap<InstrumentSetThresholdUpdateModel, DeviceInstrumentThresholdEntity>();
            CreateMap<InstrumentSetThresholdCreateModel, DeviceInstrumentThresholdEntity>();

            // User
            CreateMap<UserEntity, ProfileUser>();

            // ESP
            CreateMap<EspEntity, EspDisplayModel>();
            CreateMap<EspCreateModel, EspEntity>();
            CreateMap<DeviceEntity, DeviceDisplayModel>();

            CreateMap<DeviceCreateModel, DeviceEntity>().ForMember(dest => dest.Gpio, opt => opt.MapFrom(src => src.Gpio.ToString()));

        }
    }
}