using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Cloud.TextToSpeech.V1;

namespace T2PHandler
{
    public class SpeechService
    {

        private TextToSpeechClient _client;
        private TextToSpeechClientBuilder _builder;

        // Create Audiosettings class and create it as property here
        public AudioSettings Settings { get; set; }

        public SpeechService(Auth authService)
        {
            Settings = new AudioSettings();
            _builder = new TextToSpeechClientBuilder { CredentialsPath = authService.JsonCredentialPath };
            _client = _builder.Build();
        }

        //public bool Authenticate()
        //{
        //    return 
        //}

        public byte[] GetSpeech(string text)
        {
            SynthesisInput input = new SynthesisInput { Text = text };
            VoiceSelectionParams voiceSelection = new VoiceSelectionParams
            {
                LanguageCode = Settings.LanguageCode,
                SsmlGender = (SsmlVoiceGender)Settings.Gender
            };

            AudioConfig audioConfig = new AudioConfig { 
                AudioEncoding = (AudioEncoding)Settings.Encoding,
                SpeakingRate = Settings.SpeakingRate,
                Pitch = Settings.Pitch,
                VolumeGainDb = Settings.VolumeGain,
            };

            SynthesizeSpeechResponse res = _client.SynthesizeSpeech(input, voiceSelection, audioConfig);
            return res.AudioContent.ToByteArray();
        }

        public static Dictionary<string, string> GetLanguageCodes()
        {
            Dictionary<string, string> languageToCode = new Dictionary<string, string>();
            using (var reader = new StreamReader("LanguageIds.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    languageToCode.Add(line.Split(' ')[1], line.Split(' ')[0]);
                }
            }
            return languageToCode;
        }

        public List<string> GetAccents(string languageCode)
        {
            List<string> accents = new List<string>();
            foreach (var voice in _client.ListVoices(languageCode).Voices)
            {
                foreach (var accent in voice.LanguageCodes)
                {
                    if (!accents.Contains(accent)) {
                        accents.Add(accent);
                    }
                }
            }
            return accents;
        }

        public List<string> GetGenders()
        {
            return new List<string>(Enum.GetNames(typeof(Gender)));
        }
    }
}
