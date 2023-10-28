﻿using AutoMapper;
using Entities;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ValueTypeService : IValueTypeService
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public ValueTypeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<bool> CreateTypeDeviceDrivers(DeviceDriversTypeCreateModel model)
        {
            try
            {
                logger.LogInformation($"ValueTypeService - CreateTypeDeviceDrivers - Start");
                var entity = mapper.Map<DeviceDriverTypeEntity>(model);
                repository.DeviceDriverType.CreateTypeDeviceDrivers(entity);
                await repository.SaveAsync();
                logger.LogInformation($"ValueTypeService - CreateTypeDeviceDrivers - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - CreateTypeDeviceDrivers - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> CreateTypeInstrumentations(InstrumentationTypeCreateModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - Create Type Instrumentations - Start");
                var entity = mapper.Map<InstrumentationTypeEntity>(model);
                repository.InstrumentationType.CreateTypeInstrumentations(entity);
                await repository.SaveAsync();
                logger.LogInformation($"Value Type Service - Create Type Instrumentations - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - CreateTypeInstrumentations - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> CreateTypeTrees(TypeTreeCreateModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - CreateTypeTrees - Start");
                var entity = mapper.Map<TypeTreeEntity>(model);
                repository.TypeTree.CreateTypeTrees(entity);
                await repository.SaveAsync();
                logger.LogInformation($"Value Type Service - CreateTypeTrees - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - CreateTypeTrees - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> DeleteTypeDeviceDrivers(DeviceDriversTypeDisplayModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - DeleteTypeDeviceDrivers - Start");
                var entity = mapper.Map<DeviceDriverTypeEntity>(model);
                repository.DeviceDriverType.DeleteTypeDeviceDrivers(entity);
                await repository.SaveAsync();
                logger.LogInformation($"Value Type Service - DeleteTypeDeviceDrivers - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - DeleteTypeDeviceDrivers - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> DeleteTypeInstrumentations(InstrumentationTypeDisplayModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - DeleteTypeInstrumentations - Start");
                var entity = mapper.Map<InstrumentationTypeEntity>(model);
                repository.InstrumentationType.DeleteTypeInstrumentations(entity);
                await repository.SaveAsync();
                logger.LogInformation($"Value Type Service - DeleteTypeInstrumentations - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - DeleteTypeInstrumentations - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> DeleteTypeTrees(TypeTreeDisplayModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - DeleteTypeInstrumentations - Start");
                var typeTree = mapper.Map<TypeTreeEntity>(model);
                repository.TypeTree.DeleteTypeTrees(typeTree);
                logger.LogInformation($"Value Type Service - DeleteTypeInstrumentations - End");
                await repository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - DeleteTypeTrees - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<List<DeviceDriversTypeDisplayModel>> GetTypeDeviceDrivers()
        {
            try
            {
                logger.LogInformation($"Value Type Service - GetTypeDeviceDrivers - Start");
                var result = mapper.Map<List<DeviceDriversTypeDisplayModel>>(await repository.DeviceDriverType.GetTypeDeviceDrivers());
                logger.LogInformation($"Value Type Service - GetTypeDeviceDrivers - End");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - GetTypeDeviceDrivers - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<List<InstrumentationTypeDisplayModel>> GetTypeInstrumentation()
        {
            try
            {
                logger.LogInformation($"Value Type Service - GetTypeInstrumentation - Start");
                var result = mapper.Map<List<InstrumentationTypeDisplayModel>>(await repository.InstrumentationType.GetTypeInstrumentation());
                logger.LogInformation($"Value Type Service - GetTypeInstrumentation - End");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - GetTypeInstrumentation - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<List<TypeTreeDisplayModel>> GetTypeTrees()
        {
            try
            {
                logger.LogInformation($"Value Type Service - GetTypeTrees - Start");
                var typeTrees = await repository.TypeTree.GetTypeTree();
                var result = mapper.Map<List<TypeTreeDisplayModel>>(typeTrees);
                logger.LogInformation($"Value Type Service - GetTypeTrees - End");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - GetTypeTrees - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> UpdateTypeDeviceDriver(DeviceDriversTypeDisplayModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - UpdateTypeDeviceDriver - Start");
                var entity = mapper.Map<DeviceDriverTypeEntity>(model);
                repository.DeviceDriverType.UpdateTypeDeviceDriver(entity);
                await repository.SaveAsync();
                logger.LogInformation($"Value Type Service - UpdateTypeDeviceDriver - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - UpdateTypeDeviceDriver - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - UpdateTypeInstrumentation - Start");
                var entity = mapper.Map<InstrumentationTypeEntity>(model);
                repository.InstrumentationType.UpdateTypeInstrumentation(entity);
                await repository.SaveAsync();
                logger.LogInformation($"Value Type Service - UpdateTypeInstrumentation - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - UpdateTypeDeviceDriver - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> UpdateTypeTree(TypeTreeDisplayModel model)
        {
            try
            {
                logger.LogInformation($"Value Type Service - UpdateTypeTree - Start");
                var entity = mapper.Map<TypeTreeEntity>(model);
                repository.TypeTree.UpdateTypeTree(entity);
                await repository.SaveAsync();
                logger.LogInformation($"Value Type Service - UpdateTypeTree - End");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Value Type Service - UpdateTypeTree - Exeption: {ex.Message}");
                throw new AggregateException(ex.Message);
            }
        }
    }
}
