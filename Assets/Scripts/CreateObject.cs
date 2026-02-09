using System;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject axisPrefab;

    public GameObject[] axis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject axisObj = Instantiate(axisPrefab, transform);
        axis = new GameObject[3];
        axis[0] = axisObj.transform.GetChild(0).gameObject;
        axis[1] = axisObj.transform.GetChild(1).gameObject;
        axis[2] = axisObj.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        FindFirstObjectByType<CreateManager>().SwapActiveObject(this.gameObject);
    }

    public void SwapAxis(bool isActive)
    {
        foreach (GameObject x in axis)
        {
            x.SetActive(isActive);
        }
    }
}