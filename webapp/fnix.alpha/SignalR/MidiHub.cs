using fnix.alpha.Enums;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace fnix.alpha.SignalR
{
    public class MidiHub : Hub<ITypedMidiHubClient>
    {
        public async Task SendMessage(int meetingId, int note, EventType eventType)
        {
            await Clients.All.SendNote(meetingId, note, eventType);
        }
    }

    public interface ITypedMidiHubClient
    {
        Task SendNote(int meetingId, int note, EventType eventType);
    }
}
