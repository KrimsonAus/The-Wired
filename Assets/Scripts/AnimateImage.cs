using UnityEngine;
using UnityEngine.UI;

public class AnimateImage : MonoBehaviour
{
    public Texture2D[] textures;
    public float speed;
    public RawImage image;

    private float timer;

    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= speed)
        {
            timer = 0;
            index++;
            if (index >= textures.Length)
            {
                index = 0;
            }
            image.texture = textures[index];
        }
    }
}
