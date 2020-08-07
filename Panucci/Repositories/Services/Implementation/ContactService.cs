using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Panucci.Models;

namespace Panucci.Repositories.Services
{
    public class ContactService : Repository<Contact> , IContactService
    {
        public ContactService(DB db) : base(db)
        {
        }
    }
}
