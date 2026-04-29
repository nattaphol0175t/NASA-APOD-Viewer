using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NasaApodWeb.Models;
using NasaApodWeb.Services;

namespace NasaApodWeb.Pages
{
    public class DetailModel : PageModel
    {
        private readonly NasaService _service;

        public Apod? Item { get; set; }

        public DetailModel(NasaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string? date)
        {
            if (string.IsNullOrEmpty(date))
                return NotFound();
                
            Item = await _service.GetApodByDate(date);

            if (Item == null)
                return NotFound();

            return Page();
        }
    }
}