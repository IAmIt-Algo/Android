using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Mindblower.Core;
using Newtonsoft.Json;
using System;
using System.Threading;

public class Rating : MonoBehaviour, IAmItRequestListener {

    public Text PositionText;
    public Text StarsText;
    int LowestPosition;
    int HighestPosition;
    int StarsCount;
    private bool _visible = false;
    private string _code = "";
    private bool _isShowed = false;

    public GameObject content;
    public Scrollbar scroll;
    GameObject player;
    GameObject userName;
    GameObject position;
    GameObject starsCount;
    public Font font;
    string[] names;
    int?[] stars;

    // Use this for initialization
    void Start () {
        IAmItHttpRequest.Get<GetRatingPositionModel>(IAmItServerMethods.GET_RATING_POSITION, this);
    }

    public void BuildListForScroll()
    {
        content.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, names.Length * 30f);
        for (int t = 0; t < names.Length; t++)
        {
            player = new GameObject("Player" + t, typeof(LayoutElement), typeof(HorizontalLayoutGroup), typeof(RectTransform));
            player.transform.SetParent(content.transform);
            player.GetComponent<LayoutElement>().preferredHeight = 30;
            player.GetComponent<HorizontalLayoutGroup>().childForceExpandHeight = true;
            player.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
            player.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


            position = new GameObject("postion" + t, typeof(Text), typeof(LayoutElement));
            position.transform.SetParent(player.transform);
            position.GetComponent<LayoutElement>().preferredHeight = 30;
            position.GetComponent<LayoutElement>().preferredWidth = 30;
            position.GetComponent<Text>().fontSize = 20;
            position.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            position.GetComponent<Text>().text = t + 1 + "";
            position.GetComponent<Text>().color = Color.black;
            position.GetComponent<Text>().font = font;

            userName = new GameObject("userName" + t, typeof(Text), typeof(LayoutElement));
            userName.transform.SetParent(player.transform);
            userName.GetComponent<LayoutElement>().preferredHeight = 30;
            userName.GetComponent<LayoutElement>().preferredWidth = 440;
            userName.GetComponent<Text>().fontSize = 20;
            userName.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            userName.GetComponent<Text>().text = names[t];
            userName.GetComponent<Text>().color = Color.black;
            userName.GetComponent<Text>().font = font;

            starsCount = new GameObject("starsCount" + t, typeof(Text), typeof(LayoutElement));
            starsCount.transform.SetParent(player.transform);
            starsCount.GetComponent<LayoutElement>().preferredHeight = 30;
            starsCount.GetComponent<LayoutElement>().preferredWidth = 80;
            starsCount.GetComponent<Text>().fontSize = 20;
            starsCount.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            starsCount.GetComponent<Text>().text = stars[t].ToString();
            starsCount.GetComponent<Text>().color = Color.black;
            starsCount.GetComponent<Text>().font = font;
        }
        scroll.value = 1;
    
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnLogin()
    {
        throw new NotImplementedException();
    }

    public void OnFail(string code)
    {
        _isShowed = false;
        _visible = true;
        _code = code;
    }

    void OnGUI()
    {
        if (_visible)
        {
            GUI.Box(new Rect(Screen.width / 2 - Screen.width * 4 / 10, Screen.height * 23 / 32, Screen.width * 4 / 5, Screen.height / 4), "");
            GUI.Label(new Rect(Screen.width / 2 - Screen.width * 4 / 10, Screen.height * 23 / 32, Screen.width * 4 / 5, Screen.height / 4), _code);
            Thread myThread = new Thread(new ThreadStart(Pause));
            myThread.Start();
        }
    }

    public void Pause()
    {
        if (!_isShowed)
        {
            _isShowed = true;
            Debug.Log("Count");
            Thread.Sleep(3000);
            _visible = false;
        }
    }

    public void OnGet<T>(T responseModel)
    {
        GetRatingPositionModel model = responseModel as GetRatingPositionModel;
        Debug.Log(JsonConvert.SerializeObject(responseModel));
        if (model.HighestPosition != model.LowestPosition) {
            PositionText.text = model.HighestPosition + " - " + model.LowestPosition;
        }else
        {
            PositionText.text = model.HighestPosition + "";
        }
        StarsText.text = model.StarsCount + "";

        names = new string[model.Rating.Length];
        stars = new int?[model.Rating.Length];
        for (int i = 0; i<model.Rating.Length; i++)
        {
            names[i] = model.Rating[i].UserName;
            stars[i] = model.Rating[i].StarsCount;
        }

        BuildListForScroll();
    }

    public void OnPost(string s)
    {
        throw new NotImplementedException();
    }

    public void OnLogOut()
    {
        throw new NotImplementedException();
    }
}
