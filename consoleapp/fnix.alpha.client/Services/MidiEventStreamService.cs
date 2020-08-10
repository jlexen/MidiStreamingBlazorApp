using fnix.alpha.client.Enums;
using fnix.alpha.client.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fnix.alpha.client.Services
{
    public class MidiEventStreamService : IMidiEventStreamService
    {
        private readonly string _streamURL;
        private readonly HttpClient _httpClient;

        public MidiEventStreamService(string streamURL)
        {
            _streamURL = streamURL;
            _httpClient = new HttpClient();
        }

        public async Task SendNote(int meetingId, int note, EventType eventType)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new MidiEvent() { Note = note, EventType = eventType });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var uri = new Uri($"{_streamURL}/api/Midi/{meetingId}");
                await _httpClient.SendAsync(new HttpRequestMessage()
                {
                    RequestUri = uri,
                    Method = HttpMethod.Post,
                    Content = content
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
