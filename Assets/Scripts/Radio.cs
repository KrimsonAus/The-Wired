using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Radio : MonoBehaviour
{
    public AudioSource audioSource;

    public string url;
    public AudioType audioType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DownloadAndPlayAudio(url));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DownloadAndPlayAudio(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType)) // Specify the correct AudioType
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                print(www.url);
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
    
}
