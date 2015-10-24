using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
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
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
