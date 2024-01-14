using AutoMapper;
using Common.Enum;
using Common.GuidHelper;
using Entities.Module;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class MockDataService : IMockDataService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ModuleEntity mockModuleEsp32 = new ModuleEntity()
        {
            Id = new Guid(),
            Name = "Module ESP32 CP210x",
            DateCreated = DateTime.UtcNow,
            Note = "This is Module ESP32 CP210x",
            MqttServer = "broker.emqx.io",
            MqttPort = 1883,
            ClientId = "Public",
            UserName = "Server Public",
            Password = "Server Public",
            ModuleType = Common.Enum.ModuleType.ESP32,
            UserId = null
        };
        private readonly ModuleEntity mockModuleEsp8266 = new ModuleEntity()
        {
            Id = new Guid(),
            Name = "Module ESP8266 CP210x",
            DateCreated = DateTime.UtcNow,
            Note = "This is Module ESP8266 CP210x",
            MqttServer = "broker.emqx.io",
            MqttPort = 1883,
            ClientId = "Public",
            UserName = "Server Public",
            Password = "Server Public",
            ModuleType = Common.Enum.ModuleType.ESP8266,
            UserId = null
        };


        public MockDataService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<bool> MockDataModule(ModuleType type)
        {
            switch (type)
            {
                case ModuleType.ESP32:
                    return await _repositoryManager.MockData.MockModule(mockModuleEsp32);
                case ModuleType.ESP8266:
                    return await _repositoryManager.MockData.MockModule(mockModuleEsp8266);
            }
            return false;
        }

        public async Task<bool> MockDataDeviceOnModule(Guid moduleId)
        {
            var typeModule = await _repositoryManager.MockData.GetTypeModule(moduleId);
            switch (typeModule)
            {
                case ModuleType.ESP32:
                    return await _repositoryManager.MockData.MockDevicesOnModule(GetDeviceMock(ModuleType.ESP32, moduleId));
                case ModuleType.ESP8266:
                    return await _repositoryManager.MockData.MockDevicesOnModule(GetDeviceMock(ModuleType.ESP8266, moduleId));
            }
            return false;
        }

        private List<DeviceEntity> GetDeviceMock(ModuleType type, Guid moduleId)
        {
            List<DeviceEntity> mockDeviceOnModuleESP8266 = new List<DeviceEntity>()
            {
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "*C",
                    Gate = ESP8266GateTransferDataType.D1.ToString(),
                    NameRef = FunctionDeviceType.AirTemperature,
                    DateCreated = null,
                    Description = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    Name = "Nhiệt độ không khí",
                    ZoneId = null,
                    DeviceType = DeviceType.R.ToString(),
                    TypeStatis = StatisticType.ValueDouble,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "%",
                    Gate = ESP8266GateTransferDataType.D1.ToString(),
                    NameRef = FunctionDeviceType.AirHumidity,
                    DateCreated = null,
                    Description = null,
                    Name = "Độ ẩm không khí",
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    DeviceType = DeviceType.R.ToString(),
                    TypeStatis = StatisticType.ValueDouble,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "%",
                    Gate = ESP8266GateTransferDataType.D2.ToString(),
                    NameRef = FunctionDeviceType.SoilMoisture,
                    DateCreated = null,
                    Description = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    Name = "Độ ẩm đất",
                    ZoneId = null,
                    DeviceType = DeviceType.R.ToString(),
                    TypeStatis = StatisticType.ValueDouble,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gate = ESP8266GateTransferDataType.D3.ToString(),
                    NameRef = FunctionDeviceType.RainDetection,
                    DateCreated = null,
                    Description = null,
                    Name = "Cảm biến mưa",
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    DeviceType = DeviceType.R.ToString(),
                    TypeStatis = StatisticType.ValueDetect,
                },
                 new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gate = ESP8266GateTransferDataType.D4.ToString(),
                    NameRef = FunctionDeviceType.None,
                    DateCreated = null,
                    Description = null,
                    Name = "Thiết bị đóng/mở",
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    DeviceType = DeviceType.W.ToString(),
                    TypeStatis = StatisticType.ValueOnOff,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gate = ESP8266GateTransferDataType.D5.ToString(),
                    NameRef = FunctionDeviceType.None,
                    DateCreated = null,
                    Description = null,
                    Name = "Thiết bị đóng/mở",
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    DeviceType = DeviceType.W.ToString(),
                    TypeStatis = StatisticType.ValueOnOff,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gate = ESP8266GateTransferDataType.D6.ToString(),
                    NameRef = FunctionDeviceType.None,
                    DateCreated = null,
                    Description = null,
                    Name = "Thiết bị đóng/mở",
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    DeviceType = DeviceType.W.ToString(),
                    TypeStatis = StatisticType.ValueOnOff,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gate = ESP8266GateTransferDataType.D7.ToString(),
                    NameRef = FunctionDeviceType.None,
                    DateCreated = null,
                    Description = null,
                    Name = "Thiết bị đóng/mở",
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    DeviceType = DeviceType.W.ToString(),
                    TypeStatis = StatisticType.ValueOnOff,
                }
            };
            switch (type)
            {
                case ModuleType.ESP8266:
                    return mockDeviceOnModuleESP8266;
                case ModuleType.ESP32:
                    return null;
            }
            throw new ArgumentNullException("Module not found");
        }

        public Task<bool> DeleteMockDataDeviceOnModule(Guid moduleId)
        {
            return _repositoryManager.MockData.DeleteMockDataDeviceOnModule(moduleId);
        }
    }
}
