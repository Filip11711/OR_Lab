using Backend.Models;

namespace Backend.Services.Data
{
    public interface IDataService
    {
        Task<List<NatjecanjeDto>> GetNatjecanja();
        Task<List<CsvDto>> GetCsv();
    }
}
