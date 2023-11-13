using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Data.Entities;

public partial class Natjecatelj
{
    public int Id { get; set; }

    public string Drzava { get; set; }

    public string Spol { get; set; }

    public int Natjecanjeid { get; set; }
    [JsonIgnore]
    public virtual Natjecanje Natjecanje { get; set; }

    public virtual Igrac? Igrac { get; set; }

    public virtual Tim? Tim { get; set; }
}
