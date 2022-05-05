using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogMaker : MonoBehaviour
{

    private string logFilePath;

    private float timeTaken = 0.0f;

    private StreamWriter sw;

    //ints to track number of shots for accuracy calculation
    private float autoTotal;
    private float autoHit;
    private float autoMiss;

    private float semiTotal;
    private float semiHit;
    private float semiMiss;

    private float burstTotal;
    private float burstHit;
    private float burstMiss;


    // Start is called before the first frame update
    void Start()
    {
        string testDate = System.DateTime.Today.Day + " " + System.DateTime.Today.Month + " " + System.DateTime.Today.Year;

        logFilePath = Application.persistentDataPath + "/testLog " + testDate + ".txt";

        if (File.Exists(logFilePath))
        {
            sw = new StreamWriter(File.Open(logFilePath, FileMode.Append));
        }
        else
        {
            sw = new StreamWriter(File.Open(logFilePath, FileMode.Create));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;
    }

    //save time and accuracy statistics to file
    void saveFile()
    {

        float fullAutoAccuracy = (autoMiss - autoHit) / autoTotal;
        float semiAutoAccuracy = (semiMiss - semiHit) / semiTotal;
        float burstAccuracy = (burstMiss - burstHit) / burstTotal;

        Debug.Log("Saving Test Log to: " + logFilePath);

        string info = System.DateTime.Now.TimeOfDay + " Time Taken: " + timeTaken + " seconds" + 
            " Assault Rifle Accuracy: " + fullAutoAccuracy + 
            " Semi-Auto Accuracy: " + semiAutoAccuracy + 
            " Burst-Rifle Accuracy: " + burstAccuracy;

        sw.WriteLine(info);
        sw.WriteLine();
        sw.Close();

        
    }

    //increases number of hits and total shots fired for each weapon
    public void addHit(string weapon)
    {
        switch (weapon)
        {
            case "fullAuto":
                autoHit++;
                autoTotal++;
                break;

            case "semiAuto":
                semiHit++;
                semiTotal++;
                break;

            case "burst":
                burstHit++;
                burstTotal++;
                break;
        }
    }

    //increases number of misses and total shots fired for each weapon
    public void addMiss(string weapon)
    {
        switch (weapon)
        {
            case "fullAuto":
                autoMiss++;
                autoTotal++;
                break;

            case "semiAuto":
                semiMiss++;
                semiTotal++;
                break;

            case "burst":
                burstMiss++;
                burstTotal++;
                break;
        }
    }
}
