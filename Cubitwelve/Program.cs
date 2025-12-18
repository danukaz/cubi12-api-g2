using Cubitwelve.Src.Extensions;
using Cubitwelve.Src.Middlewares;

var builder = WebApplication.CreateBuilder(args);
//var localAllowSpecificOrigins = "_localAllowSpecificOrigins";
//var deployedAllowSpecificOrigins = "_deployedAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Cache());
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();
app.UseOutputCache();


// Because it's the first middleware, it will catch all exceptions
app.UseExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");


app.UseAuthentication();
app.UseAuthorization();

// app.UseIsUserEnabled();

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.MapControllers();

// Database Bootstrap
AppSeedService.SeedDatabase(app);

app.Run();