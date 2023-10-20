using Microsoft.Extensions.Logging;
using Pri.Drinks.Core.Entities;
using Pri.Drinks.Core.Interfaces.Repositories;
using Pri.Drinks.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDbContext applicationDbContext, ILogger<BaseRepository<Property>> logger) : base(applicationDbContext, logger)
        {
        }
    }
}
