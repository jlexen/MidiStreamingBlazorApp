using fnix.alpha.client.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;

namespace fnix.alpha.client.Services
{
    public class MidiDeviceService
    {
        private readonly IMidiEventStreamService _midiEventStreamService;
        public int MeetingId { get; set; }
        private MidiInPort _inPort;
        public string DeviceName { get => _deviceName; }
        private string _deviceName = string.Empty;

        public MidiDeviceService(IMidiEventStreamService midiEventStreamService)
        {
            _midiEventStreamService = midiEventStreamService;
        }

        public async Task<List<string>> GetAvailableMidiDevices()
        {
            string midiInputQueryString = MidiInPort.GetDeviceSelector();
            var midiInputDevices = await DeviceInformation.FindAllAsync(midiInputQueryString);
            return midiInputDevices.Select(d => d.Name).ToList();
        }

        public async Task SubscribeToMidiDevice(string name)
        {
            string midiInputQueryString = MidiInPort.GetDeviceSelector();
            var midiInputDevices = await DeviceInformation.FindAllAsync(midiInputQueryString);
            var device = midiInputDevices.FirstOrDefault(d => d.Name == name);
            _inPort = await MidiInPort.FromIdAsync(device.Id);
            _inPort.MessageReceived += MidiDeviceService_MessageReceived;

            _deviceName = name;
        }

        public async void MidiDeviceService_MessageReceived(MidiInPort sender, MidiMessageReceivedEventArgs args)
        {            
            var message = args.Message;
            if (message.Type == MidiMessageType.NoteOn)
            {
                var note = (MidiNoteOnMessage)message;
                await _midiEventStreamService.SendNote(MeetingId, note.Note, EventType.NoteOn);
            }
            else if (message.Type == MidiMessageType.NoteOff)
            {
                var note = (MidiNoteOffMessage)message;
                await _midiEventStreamService.SendNote(MeetingId, note.Note, EventType.NoteOff);
            }
        }
    }
}
