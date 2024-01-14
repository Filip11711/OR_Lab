using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Frontend.ViewModels;

namespace Frontend.Controllers
{
    public class AccountController : Controller
    {

        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            //await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(new UserProfileViewModel()
            {
                Name = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }

        [Authorize]
        public async Task<IActionResult> Data()
        {
            var firstApiUrl = "https://localhost:7223/api/Data/json";
            var firstApiResponse = await CallApiAndGetFile(firstApiUrl);

            var secondApiUrl = "https://localhost:7223/api/Data/csv";
            var secondApiResponse = await CallApiAndGetFile(secondApiUrl);

            var zipStream = CreateZipStream(firstApiResponse, secondApiResponse);

            return File(zipStream, "application/zip", "Preslike.zip");

        }

        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task<byte[]> CallApiAndGetFile(string apiUrl)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    return null;
                }
            }
        }

        private byte[] CreateZipStream(byte[] jsonFileContents, byte[] csvFileContents)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                using (var archive = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                {
                    var jsonEntry = archive.CreateEntry("sportska_natjecanja.json");
                    using (var jsonEntryStream = jsonEntry.Open())
                    {
                        jsonEntryStream.Write(jsonFileContents, 0, jsonFileContents.Length);
                    }

                    var csvEntry = archive.CreateEntry("sportska_natjecanja.csv");
                    using (var csvEntryStream = csvEntry.Open())
                    {
                        csvEntryStream.Write(csvFileContents, 0, csvFileContents.Length);
                    }
                }

                System.IO.File.WriteAllBytes("C:\\Users\\filip\\Desktop\\Faks\\4. godina\\Zimski\\Otvoreno_racunarstvo\\Github\\sportska_natjecanja.json", jsonFileContents);

                System.IO.File.WriteAllBytes("C:\\Users\\filip\\Desktop\\Faks\\4. godina\\Zimski\\Otvoreno_racunarstvo\\Github\\sportska_natjecanja.csv", csvFileContents);

                return memoryStream.ToArray();
            }
        }
    }
}
