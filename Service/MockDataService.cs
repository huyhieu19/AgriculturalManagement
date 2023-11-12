using AutoMapper;
using Common.Enum;
using Common.GuidHelper;
using Common.TimeHelper;
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
            DateCreated = SetTimeZone.GetDateTimeVN(),
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
            DateCreated = SetTimeZone.GetDateTimeVN(),
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
                    return await _repositoryManager.mockData.MockModule(mockModuleEsp32);
                case ModuleType.ESP8266:
                    return await _repositoryManager.mockData.MockModule(mockModuleEsp8266);
            }
            return false;
        }

        public async Task<bool> MockDataDeviceOnModule(Guid moduleId)
        {
            var typeModule = await _repositoryManager.mockData.GetTypeModule(moduleId);
            switch (typeModule)
            {
                case ModuleType.ESP32:
                    return await _repositoryManager.mockData.MockDevicesOnModule(GetDeviceMock(ModuleType.ESP32, moduleId));
                case ModuleType.ESP8266:
                    return await _repositoryManager.mockData.MockDevicesOnModule(GetDeviceMock(ModuleType.ESP8266, moduleId));
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
                    Gpio = ESP8266GateTransferDataType.D0.ToString(),
                    NameRef = "Thiết bị đo nhiệt độ không khí - 1",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    Name = null,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.DoubleValue,
                    DeviceType = DeviceType.Instrumentation,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "%",
                    Gpio = ESP8266GateTransferDataType.D1.ToString(),
                    NameRef = "Thiết bị đo độ ẩm không khí - 1",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    Name = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.DoubleValue,
                    DeviceType = DeviceType.Instrumentation,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "*C",
                    Gpio = ESP8266GateTransferDataType.D2.ToString(),
                    NameRef = "Thiết bị đo nhiệt độ không khí - 2",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    Name = null,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.DoubleValue,
                    DeviceType = DeviceType.Instrumentation,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "%",
                    Gpio = ESP8266GateTransferDataType.D3.ToString(),
                    NameRef = "Thiết bị đo độ ẩm không khí - 2",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    Name = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.DoubleValue,
                    DeviceType = DeviceType.Instrumentation,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "%",
                    Gpio = ESP8266GateTransferDataType.D4.ToString(),
                    NameRef = "Thiết bị đo độ ẩm đất - 1",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    Name = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.DoubleValue,
                    DeviceType = DeviceType.Instrumentation,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = "%",
                    Gpio = ESP8266GateTransferDataType.D5.ToString(),
                    NameRef = "Thiết bị đo độ ẩm đất - 2",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    Name = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.DoubleValue,
                    DeviceType = DeviceType.Instrumentation,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gpio = ESP8266GateTransferDataType.D6.ToString(),
                    NameRef = "Đóng tắt thiết bị - 1",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    Name = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.TrueOrFalse,
                    DeviceType = DeviceType.DeviceDriver,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gpio = ESP8266GateTransferDataType.D7.ToString(),
                    NameRef = "Đóng tắt thiết bị - 2",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    Name = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.TrueOrFalse,
                    DeviceType = DeviceType.DeviceDriver,
                },
                new DeviceEntity()
                {
                    ModuleId = moduleId,
                    Id = GenerateGuid.GetGuid(),
                    Unit = null,
                    Gpio = ESP8266GateTransferDataType.D8.ToString(),
                    NameRef = "Đóng tắt thiết bị - 3",
                    Topic = GenerateGuid.GetGuid(),
                    DateCreated = null,
                    Description = null,
                    Name = null,
                    IsAction = false,
                    IsAuto = false,
                    IsUsed = false,
                    ZoneId = null,
                    ResponseType = ResponseSensorType.TrueOrFalse,
                    DeviceType = DeviceType.DeviceDriver,
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
            return _repositoryManager.mockData.DeleteMockDataDeviceOnModule(moduleId);
        }
    }
}
