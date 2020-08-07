using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Panucci.Models;

namespace Panucci.Repositories.Services
{
    public class SubscriperService : Repository<Subscriper> , ISubscriperService
    {
        public SubscriperService(DB db) : base(db)
        {
        }
    }
}
