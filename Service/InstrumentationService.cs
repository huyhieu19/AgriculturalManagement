using AutoMapper;
using Entities;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class InstrumentationService : IInstrumentationService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public InstrumentationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
        public Task<StatusDeviceModel> AsyncStatusInstrumentation(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateInstrumentation(InstrumentationCreateModel createModel)
        {
            try
            {
                var createEntity = mapper.Map<InstrumentationEntity>(createModel);
                repositoryManager.Instrumentation.CreateInstrumentation(createEntity);
                await repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task DeleteInstrumentation(int Id)
        {
            try
            {

                repositoryManager.Instrumentation.DeleteInstrumentation(new InstrumentationEntity() { Id = Id });
                await repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync(int Id)
        {
            try
            {
                return await repositoryManager.Instrumentation.GetInstrumentationByZoneAsync(Id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        public async Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync()
        {
            try
            {
                var instrumentations = await repositoryManager.Instrumentation.GetInstrumentationNotInZoneAsync();
                return mapper.Map<IEnumerable<InstrumentationDisplayModel>>(instrumentations);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task RemoveInstrumentation(int Id)
        {
            try
            {
                await repositoryManager.Instrumentation.RemoveInstrumentation(Id);
                await repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel)
        {
            try
            {
                await repositoryManager.Instrumentation.UpdateInforInstrumentation(updateModel);
                await repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
