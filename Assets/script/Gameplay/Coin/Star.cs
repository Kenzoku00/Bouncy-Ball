using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("unit"))
        {
            ScoreManager.Instance.IncrementScore();
            StarManager.Instance.AddStarPoint(); 
            gameObject.SetActive(false);
        }
    }
}
