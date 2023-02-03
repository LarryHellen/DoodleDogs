using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScroll : MonoBehaviour
{
    public GameObject background;
    public GameObject settingsMenu;
    public Vector3 startPoint = new Vector3(0, 0, 0);
    public int b = 0, c = 0;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (!settingsMenu.activeSelf)
        {
            startPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Debug.Log("e");
        }
    }
    private void OnMouseUp()
    {
        startPoint = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (startPoint != new Vector3(0, 0, 0))
        {
            background.transform.position -= new Vector3(0, (Input.mousePosition.y - startPoint.y)/2, 0);
            startPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if (background.transform.position.y < b)
        {
            background.transform.position = new Vector3(background.transform.position.x, b, background.transform.position.z);
        }
        if (background.transform.position.y > c)
        {
            background.transform.position = new Vector3(background.transform.position.x, c, background.transform.position.z);
        }
    }
}
