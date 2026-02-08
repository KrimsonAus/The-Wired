using UnityEngine;
using UnityEngine.SceneManagement;

public class CamController : MonoBehaviour
{
    public float speed;
    public float rotSpeed;

    private GameObject cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed + transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*rotSpeed*Time.deltaTime, 0));
        cam.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y")*rotSpeed*Time.deltaTime, 0, 0));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Place");
        }
    }
}