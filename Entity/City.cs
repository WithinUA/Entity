using System;
using System.Collections.Generic;

namespace Entity;

public partial class City
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public bool IsCapital { get; set; }

    public int Population { get; set; }

    public string Name { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
