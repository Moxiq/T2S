using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;

namespace T2PHandler
{
    class TranslationService
    {
        TranslationClient _client;

        public TranslationService(Auth authService)
        {
            TranslationClientBuilder builder = new TranslationClientBuilder { CredentialsPath = authService.JsonCredentialPath };
            _client = builder.Build();
        }

        public string RequestTranslation(string text, string targetLang, string sourceLang = null)
        {
            return _client.TranslateText(text, sourceLang, targetLang).TranslatedText;
        }

        public Dictionary<string, string> LanguageCodes()
        {
            Dictionary<string, string> nameByCode = new Dictionary<string, string>();

            foreach (Google.Cloud.Translation.V2.Language language in _client.ListLanguages())
            {
                nameByCode.Add(language.Name, language.Code);
            }
            return nameByCode;
        }
    }
}
