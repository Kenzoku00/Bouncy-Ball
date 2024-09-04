using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class skinManager : MonoBehaviour
{
    public static skinManager instance;
    [HideInInspector]public skinInformation skinInformation;
    private string loadFileName = "skinData.json";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadFromJson();
    }
    void LoadFromJson()
    {
        string path = Path.Combine(Application.dataPath, loadFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            skinInformation = JsonUtility.FromJson<skinInformation>(json);
        }
    }

    public skin get_information(int id) 
    {
        foreach(skin obj in skinInformation.skins)
        {
            if (obj.ID == id)
            {
                return obj;
            }
        }
        return null; 
    }

}
