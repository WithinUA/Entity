using System;
using System.Collections.Generic;

namespace Entity;

public partial class Good
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SectionId { get; set; }

    public decimal Price { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual ICollection<Sell> Sells { get; set; } = new List<Sell>();
}
