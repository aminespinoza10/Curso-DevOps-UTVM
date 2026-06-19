var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/aminespinoza", () =>
{
    var limit = 1000;
    var sum = Enumerable.Range(1, limit - 1)
        .Where(n => n % 3 == 0 || n % 5 == 0)
        .Sum();

    return new
    {
        Problem = "Project Euler 1",
        Description = "Suma de todos los múltiplos de 3 o 5 por debajo de 1000",
        Result = sum
    };
});

app.MapGet("/api/emiliano", () =>
{
    long factorial = 1;
    for (int i = 1; i <= 10; i++)
    {
        factorial *= i;
    }

    return new
    {
        Problem = "Factorial",
        Description = "Factorial de 10",
        Result = factorial
    };
});

app.Run();