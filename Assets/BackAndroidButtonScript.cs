using UnityEngine;
using System.Collections;

public class BackAndroidButtonScript : MonoBehaviour
{

    public GameObject ExitMessage;
    public GameObject MainMenuScene;
    public GameObject LevelsMenuScene;
    public GameObject StoreScene;
    public GameObject GameScene;
    public GameObject HelpScene;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Initializer.CurrentScene ==MainMenuScene)
                {
                    ButtonScript.ActiveMainMenuButtons(false);
                    ExitMessage.SetActive(true);
                }
                else
                {
                    MainMenuScene.SetActive(true);
                    HelpScene.SetActive(false);
                    StoreScene.SetActive(false);
                    LevelsMenuScene.SetActive(false);
                }
            }
        }
    }
}
