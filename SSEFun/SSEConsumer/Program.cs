using SSEConsumer;
using SSEConsumer.LiveInsects;
using SSEFun.Server;
using SSFun.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<GameStateService>();
builder.Services.AddHostedService<InsectMoverBackGroundService>();

builder.Services.RegisterSSEServices<ImplementedLockStepper, GameStateClient>(s =>
{
    s.IntervalInMilliseconds = 100;
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
