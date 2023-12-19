using AutoMapper;
using Backend.Data;
using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

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
                csvDto.Godina = (int)natjecanje.Godina;
                csvDto.Organizator = natjecanje.Organizator;
                csvDto.Prvak = natjecanje.Prvak;
                csvDto.MjestoFinale = natjecanje.MjestoFinale;
                csvDto.BrojNatjecatelja = (int)natjecanje.BrojNatjecatelja;
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
                csvDto.Godina = (int)natjecanje.Godina;
                csvDto.Organizator = natjecanje.Organizator;
                csvDto.Prvak = natjecanje.Prvak;
                csvDto.MjestoFinale = natjecanje.MjestoFinale;
                csvDto.BrojNatjecatelja = (int)natjecanje.BrojNatjecatelja;
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
                    if (svojstvo.Name == "Id")
                    {
                        continue;
                    }
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
                        if (svojstvo.Name == "Id")
                        {
                            continue;
                        }
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
                        if (svojstvo.Name == "Id")
                        {
                            continue;
                        }
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
                natjecanjeDto.Id = natjecanje.Id;
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
                        Id = m.Id,
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
                        Id = m.Id,
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

        public async Task<NatjecanjeDto> GetNatjecanje(int id)
        {
            var natjecanja = await _context.Natjecanja.Include(n => n.Natjecatelji)
                                                        .ThenInclude(n => n.Igrac)
                                                      .Include(n => n.Natjecatelji)
                                                        .ThenInclude(n => n.Tim)
                                                      .AsNoTracking().ToListAsync();
            NatjecanjeDto natjecanjeDto = null;
            foreach (Natjecanje natjecanje in natjecanja)
            {
                if (natjecanje.Id == id)
                {
                    natjecanjeDto = new NatjecanjeDto();
                    natjecanjeDto.Id = id;
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
                            Id = m.Id,
                            Naziv = m.Tim.Naziv,
                            Drzava = m.Drzava,
                            Osnovan = m.Tim.Osnovan,
                            SpolIgraca = m.Spol,
                            Trener = m.Tim.Trener
                        }).ToList();
                    }
                    else
                    {
                        natjecanjeDto.Igraci = natjecanje.Natjecatelji.Select(m => new ClassLibrary.Igrac
                        {
                            Id = m.Id,
                            Ime = m.Igrac.Ime,
                            Prezime = m.Igrac.Prezime,
                            DatumRodenja = m.Igrac.DatumRodenja,
                            Spol = m.Spol,
                            Drzava = m.Drzava
                        }).ToList();
                    }
                }
            };

            return natjecanjeDto;
        }

        public async Task<ClassLibrary.Tim> GetTim(int id)
        {
            var natjecatelj = await _context.Natjecatelji.FindAsync(id);
            if (natjecatelj == null)
            {
                return null;
            }

            var tim = await _context.Timovi.FindAsync(id);
            if (tim == null)
            {
                return null;
            }

            return new ClassLibrary.Tim()
            {
                Id = id,
                Naziv = tim.Naziv,
                Drzava = natjecatelj.Drzava,
                Osnovan = tim.Osnovan,
                SpolIgraca = natjecatelj.Spol,
                Trener = tim.Trener
            };
        }

        public async Task<ClassLibrary.Igrac> GetIgrac(int id)
        {
            var natjecatelj = await _context.Natjecatelji.FindAsync(id);
            if (natjecatelj == null)
            {
                return null;
            }

            var igrac = await _context.Igraci.FindAsync(id);
            if (igrac == null)
            {
                return null;
            }

            return new ClassLibrary.Igrac()
            {
                Id = id,
                Ime = igrac.Ime,
                Prezime = igrac.Prezime,
                DatumRodenja = igrac.DatumRodenja,
                Spol = natjecatelj.Spol,
                Drzava = natjecatelj.Drzava
            };
        }

        public async Task<NatjecanjeDto> CreateNatjecanje(NatjecanjeDto natjecanje)
        {
            var natjecanjeEntity = new Natjecanje()
            {
                Naziv = natjecanje.Naziv,
                Godina = (int)natjecanje.Godina,
                Organizator = natjecanje.Organizator,
                Prvak = natjecanje.Prvak,
                MjestoFinale = natjecanje.MjestoFinale,
                Sport = natjecanje.Sport
            };
            _context.Natjecanja.Add(natjecanjeEntity);
            await _context.SaveChangesAsync();

            natjecanje.Id = natjecanjeEntity.Id;
            natjecanje.BrojNatjecatelja = 0;
            return natjecanje;
        }

        public async Task<ClassLibrary.Igrac> CreateIgrac(int id, ClassLibrary.Igrac igrac)
        {
            var natjecanje = await _context.Natjecanja.FindAsync(id);
            if (natjecanje == null)
            {
                return null;
            }

            var natjecateljEntity = new Natjecatelj()
            {
                Drzava = igrac.Drzava,
                Spol = igrac.Spol,
                Natjecanjeid = id,
                Natjecanje = natjecanje
            };

            _context.Natjecatelji.Add(natjecateljEntity); 
            await _context.SaveChangesAsync();

            var igracEntity = new Backend.Data.Entities.Igrac()
            {
                Id = natjecateljEntity.Id,
                Ime = igrac.Ime,
                Prezime = igrac.Prezime,
                DatumRodenja = (DateOnly)igrac.DatumRodenja,
                Natjecatelj = natjecateljEntity
            };

            _context.Igraci.Add(igracEntity);
            await _context.SaveChangesAsync();

            natjecateljEntity.Igrac = igracEntity;
            _context.Natjecatelji.Update(natjecateljEntity);
            await _context.SaveChangesAsync();

            igrac.Id = natjecateljEntity.Id;
            return igrac;
        }

        public async Task<ClassLibrary.Tim> CreateTim(int id, ClassLibrary.Tim tim)
        {
            var natjecanje = await _context.Natjecanja.FindAsync(id);
            if (natjecanje == null)
            {
                return null;
            }

            var natjecateljEntity = new Natjecatelj()
            {
                Drzava = tim.Drzava,
                Spol = tim.SpolIgraca,
                Natjecanjeid = id,
                Natjecanje = natjecanje
            };

            _context.Natjecatelji.Add(natjecateljEntity);
            await _context.SaveChangesAsync();

            var timEntity = new Backend.Data.Entities.Tim()
            {
                Id = natjecateljEntity.Id,
                Naziv = tim.Naziv,
                Osnovan = (DateOnly)tim.Osnovan,
                Trener = tim.Trener,
                Natjecatelj = natjecateljEntity
            };

            _context.Timovi.Add(timEntity);
            await _context.SaveChangesAsync();

            natjecateljEntity.Tim = timEntity;
            _context.Natjecatelji.Update(natjecateljEntity);
            await _context.SaveChangesAsync();

            tim.Id = natjecateljEntity.Id;
            return tim;
        }

        public async Task<NatjecanjeDto> UpdateNatjecanje(NatjecanjeDto natjecanje)
        {
            var natjecanjeEntity = await _context.Natjecanja.FindAsync(natjecanje.Id);
            if (natjecanjeEntity == null)
            {
                return null;
            }
            var natjecatelji = await _context.Natjecatelji.Where(n => n.Natjecanjeid == natjecanje.Id).ToListAsync();

            natjecanjeEntity.Naziv = natjecanje.Naziv;
            natjecanjeEntity.Godina = (int)natjecanje.Godina;
            natjecanjeEntity.Organizator = natjecanje.Organizator;
            natjecanjeEntity.Prvak = natjecanje.Prvak;
            natjecanjeEntity.MjestoFinale = natjecanje.MjestoFinale;
            natjecanjeEntity.Sport = natjecanje.Sport;

            _context.Natjecanja.Update(natjecanjeEntity);
            await _context.SaveChangesAsync();

            natjecanje.BrojNatjecatelja = natjecatelji.Count;
            return natjecanje;
        }

        public async Task<ClassLibrary.Igrac> UpdateIgrac(ClassLibrary.Igrac igrac)
        {
            var natjecateljEntity = await _context.Natjecatelji.FindAsync(igrac.Id);
            if (natjecateljEntity == null)
            {
                return null;
            }

            var igracEntitity = await _context.Igraci.FindAsync(igrac.Id);
            if (igracEntitity != null)
            {
                return null;
            }

            natjecateljEntity.Drzava = igrac.Drzava;
            natjecateljEntity.Spol = igrac.Spol;
            igracEntitity.Ime = igrac.Ime;
            igracEntitity.Prezime = igrac.Prezime;
            igracEntitity.DatumRodenja = (DateOnly)igrac.DatumRodenja;

            _context.Natjecatelji.Update(natjecateljEntity);
            _context.Igraci.Update(igracEntitity);
            await _context.SaveChangesAsync();
            return igrac;
        }

        public async Task<ClassLibrary.Tim> UpdateTim(ClassLibrary.Tim tim)
        {
            var natjecateljEntity = await _context.Natjecatelji.FindAsync(tim.Id);
            if (natjecateljEntity == null)
            {
                return null;
            }

            var timEntitity = await _context.Timovi.FindAsync(tim.Id);
            if (timEntitity == null)
            {
                return null;
            }

            natjecateljEntity.Drzava = tim.Drzava;
            natjecateljEntity.Spol = tim.SpolIgraca;
            timEntitity.Naziv = tim.Naziv;
            timEntitity.Osnovan = (DateOnly)tim.Osnovan;
            timEntitity.Trener = tim.Trener;

            _context.Natjecatelji.Update(natjecateljEntity);
            _context.Timovi.Update(timEntitity);
            await _context.SaveChangesAsync();
            return tim;
        }

        public async Task<NatjecanjeDto> DeleteNatjecanje(int id)
        {
            var natjecanjeEntity = await _context.Natjecanja.FindAsync(id);
            if (natjecanjeEntity == null)
            {
                return null;
            }

            var natjecatelji = await _context.Natjecatelji.Include(n => n.Igrac)
                                                    .Include(n => n.Tim)
                                                    .Where(n => n.Natjecanjeid == id)
                                                    .ToListAsync();
            foreach(Natjecatelj natjecatelj in natjecatelji)
            {
                if (natjecatelj.Igrac == null)
                {
                    _context.Timovi.Remove(natjecatelj.Tim);
                }
                else
                {
                    _context.Igraci.Remove(natjecatelj.Igrac);
                }
                _context.Natjecatelji.Remove(natjecatelj);
            }

            _context.Natjecanja.Remove(natjecanjeEntity);
            await _context.SaveChangesAsync();

            return new NatjecanjeDto();
        }

        public async Task<ClassLibrary.Igrac> DeleteIgrac(int id)
        {
            var natjecatelj = await _context.Natjecatelji.FindAsync(id);
            if (natjecatelj == null)
            {
                return null;
            }

            var igrac = await _context.Igraci.FindAsync(id);
            if (igrac == null)
            {
                return null;
            }

            _context.Natjecatelji.Remove(natjecatelj);
            _context.Igraci.Remove(igrac);
            await _context.SaveChangesAsync();

            return new ClassLibrary.Igrac();
        }

        public async Task<ClassLibrary.Tim> DeleteTim(int id)
        {
            var natjecatelj = await _context.Natjecatelji.FindAsync(id);
            if (natjecatelj == null)
            {
                return null;
            }

            var tim = await _context.Timovi.FindAsync(id);
            if (tim == null)
            {
                return null;
            }

            _context.Natjecatelji.Remove(natjecatelj);
            _context.Timovi.Remove(tim);
            await _context.SaveChangesAsync();

            return new ClassLibrary.Tim();
        }
    }
}
