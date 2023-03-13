using LoadBalancer.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoadBalancer, LoadBalancer.Controllers.LoadBalancer>();
builder.Services.AddSingleton<ILoadBalancerStrategy, RoundRubinStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(config => config.AllowAnyOrigin());


app.UseAuthorization();

app.MapControllers();

app.Run();