using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Assets.Scripts;

public class Initializer : MonoBehaviour
{
    public Sprite CongratText1;
    public Sprite CongratText2;
    public Sprite CongratText3;
    public Sprite StarOn;
    public Sprite StarOff;
    public GameObject CongratMessage;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;
    public GameObject block5;
    public float Scalar = 48;
    public float cellSize = 49;

    // Use this for initialization
    public void Start()
    {
        if (!PlayerPrefs.HasKey("NewGame"))
        {
            PlayerPrefs.SetInt("NewGame", 1);
        }
        if (!PlayerPrefs.HasKey("Hints"))
        {
            PlayerPrefs.SetInt("Hints", 5);
        }
        GameObject.Find("Solver").transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("Hints").ToString();
		if(GameObject.Find("ShowMessage")!=null){
			GameObject.Find("ShowMessage").SetActive(false);
		}
        if (!PlayerPrefs.HasKey("Level1"))
        {
            PlayerPrefs.SetInt("Level1", 0);
        }
        int lev = 1;

        while (PlayerPrefs.HasKey("Level" + lev))
        {
            if (PlayerPrefs.GetInt("Level" + lev) >= 0)
            {
                lev++;
            }
            else
            {
                break;
            }
        }

        if (BoxesScript.ApplicationModel.LoadLevel == -1)
        {
            BoxesScript.ApplicationModel.LoadLevel = lev - 1;
        }
        var level = BoxesScript.ApplicationModel.LoadLevel;
        DestroyOldBlocks();
        LoadLevel(level);
  
        GameObject.Find("PuzzleNumber").GetComponent<UnityEngine.UI.Text>().text = level.ToString();

        if (PlayerPrefs.GetInt("Adverts") == 1&&CheckForInternetConnection())
        {
            if (GoogleMobileAdsDemoScript.bannerView != null)
            {
                GoogleMobileAdsDemoScript.bannerView.Hide();
                GoogleMobileAdsDemoScript.bannerView.Destroy();
            }
            GoogleMobileAdsDemoScript.RequestBanner();
            GoogleMobileAdsDemoScript.bannerView.Show();

        }
        else
        {
            GameObject.Find("BackToMainMenuScene").transform.localPosition = new Vector3(GameObject.Find("BackToMainMenuScene").transform.localPosition.x, -235.5f);
            GameObject.Find("Solver").transform.localPosition = new Vector3(GameObject.Find("Solver").transform.localPosition.x, -235.5f);
            GameObject.Find("RefreshButton").transform.localPosition = new Vector3(GameObject.Find("RefreshButton").transform.localPosition.x, -235.5f);
            GameObject.Find("SoundButton").transform.localPosition = new Vector3(GameObject.Find("SoundButton").transform.localPosition.x, -235.5f);
        }
     
    }
    bool CheckForInternetConnection()
    {
        WebClient client =null;
        Stream stream=null;
        try
        {
            client = new System.Net.WebClient();
            stream = client.OpenRead("http://www.google.com");
            return true;
        }
        catch 
        {
            return false;
        }
        finally
        {
            if (client!=null) { client.Dispose(); }
            if (stream!= null) { stream.Dispose(); }
        }
    }
    public void LoadLevel(int number)
    {

           // DestroyOldBlocks();
        string level = Keeper.Levels[number - 1];

        for (int i = 0; i < level.Length; i += 4)
        {
            int x = int.Parse(level.Substring(i + 2, 1));
            int y = int.Parse(level.Substring(i + 3, 1));
            SetFigure(level.Substring(i, 2), x, y);
        }
        if (PlayerPrefs.GetInt("Adverts") == 1)
        {
            if (BoxesScript.ApplicationModel.LoadLevel%2 == 0)
            {
                if (GoogleMobileAdsDemoScript.interstitial != null)
                {
                    GoogleMobileAdsDemoScript.interstitial.Destroy();
                }
                GoogleMobileAdsDemoScript.RequestInterstitial();
            }

        }
    }
    public void ShowCongratulationMessage()
    {
        DestroyOldBlocks();
        CongratMessage.SetActive(true);
        GameObject.Find("Moves").GetComponent<UnityEngine.UI.Text>().text = "Moves: " + BoxesScript.ApplicationModel.steps;
        GameObject.Find("Perfect").GetComponent<UnityEngine.UI.Text>().text = "Perfect: " + Keeper.solvers[BoxesScript.ApplicationModel.LoadLevel - 1].Count.ToString();
        var star1 = GameObject.Find("Star1").GetComponent<UnityEngine.UI.Image>();
        var star2 = GameObject.Find("Star2").GetComponent<UnityEngine.UI.Image>();
        var star3 = GameObject.Find("Star3").GetComponent<UnityEngine.UI.Image>();
        star1.sprite = StarOn;
        star2.sprite = StarOff;
        star3.sprite = StarOff;
        var currLevel = int.Parse(GameObject.Find("PuzzleNumber").GetComponent<UnityEngine.UI.Text>().text);
        // Best set
        if (!PlayerPrefs.HasKey("Best" + currLevel))
        {
            PlayerPrefs.SetInt("Best" + currLevel, BoxesScript.ApplicationModel.steps);
            GameObject.Find("Best").GetComponent<UnityEngine.UI.Text>().text = "Best: " + BoxesScript.ApplicationModel.steps;
        }
        else
        {
            if (PlayerPrefs.GetInt("Best" + currLevel) > BoxesScript.ApplicationModel.steps)
            {
                PlayerPrefs.SetInt("Best" + currLevel, BoxesScript.ApplicationModel.steps);
                GameObject.Find("Best").GetComponent<UnityEngine.UI.Text>().text = "Best: " + BoxesScript.ApplicationModel.steps;
            }
            else
            {
                GameObject.Find("Best").GetComponent<UnityEngine.UI.Text>().text = "Best: " + PlayerPrefs.GetInt("Best" + currLevel);
            }
        }
        if (PlayerPrefs.HasKey("Level" + currLevel))
        {
            if (PlayerPrefs.GetInt("Level" + currLevel) < 1)
            {
                PlayerPrefs.SetInt("Level" + currLevel, 1);
            }
        }
        var solving = GameObject.Find("Solver").GetComponent<SolveThePuzzle>().solving;
        var status = GameObject.Find("ResultStatus").GetComponent<UnityEngine.UI.Image>();
        status.sprite = CongratText1;
        PlayerPrefs.SetInt("Level" + currLevel, 1);
        if (BoxesScript.ApplicationModel.steps <= Keeper.solvers[BoxesScript.ApplicationModel.LoadLevel - 1].Count + 14 && !solving)
        {
            star2.sprite = StarOn;
            status.sprite = CongratText2;
            if (PlayerPrefs.GetInt("Level" + currLevel) <= 1)
            {
                PlayerPrefs.SetInt("Level" + currLevel, 2);
            }

        }
        if (BoxesScript.ApplicationModel.steps <= Keeper.solvers[BoxesScript.ApplicationModel.LoadLevel - 1].Count + 7 && !solving)
        {
            star2.sprite = StarOn;
            star3.sprite = StarOn;
            status.sprite = CongratText3;
            if (PlayerPrefs.GetInt("Level" + currLevel) <= 2)
            {
                PlayerPrefs.SetInt("Level" + currLevel, 3);
            }
        }
        // Open next level
        int c = 1;
        if (PlayerPrefs.HasKey("Level" + (currLevel + 1)))
        {
            if (PlayerPrefs.GetInt("Level" + (currLevel + 1)) == -1)
            {
                PlayerPrefs.SetInt("Level" + (currLevel + 1), 0);
            }
        }


    }
    public void DestroyOldBlocks()
    {
        var amount = GameObject.Find("Blocks").transform.childCount;
        for (int i=0;i<amount;i++ )
        {
            Destroy(GameObject.Find("Blocks").transform.GetChild(i).gameObject);
        }

    }
    void SetFigure(string figure, int x, int y)
    {
        var size = int.Parse(figure[1].ToString());
        var vertical = (figure[0] == 'v');
        float yc = 0.5f;
        float xc = 0.5f;
        if (vertical)
        {
            xc = 0.5f;
            yc = size == 2 ? 1 : 1.5f;
        }
        else
        {
            yc = 0.5f;
            xc = size == 2 ? 1 : 1.5f;
        }
        var Y = 160 - 46 * yc - cellSize * (y - 1);
        var X = -135 + 46 * xc + cellSize * (x - 1);
        GameObject t = null;
        switch (figure)
        {
            case "v2":
                t = Instantiate(block1);
                break;
            case "g3":
                t = Instantiate(block2);
                break;
            case "v3":
                t = Instantiate(block3);
                break;
            case "g2":
                t = Instantiate(block4);
                break;
            case "h2":
                t = Instantiate(block5);
                break;
        }
        if (t != null)
        {
            t.GetComponent<BlockScript>().codeName = figure.ToString() + x.ToString() + y.ToString();
            t.tag = "Block";
         //   t.transform.parent = this.transform;
            t.transform.parent = GameObject.Find("Blocks").transform;
            t.transform.localPosition = new Vector3(X, Y, 0);
            t.transform.localScale = new Vector3(Scalar, Scalar, 1);
            t.GetComponent<BlockScript>().MoveToGrid();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }



}
