using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Data.Entities;

public partial class Igrac
{
    public string Ime { get; set; }

    public string Prezime { get; set; }

    public DateOnly DatumRodenja { get; set; }

    public int Id { get; set; }

    [JsonIgnore]
    public virtual Natjecatelj Natjecatelj { get; set; }
}
