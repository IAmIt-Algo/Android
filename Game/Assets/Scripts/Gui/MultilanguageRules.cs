using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Mindblower.Gui
{
    public class MultilanguageRules : MonoBehaviour
    {
        [SerializeField]
        private GameObject contentGameObject;
        [SerializeField]
        private MultilanguageText multilanguageTextPrefab;

        private List<string> fullParagraphList;
        private Dictionary<string, Dictionary<string, string>> multilanguage;

        private string language;

        void Awake()
        {
            fullParagraphList = new List<string>();
            multilanguage = new Dictionary<string, Dictionary<string, string>>();
            language = "English";
        }

        private void ReadRules(TextAsset rulesFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(rulesFile.text);

            var root = xmlDoc.DocumentElement;

            fullParagraphList.Clear();
            XmlElement paragraphList = root.SelectSingleNode("ParagraphsSequence") as XmlElement;
            foreach (XmlElement nextParagraph in paragraphList.SelectNodes("Paragraph"))
            {
                string id = nextParagraph.GetAttribute("id");
                fullParagraphList.Add(id);
            }

            foreach (XmlElement languageNode in root.SelectNodes("Language"))
            {
                string language = languageNode.GetAttribute("id");
                multilanguage[language] = new Dictionary<string, string>();

                foreach (XmlElement paragraph in languageNode.SelectNodes("Paragraph"))
                {
                    string paragraphId = paragraph.GetAttribute("id");
                    multilanguage[language][paragraphId] = paragraph.InnerText;
                }
            }
        }

        private void FillContent()
        {
            foreach (var multilanguageText in contentGameObject.GetComponentsInChildren<MultilanguageText>())
            {
                Destroy(multilanguageText.gameObject);
            }

            foreach (var paragraphId in fullParagraphList)
            {
                MultilanguageText multilanguageText = Instantiate(multilanguageTextPrefab);
                multilanguageText.ChangeText(multilanguage[language][paragraphId]);
                multilanguageText.transform.SetParent(contentGameObject.transform);
            }
        }

        public void LoadRules(TextAsset rulesFile)
        {
            ReadRules(rulesFile);
            FillContent();
        }

        public void ChangeLanguage()
        {
            if (language == "English")
                language = "Russian";
            else
                language = "English";
            FillContent();
        }
    }
}

