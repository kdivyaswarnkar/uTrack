using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uTrack.Business;
using uTrack.Business.Interface;
using uTrack.Data.Protocol;
using uTrack.Data.Repositries;

namespace uTrack.Api
{
    public class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
