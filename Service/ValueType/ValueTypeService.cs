using AutoMapper;
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

        public Task<bool> CreateTypeDeviceDrivers(List<DeviceDriversTypeDisplayModel> model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateTypeInstrumentations(List<InstrumentationTypeDisplayModel> model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateTypeTrees(TypeTreeCreateModel model)
        {
            try
            {
                var entity = mapper.Map<TypeTreeEntity>(model);
                repository.TypeTree.CreateTypeTrees(entity);
                await repository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public Task<bool> DeleteTypeDeviceDrivers(List<int> Ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTypeInstrumentations(List<int> Ids)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteTypeTrees(TypeTreeDisplayModel model)
        {
            try
            {
                var typeTree = mapper.Map<TypeTreeEntity>(model);
                repository.TypeTree.DeleteTypeTrees(typeTree);
                await repository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public Task<List<DeviceDriversTypeDisplayModel>> GetTypeDeviceDrivers()
        {
            throw new NotImplementedException();
        }

        public Task<List<InstrumentationTypeDisplayModel>> GetTypeInstrumentation()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TypeTreeDisplayModel>> GetTypeTrees()
        {
            var typeTrees = await repository.TypeTree.GetTypeTree();
            return mapper.Map<List<TypeTreeDisplayModel>>(typeTrees);
        }

        public Task<bool> UpdateTypeDeviceDriver(DeviceDriversTypeDisplayModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateTypeTree(TypeTreeDisplayModel model)
        {
            try
            {
                var entity = mapper.Map<TypeTreeEntity>(model);
                repository.TypeTree.UpdateTypeTree(entity);
                await repository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }
    }
}
