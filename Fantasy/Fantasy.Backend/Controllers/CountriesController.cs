using Fantasy.Backend.Data;
using Fantasy.Backend.Helpers;
using Fantasy.Backend.UnitsOfWork.Interfaces;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : GenericController<Country>
{
    private readonly ICountriesUnitOfWork _countriesUnitOfWork;
    private readonly IFileStorage _fileStorage;

    public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork, IFileStorage fileStorage) : base(unitOfWork)
    {
        _countriesUnitOfWork = countriesUnitOfWork;
        _fileStorage = fileStorage;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var s = await _fileStorage.SaveFileAsync(new byte[] { 0, 1, 2, 3 }, ".jpg", "teams");
        await _fileStorage.RemoveFileAsync(s, "teams");

        var response = await _countriesUnitOfWork.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _countriesUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _countriesUnitOfWork.GetComboAsync());
    }
}