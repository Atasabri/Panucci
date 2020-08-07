using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Panucci.Models;

namespace Panucci.Repositories.Services
{
    public class DrinkService : Repository<Drink> , IDrinkService
    {
        public DrinkService(DB db) : base(db)
        {
        }
    }
}
