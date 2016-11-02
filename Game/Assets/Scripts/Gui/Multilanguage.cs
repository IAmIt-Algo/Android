using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Mindblower.Gui
{
    public class Multilanguage : MonoBehaviour
    {
        [SerializeField]
        private TextAsset multiLanguageXml;

        private string language = "English";

        private Dictionary<string, Dictionary<string, string>> multilanguage;

        private void InitMultilanguage()
        {
            multilanguage = new Dictionary<string, Dictionary<string, string>>();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(multiLanguageXml.text);

            var root = xmlDoc.DocumentElement;
            var nodesList = root.SelectNodes("Language");

            foreach (XmlElement node in nodesList)
            {
                string language = node.GetAttribute("id");
                multilanguage[language] = new Dictionary<string, string>();

                var concreteLanguage = multilanguage[language];
                foreach (XmlElement field in node.ChildNodes)
                {
                    concreteLanguage[field.Name] = field.InnerText;
                }
                    
            }
        }

        void Awake()
        {
            InitMultilanguage();

        }

        public void ChangeLanguage()
        {
            if (language == "English")
            {
                language = "Russian";
            }
            else
            {
                language = "English";
            }

            foreach (var multilanguageText in GetComponentsInChildren<MultilanguageText>())
            {
                if (multilanguage[language].ContainsKey(multilanguageText.Id))
                    multilanguageText.ChangeText(multilanguage[language][multilanguageText.Id]);
            }   
        }

        void Start()
        {
            foreach (var multilanguageText in GetComponentsInChildren<MultilanguageText>())
                multilanguageText.ChangeText(multilanguage[language][multilanguageText.Id]);
        }
    }
}


