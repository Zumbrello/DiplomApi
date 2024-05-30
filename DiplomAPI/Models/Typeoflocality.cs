using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Typeoflocality
{
    public int Id { get; set; }

    public string Typelocality { get; set; } = null!;

    public virtual ICollection<Localirytype> Localirytypes { get; set; } = new List<Localirytype>();
}
