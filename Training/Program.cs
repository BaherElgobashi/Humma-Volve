
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Training.Contexts;

namespace Training
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddApiVersioning(options =>
            {
                // تحديد الإصدار الافتراضي في حال لم يرسله العميل
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                // إرجاع الإصدارات المتاحة في الـ Response Headers (api-supported-versions)
                options.ReportApiVersions = true;

                // تحديد كيفية قراءة الإصدار (من الرابط URL)
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
                .AddApiExplorer(options =>
                {
                    // شكل صيغة الإصدار في الـ URL (مثل 'v'1)
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
