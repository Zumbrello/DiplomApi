using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Regionhram
{
    public int Id { get; set; }

    public int? Idregion { get; set; }

    public int? Idchurch { get; set; }

    public virtual Church? IdchurchNavigation { get; set; }

    public virtual Region? IdregionNavigation { get; set; }
}
