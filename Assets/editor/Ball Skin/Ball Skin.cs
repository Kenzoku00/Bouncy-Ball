using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkinEditor : EditorWindow
{
    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;
    [SerializeField] private string saveFileName = "skinData.json";

    private Vector2 scrollPosition;

    [SerializeField] private skinInformation skinInfo = new skinInformation();

    [MenuItem("Window/Skin Editor")]
    public static void ShowWindow()
    {
        GetWindow<SkinEditor>("Skin Editor").Show();
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        serializedProperty = serializedObject.FindProperty("skinInfo");
        LoadFromJson(); // Load previously saved data
    }

    private void OnGUI()
    {
        GUIStyle header = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.UpperCenter,
            fontSize = 24,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white },
            padding = { top = 10, bottom = 10 }
        };

        GUIStyle sectionHeader = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            padding = { top = 0, bottom = 0 }
        };

        GUIStyle boxStyle = new GUIStyle(GUI.skin.box)
        {
            padding = new RectOffset(10, 10, 10, 10),
            margin = new RectOffset(10, 10, 10, 10)
        };

        GUILayout.Label("Skin Editor", header);

        serializedObject.Update();

        EditorGUILayout.BeginVertical(boxStyle);

        if (GUILayout.Button("Add New Skin", GUILayout.Height(30)))
        {
            skin newSkin = new skin();
            newSkin.ID = GenerateUniqueID();
            skinInfo.skins.Add(newSkin);
        }

        EditorGUILayout.Space();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < skinInfo.skins.Count; ++i)
        {
            EditorGUILayout.BeginVertical(boxStyle);

            EditorGUILayout.LabelField($"Skin {i + 1}", sectionHeader);
            skin currentSkin = skinInfo.skins[i];

            currentSkin.name = EditorGUILayout.TextField("Name", currentSkin.name);
            currentSkin.description = EditorGUILayout.TextField("Description", currentSkin.description);
            currentSkin.ID = EditorGUILayout.IntField("ID", currentSkin.ID);
            currentSkin.price = EditorGUILayout.IntField("Price", currentSkin.price);
            currentSkin.ballImage = (Image)EditorGUILayout.ObjectField("Ball Image", currentSkin.ballImage, typeof(Image), false);
            currentSkin.material = (Material)EditorGUILayout.ObjectField("Material", currentSkin.material, typeof(Material), false);

            if (GUILayout.Button("Remove Skin", GUILayout.Height(25)))
            {
                skinInfo.skins.RemoveAt(i);
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        EditorGUILayout.EndScrollView();

        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Apply", GUILayout.Height(30)))
        {
            SaveToJson();
            Debug.Log("Applied");
        }
    }

    private void SaveToJson()
    {
        string json = JsonUtility.ToJson(skinInfo, true);
        string path = Path.Combine(Application.dataPath, saveFileName);
        File.WriteAllText(path, json);
    }

    private void LoadFromJson()
    {
        string path = Path.Combine(Application.dataPath, saveFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            skinInfo = JsonUtility.FromJson<skinInformation>(json);
        }
        else
        {
            skinInfo = new skinInformation();
        }

        serializedObject = new SerializedObject(this);
        serializedProperty = serializedObject.FindProperty("skinInfo");
    }

    private int GenerateUniqueID()
    {
        // Generate a unique ID based on current time ticks.
        return (int)System.DateTime.Now.Ticks;
    }
}
