using System;
using System.Collections.Generic;

namespace Entity;

public partial class SectionCustomer
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Section Section { get; set; } = null!;
}
