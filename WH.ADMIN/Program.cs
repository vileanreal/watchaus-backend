using DBHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Net.Mime;
using System.Text;
using Utilities;
using Utilities.SeriLog;
using WH.ADMIN.Helper;

namespace WH.ADMIN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new ValidationFailedResult(context.ModelState);
                    // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });


            // SeriLog
            builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(builder.Configuration)
            .Enrich.WithCaller()
            );


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // database config
            var db_host = builder.Configuration["Database:DB_HOST"];
            var db_port = builder.Configuration["Database:DB_PORT"];
            var db_name = builder.Configuration["Database:DB_NAME"];
            var db_user = builder.Configuration["Database:DB_USER"];
            var db_password = builder.Configuration["Database:DB_PASS"];
            var conn = "Server=" + db_host + ";Port=" + db_port + ";Database=" + db_name + ";Uid=" + db_user + ";Pwd=" + db_password + ";Convert Zero Datetime=True";
            BaseManager.CONNECTION_STRING = conn;

            // return response in camelcase
            builder.Services
            .AddMvc()
            .AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            });

            TokenHelper.Issuer = builder.Configuration["Jwt:Issuer"];
            TokenHelper.Audience = builder.Configuration["Jwt:Audience"];
            TokenHelper.Key = builder.Configuration["Jwt:Key"];



            var validateLifeTime = true;
            #if DEBUG
                validateLifeTime = false;
            #endif

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = TokenHelper.Issuer,
                    ValidAudience = TokenHelper.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(TokenHelper.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = validateLifeTime,
                    ValidateIssuerSigningKey = true
                };
            });




            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}