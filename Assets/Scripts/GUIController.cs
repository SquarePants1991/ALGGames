﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
	public Text scoreText;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = "Score: " + ScoreService.sharedService.totalScore;
	}
}
