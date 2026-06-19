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


app.MapGet("/api/Yolanda", () =>
{
    var oddNumbers = Enumerable.Range(1, 40)
        .Where(n => n % 2 == 1)
        .Take(20)
        .ToArray();

    return new
    {
        Problem = "Números impares",
        Description = "Primeros 20 números impares",
        Result = oddNumbers
    };
});


app.MapGet("/api/romano", (int number) =>
{
    if (number <= 0 || number > 3999)
    {
        return Results.BadRequest(new
        {
            Problem = "Conversión a romano",
            Description = "Convierte un número entero a numeral romano (1-3999)",
            Input = number,
            Error = "El número debe estar entre 1 y 3999"
        });
    }

    return Results.Ok(new
    {
        Problem = "Conversión a romano",
        Description = "Convierte un número entero a numeral romano",
        Number = number,
        Roman = ConvertToRoman(number)
    });
});

static string ConvertToRoman(int value)
{
    var map = new (int Value, string Symbol)[]
    {
        (1000, "M"),
        (900, "CM"),
        (500, "D"),
        (400, "CD"),
        (100, "C"),
        (90, "XC"),
        (50, "L"),
        (40, "XL"),
        (10, "X"),
        (9, "IX"),
        (5, "V"),
        (4, "IV"),
        (1, "I")
    };

    var result = new System.Text.StringBuilder();
    var remaining = value;

    foreach (var (digitValue, symbol) in map)
    {
        while (remaining >= digitValue)
        {
            result.Append(symbol);
            remaining -= digitValue;
        }
    }

    return result.ToString();
}

app.Run();