using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Models.ESP;
using Service.Contracts.ESP;

namespace Service.ESP
{
    public class EspBackgroundProcessService : IEspBackgroundProcessService
    {
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;

        public EspBackgroundProcessService(IMapper mapper, DapperContext dapperContext)
        {
            this.mapper = mapper;
            this.dapperContext = dapperContext;
        }

        public async Task<List<EspAndTopicModel>> GetAll()
        {
            var query = EspQuery.GetAll;
            var connection = dapperContext.CreateConnection();
            var entities = await connection.QueryAsync<EspAndTopicModel>(query);
            return mapper.Map<List<EspAndTopicModel>>(entities);
        }
    }
}
