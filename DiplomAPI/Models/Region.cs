using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Region
{
    public int Id { get; set; }

    public string Nameofregion { get; set; } = null!;

    public string? Regionphoto { get; set; }

    public string? NameofregionEng { get; set; }

    public virtual ICollection<Regionhram> Regionhrams { get; set; } = new List<Regionhram>();
}
