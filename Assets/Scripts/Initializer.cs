using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Assets.Scripts;

public class Initializer : MonoBehaviour
{

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
        

        LoadLevel(BoxesScript.ApplicationModel.LoadLevel);

    }

    public void LoadLevel(int number)
    {
        string level = Keeper.Levels[number - 1];
        var oldBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (var block in oldBlocks)
        {
            Destroy(block);
        }
        for (int i = 0; i < level.Length; i+=4)
        {
            int x = int.Parse(level.Substring(i+2, 1));
            int y = int.Parse(level.Substring(i+3, 1));
            SetFigure(level.Substring(i,2), x, y);
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

        GameObject obj=new GameObject();
        if (figure == "v2") obj = block1;
        if (figure == "g3") obj = block2;
        if (figure == "v3") obj = block3;
        if (figure == "g2") obj = block4;
        if (figure == "h2") obj = block5;
        var t = Instantiate(obj);
        t.GetComponent<BlockScript>().codeName = figure.ToString() + x.ToString() + y.ToString();
        t.tag = "Block";
        t.transform.parent = this.transform;
        t.transform.localPosition = new Vector3(X,Y, 0);
        t.transform.localScale = new Vector3(Scalar, Scalar, 1);
        t.GetComponent<BlockScript>().MoveToGrid();
    }
    // Update is called once per frame
    void Update () {
	
	}

  

}
