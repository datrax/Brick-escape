using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{

    public GameObject myCanvas;
	// Use this for initialization
	void Start ()
	{
	   
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var t = GetComponent<RectTransform>();

            print(t.position.y-t.rect.height);
          //   print(t.localPosition);
      
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
        

        /* Vector2 pos;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, GetComponent<Camera>(), out pos);
         transform.position = myCanvas.transform.TransformPoint(pos);*/

        /*   CanvasScaler scaler = GetComponentInParent<CanvasScaler>();

           transform.position = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width, Input.mousePosition.y * scaler.referenceResolution.y / Screen.height);*/

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, Camera.main, out pos);
        var poss = myCanvas.transform.TransformPoint(pos);
        GetComponent<Rigidbody2D>().MovePosition(poss);

    }
    void OnMouseDown()
    {
       /* var t=Instantiate(this);
        t.transform.position = SetIn(0, 3);
        t.transform.parent = transform.parent;
        t.transform.localScale = transform.localScale;*/
    }

    void OnMouseUp()
    {
       // GetComponent<Rigidbody2D>().isKinematic = true;
    }

}
