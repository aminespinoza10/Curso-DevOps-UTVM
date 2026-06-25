var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/api/israel/{numero:int}", (int numero) =>
{
    if (numero <= 0)
    {
        return Results.BadRequest("El número debe ser mayor que cero.");
    }

    var valores = new int[]    {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1};
    var simbolos = new string[]{"M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"};

    var romano = "";

    for (int i = 0; i < valores.Length; i++)
    {
        while (numero >= valores[i])
        {
            romano += simbolos[i];
            numero -= valores[i];
        }
    }

    return Results.Ok(new
    {
        Resultado = romano
    });
});

app.Run();