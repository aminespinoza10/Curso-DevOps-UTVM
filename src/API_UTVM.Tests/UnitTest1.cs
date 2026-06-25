namespace API_UTVM.Tests;

using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class AminespinozaEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AminespinozaEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAminespinozaEndpoint_ReturnsCorrectResult()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/aminespinoza");

        Assert.True(response.IsSuccessStatusCode);

        var jsonString = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        using var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        // Properties should be case-insensitive
        Assert.True(root.TryGetProperty("Problem", out var problemProp) || root.TryGetProperty("problem", out problemProp));
        Assert.True(root.TryGetProperty("Description", out var descProp) || root.TryGetProperty("description", out descProp));
        Assert.True(root.TryGetProperty("Result", out var resultProp) || root.TryGetProperty("result", out resultProp));
        
        Assert.Equal("Project Euler 1", problemProp.GetString());
        Assert.Equal("Suma de todos los múltiplos de 3 o 5 por debajo de 1000", descProp.GetString());
        Assert.Equal(233168, resultProp.GetInt32());
    }

    [Fact]
    public async Task GetAminespinozaEndpoint_ReturnsJsonContentType()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/aminespinoza");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }
}

public class CuitlahuacEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CuitlahuacEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetCuitlahuacEndpoint_ReturnsSumResult()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/cuitlahuac?a=5&b=7");

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        var jsonString = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        static JsonElement GetJsonProperty(JsonElement element, string name)
        {
            if (element.TryGetProperty(name, out var property))
            {
                return property;
            }

            var camelCaseName = char.ToLowerInvariant(name[0]) + name.Substring(1);
            if (element.TryGetProperty(camelCaseName, out property))
            {
                return property;
            }

            throw new KeyNotFoundException($"Property '{name}' not found in JSON response.");
        }

        Assert.Equal(12, GetJsonProperty(root, "Sum").GetInt32());
        Assert.True(GetJsonProperty(root, "Verified").GetBoolean());
        var input = GetJsonProperty(root, "Input");
        Assert.Equal(5, GetJsonProperty(input, "A").GetInt32());
        Assert.Equal(7, GetJsonProperty(input, "B").GetInt32());
    }
}

public class KrizamudioEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public KrizamudioEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetKrizamudioEndpoint_ReturnsCorrectResult()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/krizamudio");

        Assert.True(response.IsSuccessStatusCode);

        var jsonString = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        using var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        // Properties should be case-insensitive
        Assert.True(root.TryGetProperty("Problem", out var problemProp) || root.TryGetProperty("problem", out problemProp));
        Assert.True(root.TryGetProperty("Description", out var descProp) || root.TryGetProperty("description", out descProp));
        Assert.True(root.TryGetProperty("Result", out var resultProp) || root.TryGetProperty("result", out resultProp));
        
        Assert.Equal("Suma de primos", problemProp.GetString());
        Assert.Equal("Suma de los primeros 10 números primos", descProp.GetString());
        // Sum of first 10 primes: 2+3+5+7+11+13+17+19+23+29 = 129
        Assert.Equal(129, resultProp.GetInt32());
    }

    [Fact]
    public async Task GetKrizamudioEndpoint_ReturnsJsonContentType()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/krizamudio");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetKrizamudioEndpoint_ReturnsOkStatus()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/krizamudio");

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}
