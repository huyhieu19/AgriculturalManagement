using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IRepositoryBase> repositoryBase;

        public RepositoryManager()
        {
            repositoryBase = new Lazy<IRepositoryBase>(() => new RepositoryBase());
        }

        public IRepositoryBase RepositoryBase => repositoryBase.Value;
    }
}
