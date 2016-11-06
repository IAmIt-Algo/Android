using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leaders : MonoBehaviour {

    public GameObject content;
    public Scrollbar scroll;
    GameObject text;
    public Font font;
    string[] names = { "asdfghhjkl", "qwerty", "zxcvbnm", "aaaaaaaaaaaa", "vbvvbvbvbvb", "a", "s", "d", "f", "c", "v", "b", "e", "w", "q", "u", "i", "k", "l", "m" };

	// Use this for initialization
	void Start () {
        content.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, names.Length*30f);
        for (int t = 0; t < names.Length; t++)
        {
            text = new GameObject("name" + t, typeof(Text), typeof(LayoutElement));
            text.transform.SetParent(content.transform);
            text.GetComponent<LayoutElement>().preferredHeight = 30;
            text.GetComponent<Text>().fontSize = 20;
            text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            text.GetComponent<Text>().text = names[t];
            text.GetComponent<Text>().color = Color.black;
            text.GetComponent<Text>().font = font;
        }
        scroll.value = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
