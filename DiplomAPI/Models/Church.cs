using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Church
{
    public int Id { get; set; }

    public string? Churchname { get; set; }

    public string Builddate { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? Idlocate { get; set; }

    public string? ChurchnameEng { get; set; }

    public string? BuilddateEng { get; set; }

    public string? DescriptionEng { get; set; }

    public virtual Localirytype? IdlocateNavigation { get; set; }

    public virtual ICollection<Photoofhram> Photoofhrams { get; set; } = new List<Photoofhram>();

    public virtual ICollection<Regionhram> Regionhrams { get; set; } = new List<Regionhram>();
}
