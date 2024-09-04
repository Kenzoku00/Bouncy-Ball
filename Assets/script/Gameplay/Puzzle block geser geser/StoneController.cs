using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField] private int maxMoves = 3; // Maksimal gerakan sebelum berbalik arah
    [SerializeField] private float moveDistance = 1f; // Jarak yang ditempuh setiap kali bergerak
    [SerializeField] private bool moveHorizontally = true; // Mengatur apakah bergerak di sumbu X (true) atau sumbu Z (false)
    [SerializeField] private bool movingRightOrForward = true; // Mengatur apakah bergerak ke kanan/depan (true) atau ke kiri/belakang (false)

    private int currentMoves = 0; // Jumlah gerakan saat ini
    private Vector3 initialPosition; // Posisi awal batu
    private Vector3 targetPosition; // Posisi target batu

    private void Start()
    {
        initialPosition = transform.position; // Simpan posisi awal
        targetPosition = initialPosition; // Atur posisi target awal
    }

    private void Update()
    {
        // Deteksi input dari layar sentuh untuk Android
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && IsTouchingStone(touch.position))
            {
                MoveStone();
            }
        }
    }

    private void MoveStone()
    {
        if (currentMoves < maxMoves)
        {
            // Update posisi target sesuai arah gerakan
            float direction = movingRightOrForward ? 1f : -1f;
            if (moveHorizontally)
            {
                targetPosition += new Vector3(direction * moveDistance, 0, 0); // Gerak horizontal di sumbu X
            }
            else
            {
                targetPosition += new Vector3(0, 0, direction * moveDistance); // Gerak vertikal di sumbu Z
            }

            // Tambah jumlah gerakan yang telah dilakukan
            currentMoves++;
            Debug.Log($"Move {currentMoves}: Batu bergerak {(movingRightOrForward ? (moveHorizontally ? "ke kanan" : "ke depan") : (moveHorizontally ? "ke kiri" : "ke belakang"))}");
        }
        else
        {
            // Balik arah gerakan dan lanjutkan gerakan 1 blok ke arah yang berlawanan
            movingRightOrForward = !movingRightOrForward;
            currentMoves = 1; // Set ke 1 karena akan memulai kembali dari arah berlawanan

            // Update posisi target untuk arah baru (bergerak 1 blok ke arah berlawanan)
            float direction = movingRightOrForward ? 1f : -1f;
            if (moveHorizontally)
            {
                targetPosition += new Vector3(direction * moveDistance, 0, 0); // Gerak horizontal di sumbu X
            }
            else
            {
                targetPosition += new Vector3(0, 0, direction * moveDistance); // Gerak vertikal di sumbu Z
            }

            Debug.Log($"Batu berbalik arah: {(movingRightOrForward ? (moveHorizontally ? "ke kanan" : "ke depan") : (moveHorizontally ? "ke kiri" : "ke belakang"))}");
        }

        StartCoroutine(MoveToTargetPosition());
    }

    private System.Collections.IEnumerator MoveToTargetPosition()
    {
        // Gerakkan batu ke posisi target
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 3f);
            yield return null;
        }
        transform.position = targetPosition;
    }

    private bool IsTouchingStone(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
