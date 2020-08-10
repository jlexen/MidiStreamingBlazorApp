using fnix.alpha.client.Enums;
using System.Threading.Tasks;

namespace fnix.alpha.client.Services
{
    public interface IMidiEventStreamService
    {
        Task SendNote(int meetingId, int note, EventType eventType);
    }
}