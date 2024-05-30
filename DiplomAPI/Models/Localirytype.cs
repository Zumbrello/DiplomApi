using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Localirytype
{
    public int Id { get; set; }

    public int? Idlocality { get; set; }

    public int? Idtypelocality { get; set; }

    public virtual ICollection<Church> Churches { get; set; } = new List<Church>();

    public virtual Locality? IdlocalityNavigation { get; set; }

    public virtual Typeoflocality? IdtypelocalityNavigation { get; set; }
}
