using System.Text.Json.Serialization;

namespace fnix.alpha.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventType
    {
        NoteOn = 1,
        NoteOff = 2,
    }
}
