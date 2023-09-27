﻿using Models;

namespace Repository.Contracts
{
    public interface IDeviceDriverRepository
    {
        Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id);
        Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync();
        Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel);
        Task CreateDeviceDriver(DeviceDriverCreateModel createModel);
        Task DeleteDeviceDriver(int Id); //  Xóa hẳn => bị hỏng máy
        Task RemoveDeviceDriver(int Id); //chuyển Zone Id  = null
    }
}
