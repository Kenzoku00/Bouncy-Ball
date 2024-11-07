using UnityEngine;

public class FinishTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("unit"))
        {
           
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
                Debug.Log("BoxCollider pada Finish Tile dinonaktifkan.");
            }

           
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = 9999;
                Debug.Log("Massa Rigidbody pada unit diubah menjadi 9999.");
            }
        }
    }
}
