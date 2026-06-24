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

    [Fact]
    public async Task GetCuitlahuacEndpoint_ReturnsFirst20EvenNumbers()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/cuitlahuac");

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        var jsonString = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(jsonString);
        var root = document.RootElement;

        Assert.True(root.TryGetProperty("Problem", out var problemProp) || root.TryGetProperty("problem", out problemProp));
        Assert.True(root.TryGetProperty("Description", out var descProp) || root.TryGetProperty("description", out descProp));
        Assert.True(root.TryGetProperty("Result", out var resultProp) || root.TryGetProperty("result", out resultProp));

        Assert.Equal("Números pares", problemProp.GetString());
        Assert.Equal("Primeros 20 números pares", descProp.GetString());
        Assert.Equal(JsonValueKind.Array, resultProp.ValueKind);

        var expected = new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40 };
        Assert.Equal(expected.Length, resultProp.GetArrayLength());

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], resultProp[i].GetInt32());
        }
    }
}
