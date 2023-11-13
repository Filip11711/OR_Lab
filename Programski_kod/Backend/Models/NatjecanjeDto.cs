namespace Backend.Models
{
    public class NatjecanjeDto
    {
        public string Naziv { get; set; }
        public string Sport { get; set; }
        public int Godina { get; set; }
        public string Organizator { get; set; }
        public string Prvak { get; set; }
        public string MjestoFinale { get; set; }
        public int BrojNatjecatelja { get; set; }
        public List<Igrac>? Igraci {  get; set; }
        public List<Tim>? Timovi { get; set; }
    }

    public class Tim
    {
        public string Naziv { get; set; }
        public string Drzava { get; set; }
        public DateOnly Osnovan { get; set; }
        public string SpolIgraca { get; set; }
        public string Trener { get; set; }
    }

    public class Igrac
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateOnly DatumRodenja { get; set; }
        public string Spol { get; set; }
        public string Drzava { get; set; }
    }
}
