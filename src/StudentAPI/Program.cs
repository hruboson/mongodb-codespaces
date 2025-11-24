using StudentApi.Endpoints;
using StudentApi.Models;
using StudentApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StudentDatabaseSettings>(
    builder.Configuration.GetSection("StudentDatabase")
);

builder.Services.AddSingleton<StudentService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapStudents();

app.Run();