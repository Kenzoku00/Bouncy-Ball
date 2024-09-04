using System;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public Material skybox;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }

    private void Start()
    {
        int hour = DateTime.Now.Hour;
        Color color = Color.black;
        Debug.Log(hour);

        switch (hour)
        {
            case 1:
                color = new Color(28f / 255f, 29f / 255f, 41f / 255f);
                break;
            case 2:
                color = new Color(35f / 255f, 37f / 255f, 51f / 255f);
                break;
            case 3:
                color = new Color(44f / 255f, 45f / 255f, 64f / 255f);
                break;
            case 4:
                color = new Color(53f / 255f, 54f / 255f, 77f / 255f);
                break;
            case 5:
                color = new Color(63f / 255f, 64f / 255f, 90f / 255f);
                break;
            case 6:
                color = new Color(72f / 255f, 73f / 255f, 102f / 255f);
                break;
            case 7:
                color = new Color(102f / 255f, 103f / 255f, 130f / 255f);
                break;
            case 8:
                color = new Color(106f / 255f, 107f / 255f, 130f / 255f);
                break;
            case 9:
                color = new Color(126f / 255f, 126f / 255f, 154f / 255f);
                break;
            case 10:
                color = new Color(133f / 255f, 133f / 255f, 163f / 255f);
                break;
            case 11:
                color = new Color(142f / 255f, 142f / 255f, 163f / 255f);
                break;
            case 12:
                color = new Color(162f / 255f, 162f / 255f, 163f / 255f);
                break;
            case 13:
                color = new Color(163f / 255f, 161f / 255f, 159f / 255f);
                break;
            case 14:
                color = new Color(163f / 255f, 156f / 255f, 150f / 255f);
                break;
            case 15:
                color = new Color(163f / 255f, 150f / 255f, 141f / 255f);
                break;
            case 16:
                color = new Color(163f / 255f, 138f / 255f, 123f / 255f);
                break;
            case 17:
                color = new Color(129f / 255f, 100f / 255f, 93f / 255f);
                break;
            case 18:
                color = new Color(71f / 255f, 73f / 255f, 103f / 255f);
                break;
            case 19:
                color = new Color(59f / 255f, 62f / 255f, 87f / 255f);
                break;
            case 20:
                color = new Color(48f / 255f, 50f / 255f, 71f / 255f);
                break;
            case 21:
                color = new Color(41f / 255f, 43f / 255f, 60f / 255f);
                break;
            case 22:
                color = new Color(36f / 255f, 37f / 255f, 53f / 255f);
                break;
            case 23:
                color = new Color(33f / 255f, 35f / 255f, 53f / 255f);
                break;
            case 0:
                color = new Color(30f / 255f, 32f / 255f, 50f / 255f);
                break;
        }

        skybox.SetColor("_Tint", color);
        DynamicGI.UpdateEnvironment();
    }
}
