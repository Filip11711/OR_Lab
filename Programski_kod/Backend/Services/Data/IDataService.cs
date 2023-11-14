using ClassLibrary;

namespace Backend.Services.Data
{
    public interface IDataService
    {
        Task<List<NatjecanjeDto>> GetNatjecanja();
        Task<List<NatjecanjeDto>> GetFilteredNatjecanja(string searchText, string filterColumn);
        Task<List<CsvDto>> GetCsv();
        Task<List<CsvDto>> GetFilteredCsv(string searchText, string filterColumn);
    }
}
