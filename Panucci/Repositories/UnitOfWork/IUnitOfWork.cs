using Panucci.Repositories.Email;
using Panucci.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panucci.Repositories.UnitOfWork
{
   public interface IUnitOfWork
    {
        IMealService Meals { get; }
        IDrinkService Drinks { get; }
        IImageService Images { get; }
        IContactService Contacts { get;}
        ISubscriperService Subscripers { get; }

        IEmail Emails { get; }

        void Save();

    }
}
