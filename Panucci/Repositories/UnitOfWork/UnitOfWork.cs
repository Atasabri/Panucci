using Microsoft.Extensions.Configuration;
using Panucci.Models;
using Panucci.Repositories.Email;
using Panucci.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panucci.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DB db;
        private IConfiguration configuration;

        private IMealService meals;
        private IDrinkService drinks;
        private IImageService images;
        private IContactService contacts;
        private ISubscriperService subscripers;

        private IEmail emails;

        public UnitOfWork(DB db,IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        public IMealService Meals { 
               get{ 
                     if(meals==null)
                     {
                          meals = new MealService(db);
                     }
                     return meals;
                  } 
        }
        public IDrinkService Drinks
        {
            get
            {
                if (drinks == null)
                {
                    drinks = new DrinkService(db);
                }
                return drinks;
            }
        }
        public IImageService Images
        {
            get
            {
                if (images == null)
                {
                    images = new ImageService(db);
                }
                return images;
            }
        }
        public IContactService Contacts
        {
            get
            {
                if (contacts == null)
                {
                    contacts = new ContactService(db);
                }
                return contacts;
            }
        }
        public ISubscriperService Subscripers
        {
            get
            {
                if (subscripers == null)
                {
                    subscripers = new SubscriperService(db);
                }
                return subscripers;
            }
        }
        public IEmail Emails
        {
            get
            {
                if (emails == null)
                {
                    emails = new Email.Email(configuration);
                }
                return emails;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}
