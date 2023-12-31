﻿using Docker.Session.Data;
using Microsoft.EntityFrameworkCore;

namespace Docker.Session.API.Setup
{
    public static class PostgreSetup
    {
        private const string CONNECTION_STRING = "DefaultConnection";

        public static void PostgreConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(CONNECTION_STRING)));
        }
    }
}
