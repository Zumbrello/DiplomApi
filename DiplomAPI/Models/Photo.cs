using System;
using System.Collections.Generic;

namespace DiplomAPI.Models;

public partial class Photo
{
    public int Id { get; set; }

    public string? Namephoto { get; set; }

    public virtual ICollection<Photoofhram> Photoofhrams { get; set; } = new List<Photoofhram>();
}
