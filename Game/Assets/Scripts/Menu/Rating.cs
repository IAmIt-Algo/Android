using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Mindblower.Core;
using System;

public class Rating : MonoBehaviour, IAmItRequestListener {

    public Text Position;
    public Text Stars;
    int LowestPosition;
    int HighestPosition;
    int StarsCount = 25;

	// Use this for initialization
	void Start () {
        GetRatingPositionModel model = new GetRatingPositionModel();
        IAmItHttpRequest.Get(IAmItServerMethods.GET_RATING_POSITION, this, model);
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
        Position.text = model.HighestPosition + " - " + model.LowestPosition;
        Stars.text = StarsCount + "";
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
