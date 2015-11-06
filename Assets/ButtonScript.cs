using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using PuzzleStore;

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
        if (!PlayerPrefs.HasKey("Adverts"))
        {
            PlayerPrefs.SetInt("Adverts", 1);
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
            BoxesScript.LoadLevel(BoxesScript.ApplicationModel.LoadLevel);
            Application.LoadLevel("GameScene");
        }
        else
        {
            Application.LoadLevel("MainMenuScene");
        }
    }
    public void BuyLevel()
    {
        StoreInventory.BuyItem(PuzzleStoreAssets.UNLOCKLEVEL_ITEM_ID);

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
    void Update()
    {
        if (Application.loadedLevelName == "StoreScene")
        {
            if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.HUNDRED_TIPS_PACK_ID) > 0)
            {
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 100);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.HUNDRED_TIPS_PACK_ID, 1);
                GameObject.Find("Camera").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
                GameObject.Find("Panel").SetActive(false);
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.REMOVEADS_PACK_ID) > 0)
            {
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.REMOVEADS_PACK_ID, 1);
                PlayerPrefs.SetInt("Adverts", 0);
                GameObject.Find("Camera").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
                if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Hide();
                if (GoogleMobileAdsDemoScript.interstitial != null) GoogleMobileAdsDemoScript.interstitial.Destroy();
                if (GoogleMobileAdsDemoScript.bannerView != null) GoogleMobileAdsDemoScript.bannerView.Destroy();
                GameObject.Find("Panel").SetActive(false);
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.TEN_TIPS_PACK_ID) > 0)
            {
                GameObject.Find("Camera").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 10);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.TEN_TIPS_PACK_ID, 1);
                GameObject.Find("Panel").SetActive(false);
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.FIFTY_TIPS_PACK_ID) > 0)
            {
                GameObject.Find("Camera").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 50);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.FIFTY_TIPS_PACK_ID, 1);
                GameObject.Find("Panel").SetActive(false);
            }
            else if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID) > 0 && Application.loadedLevelName == "StoreScene")
            {
                int c = 1;
                while (PlayerPrefs.GetInt("Level" + c) >= 0)
                {
                    c++;
                }
                GameObject.Find("Camera").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID, 1);
                PlayerPrefs.SetInt("Level" + c, 0);
                GameObject.Find("Panel").SetActive(false);
            }
            
        }
        else if (Application.loadedLevelName == "LevelsMenuScene")
        {
            if (StoreInventory.GetItemBalance(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID) > 0)
            {
                PlayerPrefs.SetInt("Level" + GameObject.Find("Level").GetComponent<UnityEngine.UI.Text>().text.Remove(0, 6), 0);
                GameObject.Find("LevelMenu").GetComponent<ShowMessageScript>().MessageStatus.SetActive(true);
                StoreInventory.TakeItem(PuzzleStore.PuzzleStoreAssets.UNLOCKLEVEL_PACK_ID, 1);
            }
        }
    }
    
}
