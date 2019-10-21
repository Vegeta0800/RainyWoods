using System;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public KeyCode hotkey = KeyCode.F12;
    public string directory = "Screenshots";
    [Range(1, 16)] public int scale = 1;

    private string StorePath
    {
        get { return Path.Combine(Application.dataPath, directory); }
    }

    private string FullPath
    {
        get { return Path.Combine(StorePath, DateTime.Now.Ticks.ToString() + ".png"); }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!Directory.Exists(StorePath))
            Directory.CreateDirectory(StorePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hotkey))
            ScreenCapture.CaptureScreenshot(FullPath, scale);
    }
}
