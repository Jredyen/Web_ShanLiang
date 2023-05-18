using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
