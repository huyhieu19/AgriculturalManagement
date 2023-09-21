using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FarmService : IFarmService
    {

        public FarmService() { }
        public Task<IFarmService> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
