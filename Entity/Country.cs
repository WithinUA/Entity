using System;
using System.Collections.Generic;

namespace Entity;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Square { get; set; }

    public int? WorldSideId { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Sell> Sells { get; set; } = new List<Sell>();

    public virtual WorldSide? WorldSide { get; set; }
}
