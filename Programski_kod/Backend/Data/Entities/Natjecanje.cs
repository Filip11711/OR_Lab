using System;
using System.Collections.Generic;

namespace Backend.Data.Entities;

public partial class Natjecanje
{
    public int Id { get; set; }

    public string Naziv { get; set; }

    public int Godina { get; set; }

    public string Organizator { get; set; }

    public string Prvak { get; set; }

    public string? MjestoFinale { get; set; }

    public string Sport { get; set; }

    public virtual ICollection<Natjecatelj> Natjecatelji { get; set; }
}
