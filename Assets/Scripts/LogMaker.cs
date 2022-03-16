using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class LogMaker : MonoBehaviour
{

    private string logFilePath;

    private float timeTaken = 0.0f;

    private FileStream fs;

    // Start is called before the first frame update
    void Start()
    {
        string testDate = System.DateTime.Today.Day + " " + System.DateTime.Today.Month + " " + System.DateTime.Today.Year;

        logFilePath = Application.persistentDataPath + "/testLog " + testDate + ".txt";

        fs = File.Open(logFilePath, FileMode.OpenOrCreate);
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;
    }

    //save time to file
    void saveFile()
    {
        Debug.Log("Saving Test Log to: " + logFilePath);
        byte[] info = new UTF8Encoding(true).GetBytes(System.DateTime.Now.TimeOfDay + " Time Taken: " + timeTaken + " seconds" + Environment.NewLine);
        fs.Write(info, (int)fs.Length, info.Length);
        fs.Close();
    }
}
