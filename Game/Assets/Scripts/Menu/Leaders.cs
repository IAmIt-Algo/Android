using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leaders : MonoBehaviour {

    public GameObject content;
    public Scrollbar scroll;
    GameObject player;
    GameObject userName;
    GameObject position;
    GameObject starsCount;
    public Font font;
    string[] names = { "dfghhjklqwe", "wertyqwerty", "xcvbnmqwer", "aaaaaaaaa", "vvbvbvbv", "a", "s", "d", "f", "c", "v", "b", "e", "w", "q", "u", "i", "k", "l", "m" };
    string[] stars = { "122337", "12134", "1233", "233", "23", "19", "18", "17", "17", "9", "8", "8", "7", "6", "5", "0", "0", "0", "0", "0" };

    // Use this for initialization
    void Start () {
        content.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, (names.Length+1)*30f);
        for (int t = 0; t < names.Length; t++)
        {
            player = new GameObject("Player" + t, typeof(LayoutElement), typeof(HorizontalLayoutGroup), typeof(RectTransform));
            player.transform.SetParent(content.transform);
            player.GetComponent<LayoutElement>().preferredHeight = 30;
            player.GetComponent<HorizontalLayoutGroup>().childForceExpandHeight = false;
            player.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
            player.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


            position = new GameObject("postion" + t, typeof(Text), typeof(LayoutElement));
            position.transform.SetParent(player.transform);
            position.GetComponent<LayoutElement>().preferredHeight = 30;
            position.GetComponent<LayoutElement>().preferredWidth = 45;
            position.GetComponent<Text>().fontSize = 20;
            position.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            position.GetComponent<Text>().text = t + 1 + "";
            position.GetComponent<Text>().color = Color.black;
            position.GetComponent<Text>().font = font;

            userName = new GameObject("userName" + t, typeof(Text), typeof(LayoutElement));
            userName.transform.SetParent(player.transform);
            userName.GetComponent<LayoutElement>().preferredHeight = 30;
            userName.GetComponent<LayoutElement>().preferredWidth = 307;
            userName.GetComponent<Text>().fontSize = 20;
            userName.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            userName.GetComponent<Text>().text = names[t];
            userName.GetComponent<Text>().color = Color.black;
            userName.GetComponent<Text>().font = font;

            starsCount = new GameObject("starsCount" + t, typeof(Text), typeof(LayoutElement));
            starsCount.transform.SetParent(player.transform);
            starsCount.GetComponent<LayoutElement>().preferredHeight = 30;
            starsCount.GetComponent<LayoutElement>().preferredWidth = 135;
            starsCount.GetComponent<Text>().fontSize = 20;
            starsCount.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            starsCount.GetComponent<Text>().text = stars[t];
            starsCount.GetComponent<Text>().color = Color.black;
            starsCount.GetComponent<Text>().font = font;
        }
        scroll.value = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
