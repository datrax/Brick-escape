using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{

    private bool win = false;
    // Use this for initialization
    void Start ()
	{
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    var t = GetComponent<RectTransform>();

        //      print(t.position.y-t.rect.height);

        //       print(t.localPosition);


        /*    float xc = 1f;
            var cellSize = 46;
            float cellX = (7f - (135 - (t.localPosition.x - (46 * xc))) / cellSize);
            if (cellX==5)transform.position=new Vector3(transform.position.x+5000,transform.position.y);*/
	    if (win)
	    {
	        GetComponent<Rigidbody2D>()
	            .MovePosition(new Vector2(transform.position.x +Time.deltaTime*2, transform.position.y));
	    }
    }

    private Vector3 GetPosition(int x, int y)
    {
        var cellSize = 45;
        var size = int.Parse(name[1].ToString());
        var vertical = (name[0] == 'v');
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
        var Y = 160 - 46*yc - cellSize*(y - 1);
        var X = -135 + 46*xc + cellSize*(x - 1);
        return new Vector3(X,Y);
    }

    void GetXCoordinate()
    {
        
    }

    Vector3 SetIn(int x, int y)
    {
        return new Vector3(2,(float)2.4-y*1,90);
    }
    void OnMouseDrag()
    {
              

        Vector2 pos;
        GameObject myCanvas = transform.parent.gameObject;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, Camera.main, out pos);
        var poss = myCanvas.transform.TransformPoint(pos);
        GetComponent<Rigidbody2D>()
            .MovePosition((name[0] == 'v')
                ? new Vector2(transform.position.x, poss.y)
                : new Vector2(poss.x, transform.position.y));
    }
    void OnMouseDown()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;

    }

    private void MoveToGrid()
    {
        var t = GetComponent<RectTransform>();
        var size = int.Parse(name[1].ToString());
        float yc = 0.5f;
        float xc = 0.5f;
        var cellSize = 46;
        var vertical = (name[0] == 'v');
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

        float cellX = (7f - (135 - (t.localPosition.x - (46*xc)))/cellSize);
        float cellY = (1 + (160 - (t.localPosition.y + (46*yc)))/cellSize);
        print((160 - (t.localPosition.y - (46*yc)))/cellSize);
        //костыль
        var pos = GetPosition(Convert.ToInt32(Math.Round(cellX, 0)), Convert.ToInt32(Math.Round(cellY, 0)));
        if (!vertical)
            transform.localPosition = new Vector3(pos.x, transform.localPosition.y);
        else
            transform.localPosition = new Vector3(transform.localPosition.x, pos.y);

        if (name[0] == 'h' && Convert.ToInt32(Math.Round(cellX, 0)) >= 5)
        {
            print("azazza");
            win = true;
        }
    }

    void OnMouseUp()
    {       
         GetComponent<Rigidbody2D>().isKinematic = true;
         MoveToGrid();
    }

}
