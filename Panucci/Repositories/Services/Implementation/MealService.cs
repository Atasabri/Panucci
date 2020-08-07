using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Panucci.Models;

namespace Panucci.Repositories.Services
{
    public class MealService : Repository<Meal> , IMealService
    {
        public MealService(DB db) : base(db)
        {
        }
    }
}
