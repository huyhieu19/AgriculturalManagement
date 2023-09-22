﻿using Database;
using Repository.Contracts;
using Service.Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFarmRepository> farmRepository;
        private readonly Lazy<IZoneRepository> zoneRepository;
        private readonly FactDbContext factDbContext;

        public RepositoryManager(FactDbContext factDbContext, ILoggerManager logger, DapperContext dapperContext)
        {
            this.factDbContext = factDbContext;
            farmRepository = new Lazy<IFarmRepository>(() => new FarmRepository(factDbContext, logger, dapperContext));
            zoneRepository = new Lazy<IZoneRepository>(() => new ZoneRepository(factDbContext, dapperContext));
        }

        public IFarmRepository FarmRepository => farmRepository.Value;

        public IZoneRepository ZoneRepository => zoneRepository.Value;

        public async Task SaveAsync() => await factDbContext.SaveChangesAsync();
    }
}
