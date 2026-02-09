using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldInterpreter : MonoBehaviour
{
    public string placeName;
    public GameObject loadScreen;
    public Slider loader;
    public TextMeshProUGUI loadedNumText;

    private string path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartLoadPlace;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartLoadPlace;
    }

    // Update is called once per frame
    void Update()
    {
    }

    async void StartLoadPlace(Scene scene, LoadSceneMode mode)
    {
        string fileName = placeName + ".place";
        path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            float time = DateTime.Now.Millisecond;
            await LoadPlace(path);
            //print("loaded in " + (DateTime.Now.Millisecond - time) + "ms.");
            loadScreen.SetActive(false);
        }
        else
        {
            Debug.LogError("File not found at path: " + path);
        }
    }


    async Awaitable<bool> LoadPlace(string path)
    {
        int index = 0;
        string[] lines = File.ReadAllLines(path);
        GameObject obj;
        obj = new GameObject();
        loader.maxValue = lines.Length;
        foreach (string line in lines)
        {
            index++;
            string[] words = line.Split(" ");
            //print(words[0]);
            if (words[0].ToCharArray()[0] == '/' && words[0].ToCharArray()[1] == '/')
            {
                //print("NOTE");
                continue;
            }

            switch (words[0])
            {
                case "plane":
                    obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    break;
                case "sphere":
                    obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;
                case "cylinder":
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    break;
                case "cube":
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                case "capsule":
                    obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    break;
                case "quad":
                    obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    break;
                case "cam":
                    Camera.main.transform.parent.transform.position = new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3]));
                    break;
                case "text":
                    obj = new GameObject("text", typeof(TextMeshPro));
                    string txt = words[13].Replace(';', ' ');
                    txt = txt.TrimStart(':');
                    obj.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
                    obj.GetComponent<TextMeshPro>().fontSize = float.Parse(words[14]);
                    obj.GetComponent<TextMeshPro>().color = new Color(float.Parse(words[10]), float.Parse(words[11]), float.Parse(words[12]));
                    obj.GetComponent<TextMeshPro>().text = txt;

                    //obj.GetComponent<MeshRenderer>().material =
                    //new Material(Shader.Find("TextMeshPro/Mobile/Distance Field"));
                    break;
                case "settings":
                    //print(bool.Parse(words[1]));
                    if (bool.Parse(words[1]))
                    {
                        Camera.main.transform.parent.GetComponent<CamController>().enabled = true;
                        Camera.main.transform.parent.GetComponent<CamController>().speed = float.Parse(words[3]);
                    }

                    if (bool.Parse(words[2]))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        Camera.main.transform.parent.GetComponent<CamController>().rotSpeed = 160;
                    }

                    break;
                case "radio":
                    FindFirstObjectByType<Radio>().url = words[7];
                    obj.transform.position =
                        new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3]));
                    obj.transform.eulerAngles = new Vector3(float.Parse(words[4]), float.Parse(words[5]), float.Parse(words[6]));
                    break;
            }

            if (words[0] != "settings" || words[0] != "radio")
            {
                obj.transform.position =
                    new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3]));

                if (words.Length > 4)
                {
                    obj.transform.eulerAngles = new Vector3(float.Parse(words[4]), float.Parse(words[5]), float.Parse(words[6]));

                    if (words.Length > 7)
                    {
                        obj.transform.localScale = new Vector3(float.Parse(words[7]), float.Parse(words[8]), float.Parse(words[9]));

                        if (words[0] != "text")
                        {
                            obj.GetComponent<MeshRenderer>().material =
                                new Material(Shader.Find("Universal Render Pipeline/Lit"));
                            obj.GetComponent<MeshRenderer>().material.color =
                                new Color(float.Parse(words[10]), float.Parse(words[11]), float.Parse(words[12]));

                            if (words.Length > 13)
                            {
                                switch (words[13])
                                {
                                    case ("rot"):
                                        obj.AddComponent<Translate>().rotate = true;
                                        obj.GetComponent<Translate>().speed = float.Parse(words[14]);
                                        obj.GetComponent<Translate>().axis = int.Parse(words[15]);
                                        break;
                                }
                            }
                        }
                    }
                }


                Debug.Log(line);
            }

            //print("loaded obj [" + index +"]");
            loader.value = index;
            loadedNumText.text = index + "/" + loader.maxValue;
            if (index % 100 == 0)
            {
                await Task.Yield();
            }
        }

        return true;
    }
}