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
    void Start()
    {

    }

    public static bool EscapeEnabled = false;
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Initializer.CurrentScene == MainMenuScene)
                {
                    if (!EscapeEnabled)
                    {
                        ButtonScript.ActiveMainMenuButtons(false);
                        ExitMessage.SetActive(true);
                        EscapeEnabled = true;
                    }
                    else
                    {
                        ButtonScript.ActiveMainMenuButtons(true);
                        ExitMessage.SetActive(false);
                        EscapeEnabled = false;

                    }
                }
                else
                if (Initializer.CurrentScene == GameScene)
                {
                    MainMenuScene.SetActive(true);
                    Initializer.CurrentScene = MainMenuScene;
                    if (PlayerPrefs.GetInt("Adverts") == 1)
                    {
                        if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Hide();
                        if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Destroy();
                        if (GoogleMobileAdsDemoScript.interstitial != null) GoogleMobileAdsDemoScript.interstitial.Destroy();
                    }
                }
                else
                {
                    Initializer.CurrentScene = MainMenuScene;
                    Initializer.Canvas.SetActive(false);
                    MainMenuScene.SetActive(true);
                    HelpScene.SetActive(false);
                    StoreScene.SetActive(false);
                    LevelsMenuScene.SetActive(false);
                }
            }
        }
    }
}
