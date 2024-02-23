using System;
using System.Collections.Generic;

namespace Entity;

public partial class WorldSide
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
