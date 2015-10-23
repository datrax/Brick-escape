using UnityEngine;
using System.Collections;
using System.Xml;

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
        SetFigure("h2",1,3);
        SetFigure("g2", 5, 4);
        SetFigure("v2", 5, 5);
        SetFigure("v3", 6, 1);
        SetFigure("g3", 1, 1);
        SetFigure("v3", 3, 2);
        SetFigure("v2", 1, 4);
        SetFigure("g3", 1, 6);

    }

    Vector3 SetIn(int x, int y)
    {
        return new Vector3(2, (float)2.4 - y * 1, 90);
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
        t.transform.position = SetIn(0, 3);
        t.transform.parent = this.transform;
        t.transform.localPosition = new Vector3(X,Y, 0);
        t.transform.localScale = new Vector3(Scalar, Scalar, 1);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
