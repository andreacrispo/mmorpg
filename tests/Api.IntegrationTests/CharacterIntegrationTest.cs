using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MMORPG.Domain;
using MMORPG.Infrastrutture;
using MMORPG.Domain.Entity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace Api.IntegrationTests;


[TestFixture]
public class CharacterIntegrationTest
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;



    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client?.Dispose();
    }

    [SetUp]
    public void Setup()
    {

        _factory = new CustomWebApplicationFactory<Program>();
        using (var scope = _factory.Services.CreateScope())
        {
            var scoped = scope.ServiceProvider;
            var db = scoped.GetRequiredService<MMORPGDbContext>();

            DbTestSeeding.InitDb(db);
        }
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }




    [Test]
    public async Task
    Ensure_throws_exception_if_character_id_is_invalid()
    {

        var response = await _client.GetAsync("/api/character/-1/position");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }


    [Test]
    public async Task
    Ensure_existing_character_is_found()
    {

        var response = await _client.GetAsync("/api/character/100");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Paladin characterFound = await response.Content.ReadFromJsonAsync<Paladin>();

        Assert.That(characterFound.Id, Is.EqualTo(100));
    }

    [Test]
    public async Task
    Ensure_that_position_is_correct_for_specific_char()
    {

        // Act  
        var response = await _client.GetAsync("/api/character/100/position");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var returnedPosition = await response.Content.ReadFromJsonAsync<Position>();

        var expectedPosition = Position.At(8, 2, 0);
        Assert.That(returnedPosition.X, Is.EqualTo(expectedPosition.X));
        Assert.That(returnedPosition.Y, Is.EqualTo(expectedPosition.Y));
    }


    [Test]
    public async Task
    Ensure_characters_list_is_valid_and_not_empty()
    {
        // Act  
        var response = await _client.GetAsync("/api/character/all");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var jsonResponse = JsonConvert.DeserializeObject<List<Paladin>>(
          await response.Content.ReadAsStringAsync()
        );

        Assert.That(jsonResponse.Count, Is.GreaterThan(0));
    }



    [Test]
    public async Task
    Ensure_that_character_in_range_can_attack_target()
    {
        // Act  
        var attackRequest = await _client.GetAsync("/api/character/1001/attack/1002");
        var characterOneRequest = await _client.GetAsync("/api/character/1001");
        var characterTwoRequest = await _client.GetAsync("/api/character/1002");

        Paladin paladin = JsonConvert.DeserializeObject<Paladin>(await characterOneRequest.Content.ReadAsStringAsync());

        Wizard wizard = JsonConvert.DeserializeObject<Wizard>(await characterTwoRequest.Content.ReadAsStringAsync());

        Assert.That(paladin.Hp, Is.EqualTo(150));
        Assert.That(wizard.Hp, Is.LessThan(120));

    }


    [Test]
    public async Task
    Ensure_that_character_not_in_range_cannot_attack_target()
    {
        // Act  

        var attackRequest = await _client.GetAsync("/api/character/1003/attack/1004");
        var characterOneRequest = await _client.GetAsync("/api/character/1003");
        var characterTwoRequest = await _client.GetAsync("/api/character/1004");

        Paladin paladin = JsonConvert.DeserializeObject<Paladin>(await characterOneRequest.Content.ReadAsStringAsync());
        Wizard wizard = JsonConvert.DeserializeObject<Wizard>(await characterTwoRequest.Content.ReadAsStringAsync());

        Assert.That(paladin.Hp, Is.EqualTo(150));
        Assert.That(wizard.Hp, Is.EqualTo(120));
    }

    [Test]
    public async Task
    Ensure_dead_character_is_respawned()
    {
        // Act  
        var respawnRequest = await _client.GetAsync("/api/character/respawn/2002");
        var respawnedCharacterRequest = await _client.GetAsync("/api/character/2002");

        Wizard wizard = JsonConvert.DeserializeObject<Wizard>(await respawnedCharacterRequest.Content.ReadAsStringAsync());
        Assert.That(wizard.Hp, Is.GreaterThan(0));
    }

}
