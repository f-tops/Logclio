using Logclio.Api;
using Logclio.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureApi();

builder.ConfigureLogProcessingServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
