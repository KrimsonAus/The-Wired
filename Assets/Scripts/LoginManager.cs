using System.Collections;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    
    UIManager uiManager;

    //no input, failed login
    public AudioClip[] audioClips;

    [Header("DEBUG")] 
    public string TEST_USERNAME;
    public string TEST_PASSWORD;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        if (uiManager == null)
        {
            uiManager = gameObject.AddComponent<UIManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TryLogin()
    {
        StartCoroutine(QueryCredentials());
    }

    IEnumerator QueryCredentials()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        if (username == "" || password == "")
        {
            uiManager.PlaySound(audioClips[0]);
            return null;
        }

        if (username != TEST_USERNAME || password != TEST_PASSWORD)
        {
            uiManager.PlaySound(audioClips[1]);
        }

        if (username == TEST_USERNAME && password == TEST_PASSWORD)
        {
            uiManager.ChangeScene("Main");
        }

        return null;
    }
}
