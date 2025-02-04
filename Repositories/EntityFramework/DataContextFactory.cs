using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PlooCinema.WebApi.Repositories.EntityFramework
{
    public class DataContextFactory : IDbContextFactory<DataContext>
    {
        private readonly DbContextOptions<DataContext> _options;

        public DataContextFactory(DbContextOptions<DataContext> options)
        {
            _options = options;
        }

        public DataContext CreateDbContext()
        {
            return new DataContext(_options);
        }
    }
}