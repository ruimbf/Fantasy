using Fantasy.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Fantasy.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesAsync();
        await CheckTeamsAsync();
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            //var countriesSQLScript = File.ReadAllText("Data\\Countries.sql");
            //await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
            _context.Countries.Add(new Country { Name = "Portugal" });
            _context.Countries.Add(new Country { Name = "Espanha" });
            _context.Countries.Add(new Country { Name = "França" });
            _context.Countries.Add(new Country { Name = "Inglaterra" });
            _context.Countries.Add(new Country { Name = "Itália" });

            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckTeamsAsync()
    {
        if (!_context.Teams.Any())
        {
            foreach (var country in _context.Countries)
            {
                //_context.Teams.Add(new Team { Name = country.Name, Country = country! });
                if (country.Name == "Portugal")
                {
                    _context.Teams.Add(new Team { Name = "SL Benfica", Country = country! });
                    _context.Teams.Add(new Team { Name = "Sporting CP", Country = country! });
                    _context.Teams.Add(new Team { Name = "FC Porto", Country = country! });
                    _context.Teams.Add(new Team { Name = "SC Braga", Country = country! });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}