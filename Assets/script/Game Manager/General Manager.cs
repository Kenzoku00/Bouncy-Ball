using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class generalManager : MonoBehaviour
{
    public static generalManager instance;
    public playerInformation playerInformation;

    void Awake()
    {
        Application.targetFrameRate = 240;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        playerInformation = new playerInformation();
        generalManager.saveManager.load();
    }

    public static class currency
    {
        public static class star
        {
            public static bool spend(int amount)
            {
                if (instance.playerInformation.currency.star >= amount)
                {
                    instance.playerInformation.currency.star -= amount;
                    refresh();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool add(int amount) 
            {
                if (instance.playerInformation.currency.star >= amount)
                {
                    instance.playerInformation.currency.star += amount;
                    refresh() ;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static void refresh()
            {
                TextMeshProUGUI starText = GameObject.FindGameObjectWithTag("Star").GetComponent<TextMeshProUGUI>();
                starText.text = instance.playerInformation.currency.star.ToString();
            }
        }

    }

    public static class inventory
    {
        public static class skin
        {
            public static void addItem(int id)
            {
                int[] currentArray = instance.playerInformation.inventory.ownedSkinID;

                int newSize = currentArray.Length + 1;
                int[] newArray = new int[newSize];

                Array.Copy(currentArray, newArray, currentArray.Length);

                newArray[newSize - 1] = id;

                instance.playerInformation.inventory.ownedSkinID = newArray;
            }


            public static bool removeItem(int id)
            {
                for (int i = 0; i < instance.playerInformation.inventory.ownedSkinID.Length; i++)
                {
                    if (instance.playerInformation.inventory.ownedSkinID[i] == id)
                    {
                        instance.playerInformation.inventory.ownedSkinID = RemoveAt(instance.playerInformation.inventory.ownedSkinID, id);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            }

            public static bool equipitem(int id)
            {
                if (instance.playerInformation.inventory.ownedSkinID.Contains(id))
                {
                    instance.playerInformation.inventory.equippedSkinID = id;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static T[] RemoveAt<T>(T[] source, int index)
        {
            if (index < 0 || index >= source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            T[] result = new T[source.Length - 1];
            if (index > 0)
            {
                Array.Copy(source, 0, result, 0, index);
            }
            if (index < source.Length - 1)
            {
                Array.Copy(source, index + 1, result, index, source.Length - index - 1);
            }

            return result;
        }

    }

    public static class saveManager
    {
        public static void save()
        {
            string json = JsonUtility.ToJson(generalManager.instance.playerInformation);
            string path = Application.persistentDataPath + "/data.json";
            File.WriteAllText(path, json);
        }
        public static void load() 
        {
            try
            {
                setUpData();

                string path = Application.persistentDataPath + "/data.json";
                Debug.Log(path);
                string file = File.ReadAllText(path);
                instance.playerInformation = JsonUtility.FromJson<playerInformation>(file);
                generalManager.currency.star.refresh();
            }
            catch(Exception e) 
            {
                save();
            }

        }

        private static void setUpData()
        {
            playerInformation player = new playerInformation();

            // Set Up Profile
            player.profile.name = "cache";

            // Set Up Currency
            player.currency.star = 0;

            // Set Up Inventory
            player.inventory.equippedSkinID = 0;
            player.inventory.ownedSkinID = new int[0];
        }
    }

    private void OnApplicationQuit()
    {
        generalManager.saveManager.save();
    }
}
