using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;

namespace AudioPlaybackHandler
{
    public class Playback
    {

        private WaveOut _waveOut;

        public Playback()
        {
            _waveOut = new WaveOut();
        }

        public void StartPlayback(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            IWaveProvider provider = new Mp3FileReader(stream);
            _waveOut.Init(provider);
            _waveOut.Play();
        }

        public void SetOutputDevice(int device)
        {
            _waveOut.DeviceNumber = device;
        }

        public List<PlaybackDevice> OutputDevices()
        {
            List<PlaybackDevice> devices = new List<PlaybackDevice>();
            for (int i = -1; i < WaveOut.DeviceCount; i++)
            {
                var cap = WaveOut.GetCapabilities(i);
                devices.Add(new PlaybackDevice { ID = i, Name = cap.ProductName });
            }
            return devices;
        }
    }
}
