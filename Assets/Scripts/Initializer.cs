using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    void Start () {
        if (!PlayerPrefs.HasKey("Hints"))
        {
            PlayerPrefs.SetInt("Hints", 5);
        }
        GameObject.Find("Solver").transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("Hints").ToString();
        if (!PlayerPrefs.HasKey("Level1"))
        {
            PlayerPrefs.SetInt("Level1", 0);
        }
        //level 2 was changed
        //pay attention  solution searching for level 100 takes more than 1 minute
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
        LoadLevel(level);
        GameObject.Find("PuzzleNumber").GetComponent<UnityEngine.UI.Text>().text = level.ToString();

    }

    public void LoadLevel(int number)
    {
    //    DestroyOldBlocks();
        string level = Keeper.Levels[number - 1];
        
        for (int i = 0; i < level.Length; i+=4)
        {
            int x = int.Parse(level.Substring(i+2, 1));
            int y = int.Parse(level.Substring(i+3, 1));
            SetFigure(level.Substring(i,2), x, y);
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

        var status = GameObject.Find("ResultStatus").GetComponent<UnityEngine.UI.Image>();
        status.sprite = CongratText1;
        if (BoxesScript.ApplicationModel.steps <= Keeper.solvers[BoxesScript.ApplicationModel.LoadLevel - 1].Count + 14)
        {
            star2.sprite = StarOn;
            status.sprite = CongratText2;
            if (PlayerPrefs.GetInt("Level" + currLevel) <= 1)
            {
                PlayerPrefs.SetInt("Level" + currLevel, 2);
            }

        }
        if (BoxesScript.ApplicationModel.steps <= Keeper.solvers[BoxesScript.ApplicationModel.LoadLevel - 1].Count + 7)
        {
            star2.sprite = StarOn;
            star3.sprite = StarOn;
            status.sprite = CongratText3;
            if (PlayerPrefs.GetInt("Level" + currLevel) <= 2)
            {
                PlayerPrefs.SetInt("Level" + currLevel, 3);
            }
        }
        int c = 1;
        while (PlayerPrefs.GetInt("Level" + c) >0)
        {
            c++;
        }
        PlayerPrefs.SetInt("Level" + c, 0);

    }
    public void DestroyOldBlocks()
    {
        var oldBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (var block in oldBlocks)
        {
            Destroy(block);
        }
        
    }
    void SetFigure(string figure, int x,int y)
    {
        var size = int.Parse(figure[1].ToString());
        var vertical = (figure[0]=='v');
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
        var Y = 160 - 46*yc- cellSize * (y-1);
        var X = -135 + 46*xc + cellSize*(x - 1);
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
            t.transform.parent = this.transform;
            t.transform.localPosition = new Vector3(X, Y, 0);
            t.transform.localScale = new Vector3(Scalar, Scalar, 1);
            t.GetComponent<BlockScript>().MoveToGrid();
        }
    }
    // Update is called once per frame
    void Update () {
	
	}

  

}
