namespace DiplomAPI.HelperClasses;

public class RegionDto
{
    public int Id { get; set; }

    public string Nameofregion { get; set; } = null!;
    public string NameofregionEng { get; set; } = null!;

    public string? Regionphoto { get; set; }
}