using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NasaApodWeb.Models;
using NasaApodWeb.Services;

namespace NasaApodWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NasaService _service;

        public List<Apod> ApodList { get; set; } = new();

        [BindProperty]
        public int Year { get; set; }

        [BindProperty]
        public int Month { get; set; }

        public IndexModel(NasaService service)
        {
            _service = service;
        }

        public async Task OnPostAsync()
        {
            var data = await _service.GetApods(Year, Month);

            ApodList = data
                .Where(x => x.media_type == "image")
                .ToList();
        }
    }
}