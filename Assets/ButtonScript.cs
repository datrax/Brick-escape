using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
    void Start()
    {
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
        if (BoxesScript.ApplicationModel.LoadLevel >= 5)
        {
            Application.LoadLevel("MainMenuScene");
        }
        else
        {
            BoxesScript.ApplicationModel.LoadLevel++;
            BoxesScript.LoadLevel(BoxesScript.ApplicationModel.LoadLevel);
            Application.LoadLevel("GameScene");
        }
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
    }
    
}
