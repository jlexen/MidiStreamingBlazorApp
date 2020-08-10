using System.Threading.Tasks;
using fnix.alpha.Models;
using fnix.alpha.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace fnix.alpha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MidiController : ControllerBase
    {
        private readonly IHubContext<MidiHub, ITypedMidiHubClient> _hubContext;

        public MidiController(IHubContext<MidiHub, ITypedMidiHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("{meetingId}")]
        public async Task<ActionResult> Post([FromRoute] int meetingId, MidiEvent midiEvent)
        {
            await _hubContext.Clients.All.SendNote(meetingId, midiEvent.Note, midiEvent.EventType);

            return Ok();
        }
    }
}