using AutoMapper;
using Backend.Data;
using Backend.Data.Entities;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Data
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CsvDto>> GetCsv()
        {
            var natjecanja = await _context.Natjecanja.Include(n => n.Natjecatelji)
                                                        .ThenInclude(n => n.Igrac)
                                                      .Include(n => n.Natjecatelji)
                                                        .ThenInclude(n => n.Tim)
                                                      .AsNoTracking().ToListAsync();

            var natjecanjaCsv = new List<CsvDto>();
            foreach (Natjecanje natjecanje in natjecanja)
            {
                var csvDto = new CsvDto();
                csvDto.Naziv = natjecanje.Naziv;
                csvDto.Sport = natjecanje.Sport;
                csvDto.Godina = natjecanje.Godina;
                csvDto.Organizator = natjecanje.Organizator;
                csvDto.Prvak = natjecanje.Prvak;
                csvDto.MjestoFinale = natjecanje.MjestoFinale;
                csvDto.BrojNatjecatelja = natjecanje.Natjecatelji.Count();
                csvDto.DrzaveNatjecatelja = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Drzava));
                csvDto.SpolNatjecatelja = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Spol));
                if (natjecanje.Natjecatelji.FirstOrDefault()?.Igrac == null)
                {
                    csvDto.ImenaIgraca = "";
                    csvDto.PrezimenaIgraca = "";
                    csvDto.DatumRodjenjaIgraca = "";
                    csvDto.NaziviTimova = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Tim.Naziv));
                    csvDto.Osnovani = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Tim.Osnovan));
                    csvDto.TreneriTimova = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Tim.Trener));
                } else
                {
                    csvDto.ImenaIgraca = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Igrac.Ime));
                    csvDto.PrezimenaIgraca = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Igrac.Prezime));
                    csvDto.DatumRodjenjaIgraca = string.Join("_", natjecanje.Natjecatelji.Select(n => n.Igrac.DatumRodenja));
                    csvDto.NaziviTimova = "";
                    csvDto.Osnovani = "";
                    csvDto.TreneriTimova = "";
                }
                natjecanjaCsv.Add(csvDto);
            }


            return natjecanjaCsv;
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
                    natjecanjeDto.Timovi = natjecanje.Natjecatelji.Select(m => new Models.Tim
                    {
                        Naziv = m.Tim.Naziv,
                        Drzava = m.Drzava,
                        Osnovan = m.Tim.Osnovan,
                        SpolIgraca = m.Spol,
                        Trener = m.Tim.Trener
                    }).ToList();
                } else
                {
                    natjecanjeDto.Igraci = natjecanje.Natjecatelji.Select(m => new Models.Igrac
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
