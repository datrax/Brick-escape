﻿using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }
    void OnClick()
    {
        if (name == "LevelsButton")
        {
            Application.LoadLevel("LevelsMenuScene");
        }
        else if (name == "BackToMainMenuButton")
        {
            Application.LoadLevel("MainMenuScene");
        }
        else if (name == "QuitButton")
        {
            Application.Quit();
        }
        else if (name == "PlayButton")
        {
            Application.LoadLevel("GameScene");
        }
		else if (name == "StoreButton")
		{
			Application.LoadLevel("StoreScene");		
		}
    }
}
