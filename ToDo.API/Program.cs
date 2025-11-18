using ToDo.API;
using ToDo.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAPI();
builder.Services.AddCore();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
