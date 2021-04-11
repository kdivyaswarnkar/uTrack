using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uTrack.Data.Context;

namespace uTrack.Api.DbConfig
{
    public static class DbConfig
    {     
        public static void AddDatabases(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<IdmDBContext>(options =>
            options.UseSqlServer(config.GetConnectionString("IdmDBConnection"))
            );
        }
    }
}
