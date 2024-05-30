using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomAPI.Context;
using DiplomAPI.Models;
using DiplomAPI.HelperClasses;

[ApiController]
[Route("api/churches")]
public class ChurchesController : ControllerBase
{
    private readonly PostgresContext _context;
    private readonly IWebHostEnvironment _environment;

    public ChurchesController(PostgresContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // Метод для получения всех храмов по региону
    [HttpGet("region/{regionId}")]
    public async Task<ActionResult<IEnumerable<ChurchDto>>> GetChurchesByRegion(int regionId)
    {
        var churches = await _context.Regionhrams
            .Where(rh => rh.Idregion == regionId)
            .Include(rh => rh.IdchurchNavigation)
            .Select(rh => new ChurchDto
            {
                Id = rh.IdchurchNavigation.Id,
                Churchname = rh.IdchurchNavigation.Churchname,
                BuildDate = rh.IdchurchNavigation.Builddate,
                Description = rh.IdchurchNavigation.Description,
                Idlocate = rh.IdchurchNavigation.Idlocate,
                ChurchnameEng = rh.IdchurchNavigation.ChurchnameEng,
                BuildDateEng = rh.IdchurchNavigation.BuilddateEng,
                DescriptionEng = rh.IdchurchNavigation.DescriptionEng
            })
            .ToListAsync();
        
        return Ok(churches);
    }

    // Метод для получения фотографий храма
    [HttpGet("{churchId}/photos")]
    public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotosByChurch(int churchId)
    {
        var photos = await _context.Photoofhrams
            .Where(ph => ph.Idchurch == churchId)
            .Include(ph => ph.IdphotoNavigation)
            .Select(ph => new PhotoofhramDto
            {
                Id = ph.IdphotoNavigation.Id,
                Namephoto = ph.IdphotoNavigation.Namephoto
            })
            .ToListAsync();

        return Ok(photos);
    }

    // Остальные методы контроллера (без изменений)

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Church>>> GetChurches()
    {
        return await _context.Churches.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChurchDto>> GetChurch(int id)
    {
        var church = await _context.Churches.FindAsync(id);
        if (church == null)
        {
            return NotFound();
        }

        return new ChurchDto
        {
            Id = church.Id,
            Churchname = church.Churchname,
            BuildDate = church.Builddate,
            Description = church.Description,
            Idlocate = church.Idlocate,
            ChurchnameEng = church.ChurchnameEng,
            BuildDateEng = church.BuilddateEng,
            DescriptionEng = church.DescriptionEng
            
        };
    }

    [HttpPost]
    public async Task<ActionResult<ChurchDto>> CreateChurch(ChurchDto churchDto)
    {
        var newChurch = new Church
        {
            Churchname = churchDto.Churchname,
            Builddate = churchDto.BuildDate,
            Description = churchDto.Description,
            Idlocate = churchDto.Idlocate,
            ChurchnameEng = churchDto.ChurchnameEng,
            BuilddateEng = churchDto.BuildDateEng,
            DescriptionEng = churchDto.DescriptionEng
            
        };

        _context.Churches.Add(newChurch);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetChurch), new { id = newChurch.Id }, newChurch);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChurch(int id, ChurchDto churchDto)
    {
        if (churchDto == null)
        {
            return BadRequest("Church data is null");
        }

        var church = await _context.Churches.FindAsync(id);
        if (church == null)
        {
            return NotFound($"No church found with ID {id}");
        }

        church.Churchname = churchDto.Churchname;
        church.Builddate = churchDto.BuildDate;
        church.Description = churchDto.Description;
        church.Idlocate = churchDto.Idlocate;
        church.ChurchnameEng = churchDto.ChurchnameEng;
       church.BuilddateEng = churchDto.BuildDateEng;
        church.DescriptionEng = churchDto.DescriptionEng;

        try
        {
            _context.SaveChanges();
            return NoContent();
        }
        catch (DbUpdateException dbEx)
        {
            return StatusCode(500, $"Database update error: {dbEx.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChurch(int id)
    {
        var church = await _context.Churches.FindAsync(id);
        if (church == null)
        {
            return NotFound();
        }

        _context.Churches.Remove(church);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // --- Операции с регионами ---
    [HttpGet("regions")]
    public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
    {
        return await _context.Regions.ToListAsync();
    }

    [HttpGet("regions/{id}")]
    public async Task<ActionResult<Region>> GetRegion(int id)
    {
        var region = await _context.Regions.FindAsync(id);
        if (region == null)
        {
            return NotFound();
        }
        return region;
    }

    [HttpPut("regions/{id}")]
    public async Task<IActionResult> UpdateRegion(int id, RegionDto regionDto)
    {
        if (regionDto == null)
        {
            return BadRequest("Region data is null");
        }

        var region = await _context.Regions.FindAsync(id);
        if (region == null)
        {
            return NotFound($"No region found with ID {id}");
        }

        region.Nameofregion = regionDto.Nameofregion;
        region.Regionphoto = regionDto.Regionphoto;
        region.NameofregionEng = regionDto.NameofregionEng;

        try
        {
            _context.SaveChanges();
            return NoContent();
        }
        catch (DbUpdateException dbEx)
        {
            return StatusCode(500, $"Database update error: {dbEx.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // --- Операции с фотографиями ---
    [HttpGet("photos")]
    public async Task<ActionResult<IEnumerable<Photo>>> GetAllPhotos()
    {
        return await _context.Photos.ToListAsync();
    }

    [HttpGet("photos/{id}")]
    public async Task<ActionResult<Photo>> GetPhoto(int id)
    {
        var photo = await _context.Photos.FindAsync(id);
        if (photo == null)
        {
            return NotFound();
        }
        return photo;
    }

    [HttpGet("photos/bytes/{imageName}")]
    public IActionResult GetImageBytes(string imageName)
    {
        var imagePath = Path.Combine(_environment.ContentRootPath, "Upload-Files", imageName);
        Console.WriteLine($"Attempting to load image from path: {imagePath}");
        if (!System.IO.File.Exists(imagePath))
        {
            Console.WriteLine($"Image not found: {imagePath}");
            return NotFound();
        }

        var extension = Path.GetExtension(imagePath).ToLowerInvariant();
        var mimeType = extension switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            _ => "application/octet-stream",
        };

        var imageBytes = System.IO.File.ReadAllBytes(imagePath);
        return File(imageBytes, mimeType);
    }
   

    [HttpPost("photos/upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var filePath = Path.Combine(_environment.ContentRootPath, "Upload\\Files", file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { file.FileName });
    }

    [HttpPost("photos/add")]
    public async Task<ActionResult<Photo>> CreatePhoto(Photo newPhoto)
    {
        _context.Photos.Add(newPhoto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPhoto), new { id = newPhoto.Id }, newPhoto);
    }

    [HttpPut("photos/edit/{id}")]
    public async Task<IActionResult> UpdatePhoto(int id, PhotoDto photoDto)
    {
        if (photoDto == null)
        {
            return BadRequest("Photo data is null");
        }

        var photo = await _context.Photos.FindAsync(id);
        if (photo == null)
        {
            return NotFound($"No photo found with ID {id}");
        }

        photo.Namephoto = photoDto.Namephoto;

        _context.Entry(photo).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Photos.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("photos/delete/{id}")]
    public async Task<IActionResult> DeletePhoto(int id)
    {
        var photo = await _context.Photos.FindAsync(id);
        if (photo == null)
        {
            return NotFound();
        }

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
