using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
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
