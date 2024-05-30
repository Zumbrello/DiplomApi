using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Photoofhram
{
    public int Id { get; set; }

    public int? Idchurch { get; set; }

    public int? Idphoto { get; set; }

    public virtual Church? IdchurchNavigation { get; set; }

    public virtual Photo? IdphotoNavigation { get; set; }
}
