using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] EnterObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter(Animator animator)
    {
        animator.SetTrigger("Enter");
        StartCoroutine(KillTimer(1.6f, EnterObjects));
    }

    IEnumerator KillTimer(float timer, GameObject[] gameObjects)
    {
        yield return new WaitForSeconds(timer);
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }
}
