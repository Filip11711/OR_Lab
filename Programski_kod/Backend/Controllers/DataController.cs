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

        [HttpGet("tim/{id}")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> GetTim(int id)
        {
            try
            {
                var tim = await _dataService.GetTim(id);
                if (tim == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Tim>()
                    {
                        Status = "Not Found",
                        Message = "Ne postoji tim sa traženim id-em",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Ok",
                    Message = "Dohvaćen tim",
                    Data = tim
                });
            } 
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("igrac/{id}")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> GetIgrac(int id)
        {
            try
            {
                var igrac = await _dataService.GetIgrac(id);
                if (igrac == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Igrac>()
                    {
                        Status = "Not Found",
                        Message = "Ne postoji igrać sa traženim id-em",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Ok",
                    Message = "Dohvaćen igrać",
                    Data = igrac
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<NatjecanjeDto>>> GetNatjecanje(int id)
        {
            try
            {
                var natjecanje = await _dataService.GetNatjecanje(id);
                if (natjecanje == null)
                {
                    return StatusCode(404, new ApiResponse<NatjecanjeDto>()
                    {
                        Status = "Not Found",
                        Message = "Ne postoji natjecanje sa traženim id-em",
                        Data = null
                    });
                }
                return Ok(new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Ok",
                    Message = "Dohvaćeno natjecanje",
                    Data = natjecanje
                });
            } 
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
            
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<NatjecanjeDto>>>> GetNatjecanja([FromQuery] string? searchText = null, [FromQuery] string? filterColumn = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(filterColumn))
                {
                    var natjecanja = await _dataService.GetFilteredNatjecanja(searchText, filterColumn);
                    return Ok(new ApiResponse<List<NatjecanjeDto>>()
                    {
                        Status = "Ok",
                        Message = "Dohvaćena sva natjecanja",
                        Data = natjecanja
                    });
                }
                else
                {
                    var natjecanja = await _dataService.GetNatjecanja();
                    return Ok(new ApiResponse<List<NatjecanjeDto>>()
                    {
                        Status = "Ok",
                        Message = "Dohvaćena sva natjecanja",
                        Data = natjecanja
                    });
                }
            } 
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<NatjecanjeDto>>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
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

        [HttpPost]
        public async Task<ActionResult<ApiResponse<NatjecanjeDto>>> CreateNatjecanje([FromBody] NatjecanjeDto natjecanje)
        {
            try
            {
                natjecanje = await _dataService.CreateNatjecanje(natjecanje);
                return StatusCode(201, new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Created",
                    Message = "Kreirano novo natjecanje",
                    Data = natjecanje
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost("tim/{natjecanjeId}")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> CreateTim(int natjecanjeId, [FromBody] ClassLibrary.Tim tim)
        {
            try
            {
                tim = await _dataService.CreateTim(natjecanjeId, tim);
                if (tim == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Tim>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađeno natjecanje za koje se želi dodati tim",
                        Data = null
                    });
                }
                
                return StatusCode(201, new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Created",
                    Message = "Kreiran novi tim",
                    Data = tim
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost("igrac/{natjecanjeId}")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> CreateIgrac(int natjecanjeId, [FromBody] ClassLibrary.Igrac igrac)
        {
            try
            {
                igrac = await _dataService.CreateIgrac(natjecanjeId, igrac);
                if (igrac == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Tim>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađeno natjecanje za koje se želi dodati igrać",
                        Data = null
                    });
                }

                return StatusCode(201, new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Created",
                    Message = "Kreiran novi igrać",
                    Data = igrac
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<NatjecanjeDto>>> UpdateNatjecanje([FromBody] NatjecanjeDto natjecanje)
        {
            try
            {
                natjecanje = await _dataService.UpdateNatjecanje(natjecanje);
                if (natjecanje == null)
                {
                    return StatusCode(404, new ApiResponse<NatjecanjeDto>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađeno treženo natjecanje",
                        Data = null
                    });
                }

                return StatusCode(200, new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Ok",
                    Message = "Uređeno natjecanje",
                    Data = natjecanje
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("tim")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> UpdateTim([FromBody] ClassLibrary.Tim tim)
        {
            try
            {
                tim = await _dataService.UpdateTim(tim);
                if (tim == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Tim>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađen traženi tim",
                        Data = null
                    });
                }

                return StatusCode(200, new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Ok",
                    Message = "Uređen tim",
                    Data = tim
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("igrac")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> UpdateIgrac([FromBody] ClassLibrary.Igrac igrac)
        {
            try
            {
                igrac = await _dataService.UpdateIgrac(igrac);
                if (igrac == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Tim>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađen traženi igrać",
                        Data = null
                    });
                }

                return StatusCode(200, new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Ok",
                    Message = "Uređen igrać",
                    Data = igrac
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<NatjecanjeDto>>> DeleteNatjecanje(int id)
        {
            try
            {
                var natjecanje = await _dataService.DeleteNatjecanje(id);
                if (natjecanje == null)
                {
                    return StatusCode(404, new ApiResponse<NatjecanjeDto>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađeno treženo natjecanje",
                        Data = null
                    });
                }

                return StatusCode(200, new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Ok",
                    Message = "Obrisano natjecanje",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NatjecanjeDto>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("tim/{id}")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> DeleteTim(int id)
        {
            try
            {
                var tim = await _dataService.DeleteTim(id);
                if (tim == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Tim>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađen traženi tim",
                        Data = null
                    });
                }

                return StatusCode(200, new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Ok",
                    Message = "Obrisan tim",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Tim>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("igrac/{id}")]
        public async Task<ActionResult<ApiResponse<ClassLibrary.Tim>>> DeleteIgrac(int id)
        {
            try
            {
                var igrac = await _dataService.DeleteIgrac(id);
                if (igrac == null)
                {
                    return StatusCode(404, new ApiResponse<ClassLibrary.Tim>()
                    {
                        Status = "Not found",
                        Message = "Nije pronađen traženi igrać",
                        Data = null
                    });
                }

                return StatusCode(200, new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Ok",
                    Message = "Obrisan igrać",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ClassLibrary.Igrac>()
                {
                    Status = "Internal Server Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("openapi")]
        public IActionResult DownloadFile()
        {
            try
            {
                string filePath = "C:\\Users\\filip\\Desktop\\Faks\\4. godina\\Zimski\\Otvoreno_racunarstvo\\Github\\openapi.json";

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found");
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                return File(fileBytes, "application/json", "Sportska natjecanja - OpenApi spec.json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}