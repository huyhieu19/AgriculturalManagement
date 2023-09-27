﻿using AutoMapper;
using Models;
using Models.DeviceDriver;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class DeviceDriverService : IDeviceDriverService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public DeviceDriverService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public Task CreateDeviceDrivern(DeviceDriverCreateModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeviceDriver(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDrivernNotInZoneAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDeviceDriver(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
