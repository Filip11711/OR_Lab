using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Data.Entities;

public partial class Tim
{
    public string Naziv { get; set; }

    public DateOnly Osnovan { get; set; }

    public string Trener { get; set; }

    public int Id { get; set; }

    [JsonIgnore]
    public virtual Natjecatelj Natjecatelj { get; set; }
}
