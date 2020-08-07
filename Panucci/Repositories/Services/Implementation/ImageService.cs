using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Panucci.Models;

namespace Panucci.Repositories.Services
{
    public class ImageService : Repository<Image> , IImageService
    {
        public ImageService(DB db) : base(db)
        {
        }
    }
}
