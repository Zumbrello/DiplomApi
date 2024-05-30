namespace DiplomAPI.HelperClasses;

public class RegionHramDto
{
    public int Id { get; set; }
    public string Churchname { get; set; }
    public string BuildDate { get; set; }
    public string Description { get; set; }
    
    public int? Idlocate { get; set; }
}