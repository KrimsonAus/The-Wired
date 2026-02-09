using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    public GameObject panelHold;
    public GameObject objectButtonPrefab;
    
    public GameObject selectedObject;
    public GameObject axisPrefab;

    [Header("Object Manager")]
    public GameObject objectManagerPanel;
    private GameObject lastButton;

    public TMP_InputField positionInput;
    public TMP_InputField rotationInput;
    public TMP_InputField scaleInput;
    public TMP_InputField colourInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddObject()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = "CUBE";
        obj.AddComponent<CreateObject>().axisPrefab = axisPrefab;
        GameObject button = Instantiate(objectButtonPrefab, panelHold.transform);
        button.GetComponent<ObjectButton>().linkedObject = obj;
    }

    public void SwapActiveObject(GameObject newObj)
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<CreateObject>().SwapAxis(false);
        }
        selectedObject = newObj;
        newObj.GetComponent<CreateObject>().SwapAxis(true);
    }

    public void OpenObjectManagerPanel(GameObject linkedObj, GameObject button)
    {
        selectedObject = linkedObj;
        
        positionInput.text = selectedObject.transform.position.x + "," + selectedObject.transform.position.y + "," + selectedObject.transform.position.z;
        rotationInput.text = selectedObject.transform.eulerAngles.x + "," + selectedObject.transform.eulerAngles.y + "," + selectedObject.transform.eulerAngles.z;
        scaleInput.text = selectedObject.transform.localScale.x + "," + selectedObject.transform.localScale.y + "," + selectedObject.transform.localScale.z;
        
        objectManagerPanel.SetActive(true);
        lastButton = button;
    }

    public void OnObjectManagerTypeChange(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        GameObject newObj = new GameObject();
        
        print("Changed to " + index);
        switch (index)
        {
            case 0:
                newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case 1:
                newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case 2:
                newObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                break;
            case 3:
                newObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
            case 4:
                newObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
            case 5:
                newObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                break;
            case 6:
                break;
            case 7:
                break;
        }

        newObj.AddComponent<CreateObject>();
        newObj.GetComponent<CreateObject>().axis = selectedObject.GetComponent<CreateObject>().axis;
        newObj.GetComponent<CreateObject>().axisPrefab = selectedObject.GetComponent<CreateObject>().axisPrefab;
        Destroy(selectedObject);
        selectedObject = newObj;
        lastButton.GetComponent<ObjectButton>().linkedObject = selectedObject;
        objectManagerPanel.SetActive(false);
    }

    public void ChangeObjPosition()
    {
        string[] pos =  positionInput.text.Split(",");
        print("pos array is "+pos.Length);
        float[] posXYZ = new float[3];
        for (int i = 0; i < 3; i++)
        {
            posXYZ[i] = float.Parse(pos[i]);
        }
        selectedObject.transform.position = new Vector3(posXYZ[0], posXYZ[1], posXYZ[2]);
    }
    public void ChangeObjRotation()
    {
        string[] pos =  rotationInput.text.Split(",");
        print("rot array is "+pos.Length);
        float[] posXYZ = new float[3];
        for (int i = 0; i < 3; i++)
        {
            posXYZ[i] = float.Parse(pos[i]);
        }
        selectedObject.transform.eulerAngles = new Vector3(posXYZ[0], posXYZ[1], posXYZ[2]);
    }
    
    public void ChangeObjScale()
    {
        string[] pos =  scaleInput.text.Split(",");
        print("scale array is "+pos.Length);
        float[] posXYZ = new float[3];
        for (int i = 0; i < 3; i++)
        {
            posXYZ[i] = float.Parse(pos[i]);
        }
        selectedObject.transform.localScale = new Vector3(posXYZ[0], posXYZ[1], posXYZ[2]);
    }
}
