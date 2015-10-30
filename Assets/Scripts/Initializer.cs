using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Assets.Scripts;

public class Initializer : MonoBehaviour
{
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

        //level 2 was changed
        //pay attention  solution searching for level 100 takes more than 1 minute
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
        /////////////////////////////////////////////////////////////////////////***************************TO EDIT!!!********************////
        GameObject.Find("Best").GetComponent<UnityEngine.UI.Text>().text = "Best: " + BoxesScript.ApplicationModel.steps;
        GameObject.Find("Perfect").GetComponent<UnityEngine.UI.Text>().text = "Perfect: " + Keeper.solvers[BoxesScript.ApplicationModel.LoadLevel - 1].Count.ToString();
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
