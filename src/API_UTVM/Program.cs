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

app.MapGet("/api/krizamudio", () =>
{
    int count = 10;
    int found = 0;
    int num = 2;
    long sum = 0;

    static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0) return false;
        for (int i = 3; i * i <= n; i += 2)
        {
            if (n % i == 0) return false;
        }
        return true;
    }

    while (found < count)
    {
        if (IsPrime(num))
        {
            sum += num;
            found++;
        }
        num++;
    }

    return new
    {
        Problem = "Suma de primos",
        Description = "Suma de los primeros 10 números primos",
        Result = sum
    };
});



app.Run();