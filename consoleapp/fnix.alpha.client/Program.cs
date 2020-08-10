using fnix.alpha.client.Services;
using System;
using System.Linq;

namespace fnix.alpha.client
{    
    public class Program
    {
        private static MidiDeviceService _midiDeviceService = null;

        static void Main(string[] args)
        {
            var midiEventStreamService = new MidiEventStreamService("https://fnixalphadev.azurewebsites.net/");
            _midiDeviceService = new MidiDeviceService(midiEventStreamService);

            GetMeetingId();
            SelectMidiDevice();

            Console.Clear();
            Console.WriteLine($"You are now streaming to meeting {_midiDeviceService.MeetingId} with device {_midiDeviceService.DeviceName}");

            Console.WriteLine("Press a key to quit...");
            Console.ReadKey();
        }

        private static void GetMeetingId()
        {
            _midiDeviceService.MeetingId = 0;
            while (_midiDeviceService.MeetingId <= 0)
            {
                try
                {                    
                    Console.WriteLine("Please Enter a Meeting Id:");
                    var meetingId = Console.ReadLine();
                    _midiDeviceService.MeetingId = int.Parse(meetingId);
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid Meeting Id.");
                };
            }            
        }

        private static void SelectMidiDevice()
        {
            while (string.IsNullOrWhiteSpace(_midiDeviceService.DeviceName))
            {
                try
                {
                    var devices = _midiDeviceService.GetAvailableMidiDevices().GetAwaiter().GetResult();
                    if(!devices.Any())
                    {
                        throw new Exception("No devices found. Please attempt to reconnect your midi devices.");
                    }

                    // if there is just one device, auto select it
                    if(devices.Count == 1)
                    {
                        _midiDeviceService.SubscribeToMidiDevice(devices.First()).GetAwaiter().GetResult();
                    }
                    else
                    {
                        for (int i = 0; i < devices.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {devices[i]}");
                        }
                        Console.WriteLine("Type number of device you want to stream: ");

                        var deviceChoice = Console.ReadLine();
                        var deviceChoiceInt = int.Parse(deviceChoice);
                        if (deviceChoiceInt < 0 || deviceChoiceInt > devices.Count - 1) throw new Exception("Invalid device choice.");

                        _midiDeviceService.SubscribeToMidiDevice(devices[deviceChoiceInt - 1]).GetAwaiter().GetResult();
                    }

                    break;
                }
                catch(Exception e)
                {
                    
                    Console.WriteLine($"Failed to load midi device(s). Reason: {e.Message}");
                    Console.WriteLine("Press any key to retry...");
                    Console.ReadLine();
                };
            }
        }
    }
}
