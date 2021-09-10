using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2PHandler
{
    public class AudioSettings
    {
        /// <summary>
        /// Format of returned speech
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.Mp3;
        /// <summary>
        /// Speaking speed of voice in the range[0.25, 4.0].
        /// </summary>
        public double SpeakingRate { get; set; } = 1;
        /// <summary>
        /// Speaking pitch in the range [-20.0, 20.0]
        /// </summary>
        public double Pitch { get; set; } = 0;
        /// <summary>
        /// Volume gain in the range [-96.0, 16.0]
        /// </summary>
        public double VolumeGain { get; set; } = 0;
        //public string EffectsProfile { get; set; }
        public string LanguageCode { get; set; } = "en-US";
        /// <summary>
        /// Gender of voice
        /// </summary>
        public Gender Gender { get; set; } = Gender.UNSPECIFIED;

        public void SetGender(string gender)
        {
            Gender = (Gender)Enum.Parse(typeof(Gender), gender);
        }

    }
}
