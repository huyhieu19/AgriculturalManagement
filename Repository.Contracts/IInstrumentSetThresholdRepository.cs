﻿using Entities;

namespace Repository.Contracts
{
    public interface IInstrumentSetThresholdRepository
    {
        Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOff();
        Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOffNotDelete();
        Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOffDelete();
        Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOffByIdDeviceDriver(int Id);
        void DeviceInstrumentOnOffUpdate(DeviceInstrumentThresholdEntity updateModel);
        void DeviceInstrumentOnOffCreate(DeviceInstrumentThresholdEntity model);
        void DeviceInstrumentOnOffDeleteById(int Id);
    }
}
