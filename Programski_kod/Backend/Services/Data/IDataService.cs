using ClassLibrary;

namespace Backend.Services.Data
{
    public interface IDataService
    {
        Task<List<NatjecanjeDto>> GetNatjecanja();
        Task<NatjecanjeDto> GetNatjecanje(int id);
        Task<Tim> GetTim(int id);
        Task<Igrac> GetIgrac(int id);
        Task<List<NatjecanjeDto>> GetFilteredNatjecanja(string searchText, string filterColumn);
        Task<List<CsvDto>> GetCsv();
        Task<List<CsvDto>> GetFilteredCsv(string searchText, string filterColumn);
        Task<NatjecanjeDto> CreateNatjecanje(NatjecanjeDto natjecanje);
        Task<Igrac> CreateIgrac(int id, Igrac igrac);
        Task<Tim> CreateTim(int id, Tim tim);
        Task<NatjecanjeDto> UpdateNatjecanje(NatjecanjeDto natjecanje);
        Task<Igrac> UpdateIgrac(Igrac igrac);
        Task<Tim> UpdateTim(Tim tim);
        Task<NatjecanjeDto> DeleteNatjecanje(int id);
        Task<Igrac> DeleteIgrac(int id);
        Task<Tim> DeleteTim(int id);
    }
}
