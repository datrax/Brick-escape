using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Soomla;
using Soomla.Store;
using PuzzleStore;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    public void SetScene(GameObject scene)
    {
        Initializer.CurrentScene = scene;
    }
    public GameObject ExitMessage;
    public GameObject MainMenuScene;
    public GameObject LevelsMenuScene;
    public GameObject StoreScene;
    public GameObject GameScene;
    public GameObject HelpScene;
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
        if (!PlayerPrefs.HasKey("Adverts"))
        {
            PlayerPrefs.SetInt("Adverts", 1);
        }
        // PlayerPrefs.DeleteAll();
        //print("deleted");
    }
  /*  public void LoadLevel(string name)
    {
        BoxesScript.ApplicationModel.steps = 0;
        BoxesScript.ApplicationModel.LastBlockMoved = "";
     //   Application.LoadLevel(name);
    }*/

    public void RefreshLevel()
    {
        GameObject.Find("Solver").GetComponent<SolveThePuzzle>().solving = false;
        transform.parent.GetComponent<Initializer>().Start();
        BoxesScript.ApplicationModel.steps = 0;
        BoxesScript.ApplicationModel.LastBlockMoved = "";
        GameObject.Find("Step").GetComponent<Text>().text = "moves : " + BoxesScript.ApplicationModel.steps;
    }
    public void RefreshLevelAndHideCongratMessage()
    {
        GameObject.Find("Solver").GetComponent<SolveThePuzzle>().solving = false;
        transform.parent.parent.GetComponent<Initializer>().Start();
        BoxesScript.ApplicationModel.steps = 0;
        BoxesScript.ApplicationModel.LastBlockMoved = "";
        GameObject.Find("Step").GetComponent<Text>().text = "moves : " + BoxesScript.ApplicationModel.steps;
        GameObject.Find("ShowMessage").SetActive(false);
    }
    public void BackToMainMenu()
    {
        //   GameScene.SetActive(false);
        Initializer.CurrentScene = MainMenuScene;
        MainMenuScene.SetActive(true);
    }
    public void LoadNextLevel()
    {
        if (PlayerPrefs.HasKey("Level" + (BoxesScript.ApplicationModel.LoadLevel + 1)))
        {
            if (PlayerPrefs.GetInt("Adverts")==1)
            {
                if (BoxesScript.ApplicationModel.LoadLevel % 2 == 0)
                    GoogleMobileAdsDemoScript.ShowInterstitial();
                if (GoogleMobileAdsDemoScript.bannerView != null)
                {
                    GoogleMobileAdsDemoScript.bannerView.Hide();
                    GoogleMobileAdsDemoScript.bannerView.Destroy();
                }
            }
            BoxesScript.ApplicationModel.LoadLevel++;
          //  BoxesScript.LoadLevel(BoxesScript.ApplicationModel.LoadLevel);

            GameObject.Find("Solver").GetComponent<SolveThePuzzle>().solving = false;
            transform.parent.parent.GetComponent<Initializer>().Start();
            BoxesScript.ApplicationModel.steps = 0;
            BoxesScript.ApplicationModel.LastBlockMoved = "";
            GameObject.Find("Step").GetComponent<Text>().text = "moves : " + BoxesScript.ApplicationModel.steps;
            GameObject.Find("ShowMessage").SetActive(false);
        }
        else
        {
            BackToMainMenu();
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void BuyLevel()
    {
        StoreInventory.BuyItem(PuzzleStoreAssets.UNLOCKLEVEL_ITEM_ID);

    }
    public static void ActiveMainMenuButtons(bool active)
    {
        GameObject.Find("PlayButton").GetComponent<BoxCollider>().enabled = active;
        GameObject.Find("LevelsButton").GetComponent<BoxCollider>().enabled = active;
        GameObject.Find("StoreButton").GetComponent<BoxCollider>().enabled = active;
        GameObject.Find("QuitButton").GetComponent<BoxCollider>().enabled = active;
        GameObject.Find("FacebookButton").GetComponent<BoxCollider>().enabled = active;
        GameObject.Find("GooglePlayButton").GetComponent<BoxCollider>().enabled = active;
        GameObject.Find("RateButton").GetComponent<BoxCollider>().enabled = active;
    }
    void OnClick()
    {
        if (name == "LevelsButton")
        {
            Initializer.Canvas.SetActive(false);
            LevelsMenuScene.SetActive(true);
            Initializer.CurrentScene = LevelsMenuScene;
            GameObject.Find("MainMenuScene").SetActive(false);
            GameObject.Find("InvisibleScroll").GetComponent<BoxesScript>().ShowBoxes();
        }
        else if (name == "BackToMainMenuButton")
        {
            MainMenuScene.SetActive(true);
            Initializer.CurrentScene = MainMenuScene;
            transform.parent.parent.parent.parent.parent.gameObject.SetActive(false);
            Initializer.Canvas.SetActive(false);
        }
        else if (name == "QuitButton")
        {
            ActiveMainMenuButtons(false);
            ExitMessage.SetActive(true);
        }
        else if (name == "PlayButton")
        {
            if (PlayerPrefs.HasKey("NewGame"))
            {
                // Application.LoadLevel("GameScene");
                // GameScene.SetActive(true);
                Initializer.CurrentScene = GameScene;
                Initializer.Canvas.SetActive(true);
                MainMenuScene.SetActive(false);
                Initializer.Canvas.GetComponent<Initializer>().Start();
            }
            else
            {
                Initializer.CurrentScene = HelpScene;
                HelpScene.SetActive(true);
                MainMenuScene.SetActive(false);
                PlayerPrefs.SetInt("NewGame", 1);
            }
        }
        else if (name == "StoreButton")
        {
            Initializer.Canvas.SetActive(false);
            StoreScene.SetActive(true);
            Initializer.CurrentScene = StoreScene;
            MainMenuScene.SetActive(false);
        }
        else if (name == "TenTipsButton")
        {
            StoreInventory.BuyItem(PuzzleStore.PuzzleStoreAssets.TENTIPS_ITEM_ID);
        }
        else if (name == "FiveteenTipsButton")
        {
            StoreInventory.BuyItem(PuzzleStore.PuzzleStoreAssets.FIFTY_ITEM_ID);
        }
        else if (name == "HundredTipsButton")
        {
            StoreInventory.BuyItem(PuzzleStore.PuzzleStoreAssets.HUNDRED_ITEM_ID);
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
            StoreInventory.BuyItem(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_ITEM_ID);
        }
        else if (name == "RemoveAdsButton")
        {
            if (PlayerPrefs.GetInt("Adverts") == 1)
            {
                StoreInventory.BuyItem(PuzzleStore.PuzzleStoreAssets.REMOVEADS_ITEM_ID);
            }

        }
        else if (name == "UnlockAllLevelsButton")
        {
            int c = 1;
            while (c < 100 && PlayerPrefs.GetInt("Level" + c) >= 0)
            {
                c++;
            }
            if (c < BoxesScript.LEVEL_COUNT)
            {
                StoreInventory.BuyItem(PuzzleStore.PuzzleStoreAssets.UNLOCKALLLEVELS_ITEM_ID);
            }
        }

    }

    public void DisableAdv()
    {
        if (PlayerPrefs.GetInt("Adverts") == 1)
        {
            if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Hide();
            if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Destroy();
            if (GoogleMobileAdsDemoScript.interstitial != null) GoogleMobileAdsDemoScript.interstitial.Destroy();
        }
    }
    public void LockPanel()
    {
        GameObject.Find("Camera").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
        GameObject.Find("RemoveAdsButton").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("RemoveAdsButton").GetComponent<UIButton>().enabled = false;
        GameObject.Find("OpenNextLevelButton").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("OpenNextLevelButton").GetComponent<UIButton>().enabled = false;
        GameObject.Find("TenTipsButton").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("TenTipsButton").GetComponent<UIButton>().enabled = false;
        GameObject.Find("FiveteenTipsButton").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("FiveteenTipsButton").GetComponent<UIButton>().enabled = false;
        GameObject.Find("HundredTipsButton").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("HundredTipsButton").GetComponent<UIButton>().enabled = false;
        GameObject.Find("UnlockAllLevelsButton").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("UnlockAllLevelsButton").GetComponent<UIButton>().enabled = false;
    }
    public void UnlockPanel()
    {

        GameObject.Find("RemoveAdsButton").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("RemoveAdsButton").GetComponent<UIButton>().enabled = true;
        GameObject.Find("OpenNextLevelButton").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("OpenNextLevelButton").GetComponent<UIButton>().enabled = true;
        GameObject.Find("TenTipsButton").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("TenTipsButton").GetComponent<UIButton>().enabled = true;
        GameObject.Find("FiveteenTipsButton").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("FiveteenTipsButton").GetComponent<UIButton>().enabled = true;
        GameObject.Find("HundredTipsButton").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("HundredTipsButton").GetComponent<UIButton>().enabled = true;
        GameObject.Find("UnlockAllLevelsButton").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("UnlockAllLevelsButton").GetComponent<UIButton>().enabled = true;
        GameObject.Find("Camera").GetComponent<ShowMessageScript>().MessageStatus.SetActive(false);
    }
    void Update()
    {
        if (Initializer.CurrentScene == null)
        {
            Initializer.CurrentScene = GameObject.Find("MainMenuScene");
            Initializer.Canvas = GameObject.Find("Canvas");
            Initializer.Canvas.SetActive(false);
        }
        if (Initializer.CurrentScene.name == "StoreScene")
        {
            if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.HUNDRED_TIPS_PACK_ID) > 0)
            {
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 100);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.HUNDRED_TIPS_PACK_ID, 1);
              
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.REMOVEADS_PACK_ID) > 0)
            {
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.REMOVEADS_PACK_ID, 1);
                PlayerPrefs.SetInt("Adverts", 0);
                if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Hide();
                if (GoogleMobileAdsDemoScript.interstitial != null) GoogleMobileAdsDemoScript.interstitial.Destroy();
                if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Destroy();
                LockPanel();
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.TEN_TIPS_PACK_ID) > 0)
            {
                
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 10);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.TEN_TIPS_PACK_ID, 1);
                LockPanel();
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.FIFTY_TIPS_PACK_ID) > 0)
            {
                
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 50);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.FIFTY_TIPS_PACK_ID, 1);
                LockPanel();
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID) > 0)
            {
                int c = 1;
                while (PlayerPrefs.GetInt("Level" + c) >= 0)
                {
                    c++;
                }
                
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID, 1);
                PlayerPrefs.SetInt("Level" + c, 0);
                LockPanel();
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.UNLOCKALLLEVELS_PACK_ID) > 0)
            {
                for (int c = 1; c <= 100; c++)
                {
                    if (PlayerPrefs.GetInt("Level" + c) < 0)
                    {
                        PlayerPrefs.SetInt("Level" + c, 0);
                    }
                }

                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.UNLOCKALLLEVELS_PACK_ID, 1);
                LockPanel();
            }
        }
        else if (Initializer.CurrentScene.name == "LevelsMenuScene")
        {
            if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID) > 0)
            {
                PlayerPrefs.SetInt("Level" + GameObject.Find("Level").GetComponent<UnityEngine.UI.Text>().text.Remove(0, 6), 0);
                GameObject.Find("LevelMenu").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID, 1);
                GameObject.Find("InvisibleScroll").GetComponent<BoxesScript>().ShowBoxes();
            }
        }
    }
    
}
