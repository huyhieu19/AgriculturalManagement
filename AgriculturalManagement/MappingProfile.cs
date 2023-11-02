using AutoMapper;
using Entities;
using Entities.ESP;
using Models;

namespace AgriculturalManagement
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

            // Value Type
            CreateMap<InstrumentationTypeEntity, InstrumentationTypeDisplayModel>().ReverseMap();
            CreateMap<InstrumentationTypeCreateModel, InstrumentationTypeEntity>();

            CreateMap<DeviceDriverTypeEntity, DeviceDriversTypeDisplayModel>().ReverseMap();
            CreateMap<DeviceDriversTypeCreateModel, DeviceDriverTypeEntity>();

            CreateMap<TypeTreeEntity, TypeTreeDisplayModel>().ReverseMap();
            CreateMap<TypeTreeCreateModel, TypeTreeEntity>();

            // Device Driver profile mapping
            CreateMap<DeviceDriverEntity, DeviceDriverDisplayModel>();
            CreateMap<DeviceDriverUpdateModel, DeviceDriverEntity>();
            CreateMap<DeviceDriverCreateModel, DeviceDriverEntity>();

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
        }
    }
}