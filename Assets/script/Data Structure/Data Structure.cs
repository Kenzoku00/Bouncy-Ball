using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInformation
{
    public profile profile = new profile();
    public currency currency = new currency();
    public inventory inventory = new inventory();
}

public class profile 
{
    public string name;
}

public class currency
{
    public int star;
}

[System.Serializable]
public class inventory
{
    public int equippedSkinID;
    public int[] ownedSkinID;
}

[System.Serializable]
public class skinInformation
{
    public List<skin> skins;

    public skinInformation()
    {
        skins = new List<skin>();
    }
}

[System.Serializable]
public class skin 
{ 
    public string name;
    public string description;
    public int ID;
    public int price;
    public Image ballImage;
    public Material material;
}

public enum graphic
{
    low,
    medium,
    high
}

public class setting
{
    public float volume;
    public graphic graphic;
}