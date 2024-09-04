using UnityEngine;

public class FinishTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("unit"))
        {
            // Menonaktifkan BoxCollider pada FinishTile
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
                Debug.Log("BoxCollider pada Finish Tile dinonaktifkan.");
            }

            // Mengubah massa pada Rigidbody unit menjadi 9999
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = 9999;
                Debug.Log("Massa Rigidbody pada unit diubah menjadi 9999.");
            }
        }
    }
}
