// Hubs/AttendanceHub.cs
using Microsoft.AspNetCore.SignalR;

namespace FourwardBackend.Hubs;


public class AttendanceHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "Welcome to the Attendance Hub!");
        await base.OnConnectedAsync();
    }

    public async Task MarkAttendance(string userId, bool isPresent)
    {
        await Clients.All.SendAsync("UpdateAttendance", userId, isPresent);
    }
}


