﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour {

	public Button button1; //back
	public Button button2; //newgame
	public Button button3; //resume
	public Button button4; //save and quit
	public bool gamePaused = false;
	public bool onStart = true;
	public int saved = 0;
	// Use this for initialization
	void Start () {
		saved = PlayerPrefs.GetInt ("saved", 0);
		button4.gameObject.SetActive (false);
		if (saved == 0) {
			button3.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!(onStart)) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (gamePaused) {
					button4.gameObject.SetActive (false);
					if (saved == 1) {
						button2.gameObject.SetActive (false);
						button3.gameObject.SetActive (false);
					}
					gamePaused = false;
				} else {
					button4.gameObject.SetActive (true);
					if (saved == 1) {
						button2.gameObject.SetActive (true);
						button3.gameObject.SetActive (true);
					}
					gamePaused = true;
				}
			}
		}
	}

	public void ExitStartMenu()
	{
		onStart = false;
		button2.gameObject.SetActive (false);
		button3.gameObject.SetActive (false);
	}
}
