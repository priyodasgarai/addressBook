using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using addressBook.Data;
using addressBook.Interfaces;
using addressBook.Interfaces.Logging;
using addressBook.Middlewares;
using addressBook.Models.Identity;
using addressBook.Repository;
using addressBook.Service;
using addressBook.Service.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace addressBook.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration Configuration)
        {

            //For Database
            string connectionString = "DefaultConnection";
            services.AddDbContext<ApplicationDBContext>(Options =>
                        Options.UseSqlServer(Configuration.GetConnectionString(connectionString),
                        sqlOptions =>
                        {
                            //Ensure this is the correct assemble
                            sqlOptions.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName);
                            sqlOptions.EnableRetryOnFailure(); //Enaple automatic retries for transient failures
                        })
                        // .UseExceptionProcessor()
                        , ServiceLifetime.Scoped);

             services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));
            /// services.AddScoped<IFileService, FileService>();
            // services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            // services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            return services;
        }

        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration Configuration)
        {
            // services.AddScoped<ITokenManagement, TokenManagement>();
           //  services.AddScoped<IUserManagement, UserManagement>();

            //For Identity 
            services.AddIdentityCore<AppUser>(options =>
             {
                 options.SignIn.RequireConfirmedEmail = true;
                 options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireNonAlphanumeric = true;
                 options.Password.RequiredLength = 6;
                 options.Password.RequiredUniqueChars = 1;
             }).AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDBContext>();


            //Adding Authentication
            services.AddAuthentication(options =>
                   {
                       options.DefaultAuthenticateScheme =
                       options.DefaultChallengeScheme =
                        options.DefaultForbidScheme =
                       options.DefaultScheme =
                        options.DefaultSignInScheme =
                        options.DefaultSignOutScheme =
                       JwtBearerDefaults.AuthenticationScheme;
                   })//Adding JWT Bearer
            .AddJwtBearer(Options =>
                {
                    Options.SaveToken = true;
                    Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (
                            Encoding.UTF8.GetBytes(Configuration["JWT:Signingkey"])
                        ),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ITokenService, TokenService>();

            /*  services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();*/
            return services;
        }
    
     public static IApplicationBuilder UseInfrastructructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    
    }
}