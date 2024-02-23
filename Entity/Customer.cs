using System;
using System.Collections.Generic;

namespace Entity;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string? Email { get; set; }

    public int CountryId { get; set; }

    public int TownId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<SectionCustomer> SectionCustomers { get; set; } = new List<SectionCustomer>();

    public virtual City Town { get; set; } = null!;
}
