using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using T2PHandler;
using AudioPlaybackHandler;
using TextToSpeech.ViewModels.Commands;

namespace TextToSpeech.ViewModels
{
    public class T2PViewModel : INotifyPropertyChanged
    {

        private Auth _auth;
        private SpeechService _t2p;
        private Playback _playback;

        public ICommand SpeechCommand { get; set; }
        public ICommand AuthCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, string> _speechLanguageCodes;
        public Dictionary<string, string> SpeechLanguageCodes
        {
            get
            {
                return _speechLanguageCodes;
            }
            set
            {
                _speechLanguageCodes = value;
                NotifyPropertyChanged();
            }
        }

        private string _authStatus;
        public string AuthStatus
        {
            get
            {
                return _authStatus;
            }
            set
            {
                _authStatus = value;
                NotifyPropertyChanged();
            }
        }

        private string _speechText;
        public string SpeechText
        {
            get
            {
                return _speechText;
            }
            set
            {
                _speechText = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> _accents;
        public ObservableCollection<string> Accents
        {
            get
            {
                return _accents;
            }
            set
            {
                _accents = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedAccent;
        public string SelectedAccent
        {
            get
            {
                return _selectedAccent;
            }
            set
            {
                _selectedAccent = value;
                NotifyPropertyChanged();
            }
        }

        private KeyValuePair<string, string> _selectedLanguage;
        public KeyValuePair<string, string> SelectedLanguage
        {
            get
            {
                return _selectedLanguage;
            }
            set
            {
                _selectedLanguage = value;
                ChangedLanguage();
            }
        }

        private double _selectedSpeakingRate = 1;
        public double SelectedSpeakingRate
        {
            get
            {
                return _selectedSpeakingRate;
            }
            set
            {
                _selectedSpeakingRate = value;
                NotifyPropertyChanged();
            }
        }

        private double _selectedPitch = 0;
        public double SelectedPitch
        {
            get
            {
                return _selectedPitch;
            }
            set
            {
                _selectedPitch = value;
                NotifyPropertyChanged();
            }
        }

        private double _selectedVolumeGain = 0;
        public double SelectedVolumeGain
        {
            get
            {
                return _selectedVolumeGain;
            }
            set
            {
                _selectedVolumeGain = value;
                NotifyPropertyChanged();
            }
        }

        public List<PlaybackDevice> PlaybackDevices { get; set; }

        private PlaybackDevice _selectedPlaybackDevice;
        public PlaybackDevice SelectedPlaybackDevice
        {
            get
            {
                return _selectedPlaybackDevice;
            }
            set
            {
                _selectedPlaybackDevice = value;
                NotifyPropertyChanged();
            }
        }

        private string _credentialsPath;
        public string CredentialsPath
        {
            get
            {
                return _credentialsPath;
            }
            set
            {
                _credentialsPath = value;
                Properties.Settings.Default.CredentialsPath = value;
                NotifyPropertyChanged();
            }
        }

        public List<string> Genders { get; set; }
        private string _selectedGender;
        public string SelectedGender
        {
            get
            {
                return _selectedGender;
            }
            set
            {
                _selectedGender = value;
                NotifyPropertyChanged();
            }
        }

        public T2PViewModel()
        {
            _credentialsPath = Properties.Settings.Default.CredentialsPath;
            AuthCommand = new TryAuthCommand(this);
            TryAuthenticate();
        }

        public bool TryAuthenticate()
        {
            _auth = new Auth() { JsonCredentialPath = _credentialsPath };
            try
            {
                _t2p = new SpeechService(_auth);
            }
            catch (System.IO.FileNotFoundException)
            {
                System.Windows.MessageBox.Show("Could not find the specified file");
                AuthStatus = "Authentication Failed";
                return false;
            }
            catch (SystemException)
            {
                System.Windows.MessageBox.Show("Could not authenticate.\n Write the path to Google Authentication JSON file to \"Credential Path\" and click \"Authenticate\"");
                AuthStatus = "Authentication Failed";
                return false;
            }
            _playback = new Playback();
            SpeechLanguageCodes = SpeechService.GetLanguageCodes();
            SpeechCommand = new GenerateSpeechCommand(this);
            PlaybackDevices = _playback.OutputDevices();
            Genders = _t2p.GetGenders();
            AuthStatus = "Authentication Successful";
            Properties.Settings.Default.CredentialsPath = _credentialsPath;
            Properties.Settings.Default.Save();

            return true;
        }

        private void ChangedLanguage()
        {
            List<string> accents = _t2p.GetAccents(SelectedLanguage.Value);
            Accents = new ObservableCollection<string>(accents);
        }

        public void GenerateSpeech()
        {
            _t2p.Settings.LanguageCode = _selectedAccent;
            _t2p.Settings.SpeakingRate = _selectedSpeakingRate;
            _t2p.Settings.Pitch = _selectedPitch;
            _t2p.Settings.VolumeGain = _selectedVolumeGain;
            _t2p.Settings.SetGender(_selectedGender);
            _playback.SetOutputDevice(_selectedPlaybackDevice.ID);
            var speech = _t2p.GetSpeech(_speechText);
            _playback.StartPlayback(speech);
        }

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
