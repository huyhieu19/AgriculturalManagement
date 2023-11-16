using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class TypeTreeRepository : RepositoryBase<TypeTreeEntity>, ITypeTreeRepository
    {
        private readonly DapperContext dapperContext;
        private readonly FactDbContext factDbContext;

        public TypeTreeRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
            this.factDbContext = factDbContext;
        }

        public void CreateTypeTrees(TypeTreeEntity entity)
        {
            Create(entity);
        }

        public void DeleteTypeTrees(TypeTreeEntity entity)
        {
            Delete(entity);
        }

        public async Task<List<TypeTreeEntity>> GetTypeTree()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public void UpdateTypeTree(TypeTreeEntity entity)
        {
            Update(entity);
        }
    }
}