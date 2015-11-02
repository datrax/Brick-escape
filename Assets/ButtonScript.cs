﻿using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
    void Start()
    {
        if (!PlayerPrefs.HasKey("Level1"))
        {
            PlayerPrefs.SetInt("Level1", 0);
            for (int i = 2; i <= BoxesScript.LEVEL_COUNT; i++)
            {
                PlayerPrefs.SetInt("Level" + i, -1);
            }
        }
        //PlayerPrefs.DeleteAll();
        //print("deleted");
    }
    public void LoadLevel(string name)
    {
        BoxesScript.ApplicationModel.steps = 0;
        BoxesScript.ApplicationModel.LastBlockMoved = "";
        Application.LoadLevel(name);
    }
    public void LoadNextLevel()
    {
      
            BoxesScript.ApplicationModel.LoadLevel++;
            BoxesScript.LoadLevel(BoxesScript.ApplicationModel.LoadLevel);
            Application.LoadLevel("GameScene");        
    }
    public void BuyLevel()
    {
        PlayerPrefs.SetInt("Level" + GameObject.Find("Level").GetComponent<UnityEngine.UI.Text>().text.Remove(0,6), 0);
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
            if (PlayerPrefs.HasKey("NewGame"))
            {
                Application.LoadLevel("GameScene");
            }
            else
            {
                Application.LoadLevel("HelpScene");
            }
        }
        else if (name == "StoreButton")
        {
            Application.LoadLevel("StoreScene");
        }
        else if (name == "TenTipsButton")
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 10);
        }
        else if (name == "FiveteenTipsButton")
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 50);
        }
        else if (name == "HundredTipsButton")
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 100);
        }
        else if (name == "FacebookButton")
        {
            Application.OpenURL("https://www.facebook.com/lockscreeniphone/");
        }
        else if (name == "GooglePlayButton")
        {
            Application.OpenURL("https://play.google.com/store/apps/dev?id=5282599165473944593");
        }
        else if (name == "RateButton")
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.ln.unlockpuzzle");
        }
        else if (name == "OpenNextLevelButton")
        {
            int c = 1;
            //for (; c < 100; c++)
            //{
            //    print(PlayerPrefs.GetInt("Level" + c));
            //}
            while (PlayerPrefs.GetInt("Level" + c) >= 0)
            {
                c++;
            }

            PlayerPrefs.SetInt("Level" + c, 0);
        }
    }
    
}
