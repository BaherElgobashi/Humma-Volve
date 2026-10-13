using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Training.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// إعداد الـ Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // يولد v1 و v2
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // تعريف المستندين
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo { Title = "Training API - V1", Version = "v1" });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.OpenApiInfo { Title = "Training API - V2", Version = "v2" });

    // فلترة تلقائية: كل Action تذهب للمستند المطابق لإصدارها
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        return apiDesc.GroupName == docName;
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Training API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Training API v2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();