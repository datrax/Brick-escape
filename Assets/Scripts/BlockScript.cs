using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{

    private bool win = false;
    public string codeName = "";
    public bool solving = false;
    public int solvX;
    public int solvY;
    // Use this for initialization
    void Start ()
	{
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private Vector3 oldpos;

    void ShowCongratulationMessage()
    {
        //TODO
    }
	// Update is called once per frame
	void Update ()
	{
	    if (win)
	    {
            transform.localPosition = (new Vector2(transform.localPosition.x + Time.deltaTime*100, transform.localPosition.y));
	        if (transform.localPosition.x > 245)
	        {
	            win = !win;
	            ShowCongratulationMessage();
	        };

	    }
	    if (solving)
	    {
	        var pos = GetPosition(solvX, solvY);
	        if (pos  == transform.localPosition)
	        {
                solving = false;
                MoveToGrid();
                if (!win)
                    GameObject.Find("Solver").GetComponent<SolveThePuzzle>().KeepSolving();
                return;
            }
	        
            var minX = transform.localPosition.x;
            var maxX = transform.localPosition.x;
            var minY = transform.localPosition.y;
            var maxY = transform.localPosition.y;
            float movX = Time.deltaTime * 100;
            if (pos.x - transform.localPosition.x < 0)
	        {
	            movX *= -1;
	            minX = pos.x;
	        }
            else
            {
                maxX = pos.x;
            }
	        float movY = Time.deltaTime * 100;
	        if (pos.y - transform.localPosition.y > 0)
	        {
	            movY *= -1;
                maxY = pos.y;
            }
	        else
	        {
	            minY = pos.y;
	        }
	        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x  + movX, minX, maxX),
               Mathf.Clamp(transform.localPosition.y - movY, minY,maxY));      
        }
	}

    public void StartMovingTo(int x,int y)
    {
        solvX = x;
        solvY = y;
        solving = true;
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


    void OnMouseDrag()
    {
       
        if (GameObject.Find("Solver").GetComponent<SolveThePuzzle>().solving)return;
        Vector2 pos;
        GameObject myCanvas = transform.parent.gameObject;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, Camera.main, out pos);
        var poss = myCanvas.transform.TransformPoint(pos);
      //  if(poss.x - oldpos.x>0)return;

        oldpos =poss;
        GetComponent<Rigidbody2D>()
            .MovePosition((name[0] == 'v')
                ? new Vector2(transform.position.x, poss.y)
                : new Vector2(poss.x, transform.position.y));
    }

 /*   void checkMovement(int pos, string direction)
    {
        
    }*/
    private Vector3 GetLocalPosition()
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

        float cellX = (7f - (135 - (t.localPosition.x - (46 * xc))) / cellSize);
        float cellY = (1 + (160 - (t.localPosition.y + (46 * yc))) / cellSize);
        print((160 - (t.localPosition.y - (46 * yc))) / cellSize);
        return new Vector3((float)Math.Round(cellX, 0),(float) Math.Round(cellY, 0));
    }
    public void MoveToGrid()
    {
        var vertical = (name[0] == 'v');
   
        var localPos = GetLocalPosition();
        int cellX = Convert.ToInt32(localPos.x);
        int cellY = Convert.ToInt32(localPos.y);
        //костыль
        var pos = GetPosition(cellX,cellY);
        codeName = name.Substring(0,2) + cellX.ToString() + cellY.ToString();
        if (!vertical)
            transform.localPosition = new Vector3(pos.x, transform.localPosition.y);
        else
            transform.localPosition = new Vector3(transform.localPosition.x, pos.y);

        if (name[0] == 'h' && cellX >= 5)
        {
            print("azazza");
            win = true;
        }
    }
    void OnMouseDown()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        oldpos = transform.position;
    }
    void OnMouseUp()
    {
        MoveToGrid();
        GetComponent<Rigidbody2D>().isKinematic = true;
        oldpos = Vector3.zero;
    }

}
