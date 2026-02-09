using System;
using UnityEngine;

public class ObjectAxis : MonoBehaviour
{
    public GameObject parentObj;

    private bool hasMousePos;

    private Vector3 mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentObj = transform.parent.transform.parent.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        if (!hasMousePos)
        {
            mousePos = Input.mousePosition;
            hasMousePos = true;
            print("reg mousepos as " + mousePos);
        }

        print("mousepos is " + Input.mousePosition);
        switch (name)
        {
            case "AXIS_X":
                parentObj.transform.position += parentObj.transform.right * (Input.mousePosition.x - mousePos.x)/100 * Time.deltaTime;
                break;
            case "AXIS_Y":
                parentObj.transform.position += parentObj.transform.up * (Input.mousePosition.y - mousePos.y)/100 * Time.deltaTime;
                //parentObj.transform.position += parentObj.transform.up;
                break;
            case "AXIS_Z":
                parentObj.transform.position += parentObj.transform.forward * (Input.mousePosition.x - mousePos.y)/100 * Time.deltaTime;
                //parentObj.transform.position += parentObj.transform.forward;
                break;
        }
    }

    private void OnMouseUp()
    {
        hasMousePos = false;
    }
}
