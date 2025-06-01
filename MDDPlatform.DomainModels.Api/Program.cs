using MDDPlatform.DomainModels.Api.Hubs;
using MDDPlatform.DomainModels.Api.Middlewares;
using MDDPlatform.DomainModels.Api.Services;
using MDDPlatform.DomainModels.Infrastructure;
using MDDPlatform.DomainModels.Services.Intefaces;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options=>{
    options.AddPolicy("APIClient",policy=>{
        policy.WithOrigins("http://localhost:6094","https://localhost:7021")
                .AllowAnyHeader()
                .AllowAnyMethod();                
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDomainModelNotificationService,DomainModelNotificationService>();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("APIClient");

app.UseAuthorization();
app.UseEndpoints(endpoint =>{
    endpoint.MapGet("/", () => {
        return "Hello world! MDDPlatform.DomainModels is running";
    } );
});
app.MapControllers();
app.MapHub<DomainModelHub>("/DomainModelHub");

app.Run();
