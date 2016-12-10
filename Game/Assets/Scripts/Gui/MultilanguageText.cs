using UnityEngine;
using UnityEngine.UI;

namespace Mindblower.Gui
{
    [RequireComponent(typeof(Text))]
    public class MultilanguageText : MonoBehaviour
    {
        [SerializeField]
        private string id;

        private Text multilanguageText;

        void Awake()
        {
            multilanguageText = GetComponent<Text>();
        }

        public string Id { get { return id; } }

        public void ChangeText(string newText)
        {
            multilanguageText.text = newText;
            multilanguageText.fontSize = 32;
        }
    }
}
