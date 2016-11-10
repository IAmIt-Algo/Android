using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Mindblower.Core;
using Newtonsoft.Json;
using System;

public class Rating : MonoBehaviour, IAmItRequestListener {

    public Text PositionText;
    public Text StarsText;
    int LowestPosition;
    int HighestPosition;
    int StarsCount;

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
        GetRatingPositionModel model = new GetRatingPositionModel();
        IAmItHttpRequest.Get(IAmItServerMethods.GET_RATING_POSITION, this, model);
    }

    public void BuildListForScroll()
    {
        content.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, names.Length * 30f);
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
        Debug.Log("Rating: " + code);
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
