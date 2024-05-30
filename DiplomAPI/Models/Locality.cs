using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Locality
{
    public int Id { get; set; }

    public string? Nameoflocality { get; set; }

    public virtual ICollection<Localirytype> Localirytypes { get; set; } = new List<Localirytype>();
}
