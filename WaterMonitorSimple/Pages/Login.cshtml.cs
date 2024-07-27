using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterMonitorSimple.Database;

namespace MyApp.Namespace {
	public class LoginModel : PageModel {
		public void OnGet() { }

		public async Task OnPostAsync(
			[FromServices] WaterMonitorDbContext dbContext
		) {

		}
	}
}
