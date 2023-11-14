namespace ClassLibrary
{
    public class CsvDto
    {
        public string Naziv { get; set; }
        public string Sport { get; set; }
        public int Godina { get; set; }
        public string Organizator { get; set; }
        public string Prvak { get; set; }
        public string MjestoFinale { get; set; }
        public int BrojNatjecatelja { get; set; }
        public string DrzaveNatjecatelja { get; set; }
        public string SpolNatjecatelja { get; set; }
        public string? ImenaIgraca { get; set; }
        public string? PrezimenaIgraca { get; set; }
        public string? DatumRodjenjaIgraca { get; set; }
        public string? NaziviTimova { get; set; }
        public string? Osnovani { get; set; }
        public string? TreneriTimova { get; set; }
    }
}
