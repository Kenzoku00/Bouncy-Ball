using UnityEngine;
using Cinemachine;

public class CameraControlAndroid : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // Referensi ke kamera bebas Cinemachine
    [SerializeField] private float zoomSpeed = 0.05f;            // Kecepatan zoom kamera (disesuaikan untuk sentuhan)
    [SerializeField] private float minFOV = 20f;                 // Batas minimum Field of View (FOV)
    [SerializeField] private float maxFOV = 60f;                 // Batas maksimum Field of View (FOV)
    [SerializeField] private float touchSensitivityX = 0.05f;    // Sensitivitas sentuhan untuk gerakan horizontal (kanan/kiri) yang lebih lambat
    [SerializeField] private float touchSensitivityY = 0.025f;   // Sensitivitas sentuhan untuk gerakan vertikal (atas/bawah) yang lebih lambat
    [SerializeField] private float inertiaFactor = 0.5f;         // Faktor inersia untuk pergerakan kamera yang lebih berat

    private Vector2 lastTouchPosition;                           // Posisi sentuhan terakhir
    private Vector2 currentVelocity;                             // Kecepatan gerakan kamera saat ini
    private bool isTouching = false;                             // Status apakah layar sedang disentuh

    void Update()
    {
        if (freeLookCamera.enabled)
        {
            HandleZoom();
            HandleTouchInput();
            ApplyInertia(); // Terapkan efek inersia setelah input sentuhan selesai
        }
    }

    private void HandleZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float currentFOV = freeLookCamera.m_Lens.FieldOfView;
            float newFOV = Mathf.Clamp(currentFOV + deltaMagnitudeDiff * zoomSpeed, minFOV, maxFOV);
            freeLookCamera.m_Lens.FieldOfView = newFOV;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isTouching = true;
                currentVelocity = Vector2.zero; // Reset kecepatan saat mulai menyentuh
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 touchDelta = touch.position - lastTouchPosition;

                // Update sumbu X dan Y dari kamera bebas dengan sensitivitas yang telah diatur
                freeLookCamera.m_XAxis.Value += touchDelta.x * touchSensitivityX;
                freeLookCamera.m_YAxis.Value -= touchDelta.y * touchSensitivityY; // Inversi untuk gerakan atas/bawah

                // Update kecepatan saat ini berdasarkan gerakan
                currentVelocity = touchDelta * 0.1f; // Faktor pengali untuk kontrol inersia

                lastTouchPosition = touch.position; // Update posisi sentuhan terakhir
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }

    private void ApplyInertia()
    {
        if (!isTouching && currentVelocity != Vector2.zero)
        {
            // Terapkan inersia pada gerakan kamera
            freeLookCamera.m_XAxis.Value += currentVelocity.x * touchSensitivityX;
            freeLookCamera.m_YAxis.Value -= currentVelocity.y * touchSensitivityY;

            // Kurangi kecepatan secara bertahap
            currentVelocity *= inertiaFactor;

            // Jika kecepatan sangat kecil, hentikan pergerakan
            if (currentVelocity.magnitude < 0.01f)
            {
                currentVelocity = Vector2.zero;
            }
        }
    }
}
