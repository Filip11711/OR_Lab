using AutoMapper;
using Backend.Data;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using System.Reflection;

namespace Backend.Services.Data
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CsvDto>> GetFilteredCsv(string searchText, string filterColumn)
        {
            var natjecanja = await GetFilteredNatjecanja(searchText, filterColumn);

            var natjecanjaCsv = new List<CsvDto>();
            foreach (var natjecanje in natjecanja)
            {
                var csvDto = new CsvDto();
                csvDto.Naziv = natjecanje.Naziv;
                csvDto.Sport = natjecanje.Sport;
                csvDto.Godina = natjecanje.Godina;
                csvDto.Organizator = natjecanje.Organizator;
                csvDto.Prvak = natjecanje.Prvak;
                csvDto.MjestoFinale = natjecanje.MjestoFinale;
                csvDto.BrojNatjecatelja = natjecanje.BrojNatjecatelja;
                if (natjecanje.Igraci == null)
                {
                    csvDto.ImenaIgraca = "";
                    csvDto.PrezimenaIgraca = "";
                    csvDto.DatumRodjenjaIgraca = "";
                    csvDto.DrzaveNatjecatelja = string.Join("_", natjecanje.Timovi.Select(n => n.Drzava));
                    csvDto.SpolNatjecatelja = string.Join("_", natjecanje.Timovi.Select(n => n.SpolIgraca));
                    csvDto.NaziviTimova = string.Join("_", natjecanje.Timovi.Select(n => n.Naziv));
                    csvDto.Osnovani = string.Join("_", natjecanje.Timovi.Select(n => n.Osnovan));
                    csvDto.TreneriTimova = string.Join("_", natjecanje.Timovi.Select(n => n.Trener));
                }
                else
                {
                    csvDto.ImenaIgraca = string.Join("_", natjecanje.Igraci.Select(n => n.Ime));
                    csvDto.PrezimenaIgraca = string.Join("_", natjecanje.Igraci.Select(n => n.Prezime));
                    csvDto.DatumRodjenjaIgraca = string.Join("_", natjecanje.Igraci.Select(n => n.DatumRodenja));
                    csvDto.DrzaveNatjecatelja = string.Join("_", natjecanje.Igraci.Select(n => n.Drzava));
                    csvDto.SpolNatjecatelja = string.Join("_", natjecanje.Igraci.Select(n => n.Spol));
                    csvDto.NaziviTimova = "";
                    csvDto.Osnovani = "";
                    csvDto.TreneriTimova = "";
                }
                natjecanjaCsv.Add(csvDto);
            }


            return natjecanjaCsv;
        }

        public async Task<List<CsvDto>> GetCsv()
        {
            var natjecanja = await GetNatjecanja();

            var natjecanjaCsv = new List<CsvDto>();
            foreach (var natjecanje in natjecanja)
            {
                var csvDto = new CsvDto();
                csvDto.Naziv = natjecanje.Naziv;
                csvDto.Sport = natjecanje.Sport;
                csvDto.Godina = natjecanje.Godina;
                csvDto.Organizator = natjecanje.Organizator;
                csvDto.Prvak = natjecanje.Prvak;
                csvDto.MjestoFinale = natjecanje.MjestoFinale;
                csvDto.BrojNatjecatelja = natjecanje.BrojNatjecatelja;
                if (natjecanje.Igraci == null)
                {
                    csvDto.ImenaIgraca = "";
                    csvDto.PrezimenaIgraca = "";
                    csvDto.DatumRodjenjaIgraca = "";
                    csvDto.DrzaveNatjecatelja = string.Join("_", natjecanje.Timovi.Select(n => n.Drzava));
                    csvDto.SpolNatjecatelja = string.Join("_", natjecanje.Timovi.Select(n => n.SpolIgraca));
                    csvDto.NaziviTimova = string.Join("_", natjecanje.Timovi.Select(n => n.Naziv));
                    csvDto.Osnovani = string.Join("_", natjecanje.Timovi.Select(n => n.Osnovan));
                    csvDto.TreneriTimova = string.Join("_", natjecanje.Timovi.Select(n => n.Trener));
                } else
                {
                    csvDto.ImenaIgraca = string.Join("_", natjecanje.Igraci.Select(n => n.Ime));
                    csvDto.PrezimenaIgraca = string.Join("_", natjecanje.Igraci.Select(n => n.Prezime));
                    csvDto.DatumRodjenjaIgraca = string.Join("_", natjecanje.Igraci.Select(n => n.DatumRodenja));
                    csvDto.DrzaveNatjecatelja = string.Join("_", natjecanje.Igraci.Select(n => n.Drzava));
                    csvDto.SpolNatjecatelja = string.Join("_", natjecanje.Igraci.Select(n => n.Spol));
                    csvDto.NaziviTimova = "";
                    csvDto.Osnovani = "";
                    csvDto.TreneriTimova = "";
                }
                natjecanjaCsv.Add(csvDto);
            }


            return natjecanjaCsv;
        }

        private static bool GetPropertyValue(NatjecanjeDto natjecanje, string propertyName, string value)
        {
            if (propertyName == "Naziv" || propertyName == "Sport" || propertyName == "Godina" || propertyName == "Organizator" || propertyName == "Prvak" || propertyName == "MjestoFinale" || propertyName == "BrojNatjecatelja")
            {
                var propertyInfo = typeof(NatjecanjeDto).GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    var _object = propertyInfo.GetValue(natjecanje);
                    if (_object == null)
                    {
                        return false;
                    } else
                    {
                        return _object.ToString().Contains(value, StringComparison.OrdinalIgnoreCase);
                    }
                }
                return false;
            } else if (propertyName == "Wildcard")
            {
                var svojstva = typeof(NatjecanjeDto).GetProperties();
                foreach (var svojstvo in svojstva)
                {
                    var vrijednostSvojstva = svojstvo.GetValue(natjecanje);
                    if (vrijednostSvojstva != null && vrijednostSvojstva.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                if (natjecanje.Igraci == null)
                {
                    svojstva = typeof(ClassLibrary.Tim).GetProperties();
                    foreach (var svojstvo in svojstva)
                    {
                        if (natjecanje.Timovi.Any(n => svojstvo.GetValue(n).ToString().Contains(value, StringComparison.OrdinalIgnoreCase)))
                        {
                            return true;
                        }
                    }
                } else
                {
                    svojstva = typeof(ClassLibrary.Igrac).GetProperties();
                    foreach (var svojstvo in svojstva)
                    {
                        if (natjecanje.Igraci.Any(n => svojstvo.GetValue(n).ToString().Contains(value, StringComparison.OrdinalIgnoreCase)))
                        {
                            return true;
                        }
                    }
                }

                return false;
            } else
            {
                if (natjecanje.Igraci == null)
                {
                    if (propertyName == "Spol")
                    {
                        var propertyInfo = typeof(ClassLibrary.Tim).GetProperty("SpolIgraca");
                        if (propertyInfo != null)
                        {
                            return natjecanje.Timovi.Any(n => propertyInfo.GetValue(n).ToString().Contains(value, StringComparison.OrdinalIgnoreCase));
                        }
                        return false;
                    } else if (propertyName == "NazivTim")
                    {
                        var propertyInfo = typeof(ClassLibrary.Tim).GetProperty("Naziv");
                        if (propertyInfo != null)
                        {
                            return natjecanje.Timovi.Any(n => propertyInfo.GetValue(n).ToString().Contains(value, StringComparison.OrdinalIgnoreCase));
                        }
                        return false;
                    } else
                    {
                        var propertyInfo = typeof(ClassLibrary.Tim).GetProperty(propertyName);
                        if (propertyInfo != null)
                        {
                            return natjecanje.Timovi.Any(n => propertyInfo.GetValue(n).ToString().Contains(value, StringComparison.OrdinalIgnoreCase));
                        }
                        return false;
                    }
                } else
                {

                    var propertyInfo = typeof(ClassLibrary.Igrac).GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        return natjecanje.Igraci.Any(n => propertyInfo.GetValue(n).ToString().Contains(value, StringComparison.OrdinalIgnoreCase));
                    }
                    return false;
                }

            }
        }

        public async Task<List<NatjecanjeDto>> GetFilteredNatjecanja(string searchText, string filterColumn)
        {
            var natjecanja = await GetNatjecanja();

            var filteredNatjecanja = natjecanja.Where(n => GetPropertyValue(n, filterColumn, searchText)).ToList();

            return filteredNatjecanja;
        }

        public async Task<List<NatjecanjeDto>> GetNatjecanja()
        {
            var natjecanja = await _context.Natjecanja.Include(n => n.Natjecatelji)
                                                        .ThenInclude(n => n.Igrac)
                                                      .Include(n => n.Natjecatelji)
                                                        .ThenInclude(n => n.Tim)
                                                      .AsNoTracking().ToListAsync();
            var natjecanjaDto = new List<NatjecanjeDto>();
            foreach (Natjecanje natjecanje in natjecanja)
            {
                var natjecanjeDto = new NatjecanjeDto();
                natjecanjeDto.Naziv = natjecanje.Naziv;
                natjecanjeDto.Sport = natjecanje.Sport;
                natjecanjeDto.Godina = natjecanje.Godina;
                natjecanjeDto.Organizator = natjecanje.Organizator;
                natjecanjeDto.Prvak = natjecanje.Prvak;
                natjecanjeDto.MjestoFinale = natjecanje.MjestoFinale;
                natjecanjeDto.BrojNatjecatelja = natjecanje.Natjecatelji.Count();
                if (natjecanje.Natjecatelji.FirstOrDefault()?.Igrac == null)
                {
                    natjecanjeDto.Timovi = natjecanje.Natjecatelji.Select(m => new ClassLibrary.Tim
                    {
                        Naziv = m.Tim.Naziv,
                        Drzava = m.Drzava,
                        Osnovan = m.Tim.Osnovan,
                        SpolIgraca = m.Spol,
                        Trener = m.Tim.Trener
                    }).ToList();
                } else
                {
                    natjecanjeDto.Igraci = natjecanje.Natjecatelji.Select(m => new ClassLibrary.Igrac
                    {
                        Ime = m.Igrac.Ime,
                        Prezime = m.Igrac.Prezime,
                        DatumRodenja = m.Igrac.DatumRodenja,
                        Spol = m.Spol,
                        Drzava = m.Drzava
                    }).ToList();
                }
                natjecanjaDto.Add(natjecanjeDto);
            };

            return natjecanjaDto;
        }
    }
}
