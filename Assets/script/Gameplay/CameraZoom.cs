using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera; // Referensi ke kamera Cinemachine
    [SerializeField] private float zoomSpeed = 0.05f;            // Kecepatan zoom kamera (disesuaikan untuk sentuhan)
    [SerializeField] private float minFOV = 20f;                 // Batas minimum Field of View (FOV)
    [SerializeField] private float maxFOV = 60f;                 // Batas maksimum Field of View (FOV)

    void Update()
    {
        HandleTouchZoom();
    }

    private void HandleTouchZoom()
    {
        if (Input.touchCount == 2) // Deteksi dua jari menyentuh layar
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Hitung jarak antara dua posisi sentuhan sekarang dan sebelumnya
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Perbedaan antara jarak sekarang dan sebelumnya
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Perbarui Field of View (FOV) berdasarkan perbedaan jarak sentuhan
            float currentFOV = freeLookCamera.m_Lens.FieldOfView;
            float newFOV = Mathf.Clamp(currentFOV + deltaMagnitudeDiff * zoomSpeed, minFOV, maxFOV);

            freeLookCamera.m_Lens.FieldOfView = newFOV;
        }
    }
}
