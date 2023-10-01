using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using FourwardBackend.Hubs;

namespace FourwardBackend.Pages
{
    public class WelcomeModel : PageModel
    {
        private readonly IHubContext<AttendanceHub> _hubContext;

        public WelcomeModel(IHubContext<AttendanceHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [FromRoute]
        public string Name { get; set; }

        [FromRoute]
        public string Bye { get; set; } = "";

        public string Message { get; set; }
        public bool IsPresent { get; set; }
        public async Task OnGetAsync()
        {
            IsPresent = Bye?.ToLower() != "bye";
            Message = IsPresent ? $"Welcome {Name}" : $"Goodbye {Name}";

            await _hubContext.Clients.All.SendAsync("UpdateAttendance", $"{Name}", IsPresent);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", Message);
        }
    }
}
