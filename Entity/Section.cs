using System;
using System.Collections.Generic;

namespace Entity;

public partial class Section
{
    public int Id { get; set; }

    public string TypeName { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();

    public virtual ICollection<SectionCustomer> SectionCustomers { get; set; } = new List<SectionCustomer>();
}
