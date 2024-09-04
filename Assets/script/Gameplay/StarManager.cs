using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;

    public Image[] starImages; 
    private int starPoints = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddStarPoint()
    {
        starPoints++;
        UpdateStars();
    }

    private void UpdateStars()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            Color color = starImages[i].color;

            if (i < starPoints)
            {
                color.a = 1f;
            }
            else
            {
                color.a = 0.5f; 
            }

            starImages[i].color = color;
        }
    }
}
