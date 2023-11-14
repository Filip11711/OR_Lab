using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Backend.Data.Entities;
using ClassLibrary;
using Backend.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly IDataService _dataService;

        public DataController(ILogger<DataController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NatjecanjeDto>>> GetNatjecanja([FromQuery] string? searchText = null, [FromQuery] string? filterColumn = null)
        {
            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(filterColumn))
            {
                var natjecanja = await _dataService.GetFilteredNatjecanja(searchText, filterColumn);
                return Ok(natjecanja);
            } else
            {
                var natjecanja = await _dataService.GetNatjecanja();
                return Ok(natjecanja);
            }
        }

        [HttpGet("json")]
        public async Task<ActionResult> GetJson([FromQuery] string? searchText = null, [FromQuery] string? filterColumn = null)
        {
            List<NatjecanjeDto> natjecanja;

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(filterColumn))
            {
                natjecanja = await _dataService.GetFilteredNatjecanja(searchText, filterColumn);
            }
            else
            {
                natjecanja = await _dataService.GetNatjecanja();
            }

            var json = JsonConvert.SerializeObject(natjecanja, Formatting.Indented);

            var jsonBytes = Encoding.Unicode.GetBytes(json);
            return File(jsonBytes, "application/json", "sportska_natjecanja.json");
        }

        [HttpGet("csv")]
        public async Task<ActionResult> GetCsv([FromQuery] string? searchText = null, [FromQuery] string? filterColumn = null)
        {
            List<CsvDto> natjecanja;

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(filterColumn))
            {
                natjecanja = await _dataService.GetFilteredCsv(searchText, filterColumn);
            }
            else
            {
                natjecanja = await _dataService.GetCsv();
            }

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Naziv,Sport,Godina,Organizator,Prvak,MjestoFinale,BrojNatjecatelja,DržaveNatjecatelja,SpolNatjecatelja,ImenaIgrača,PrezimenaIgrača,DatumRodjenjaIgrača,NaziviTimova,Osnovani,TreneriTimova");
            foreach (var natjecanje in natjecanja)
            {
                csvBuilder.AppendLine($"{natjecanje.Naziv},{natjecanje.Sport},{natjecanje.Godina},{natjecanje.Organizator}," +
                    $"{natjecanje.Prvak},{natjecanje.MjestoFinale},{natjecanje.BrojNatjecatelja},{natjecanje.DrzaveNatjecatelja},{natjecanje.SpolNatjecatelja}," +
                    $"{natjecanje.ImenaIgraca},{natjecanje.PrezimenaIgraca},{natjecanje.DatumRodjenjaIgraca}," +
                    $"{natjecanje.NaziviTimova},{natjecanje.Osnovani},{natjecanje.TreneriTimova}");
            }

            var csvBytes = Encoding.Unicode.GetBytes(csvBuilder.ToString());

            return File(csvBytes, "text/csv", "sportska_natjecanja.csv");
        }
    }
}