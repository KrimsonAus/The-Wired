using UnityEngine;

public class Translate : MonoBehaviour
{
    public bool rotate = false;
    public bool translate = false;

    public float speed;

    public int axis;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            switch (axis)
            {
                case 0:
                    transform.Rotate(speed * Time.deltaTime, 0, 0);
                    break;
                case 1:
                    transform.Rotate(0, speed * Time.deltaTime, 0);
                    break;
                case 2:
                    transform.Rotate(0, 0, speed * Time.deltaTime);
                    break;
            }
            
        }
    }
}
