using System;
using System.Collections.Generic;

namespace Entity;

public partial class Sell
{
    public int Id { get; set; }

    public int GoodId { get; set; }

    public int CountryId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int SellPercent { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Good Good { get; set; } = null!;
}
