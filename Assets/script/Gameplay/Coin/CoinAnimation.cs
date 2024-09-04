using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    // Kecepatan rotasi koin (derajat per detik)
    public float rotationSpeed = 100f;
    
    // Kecepatan gerakan naik turun
    public float bobbingSpeed = 1f;
    
    // Besar gerakan naik turun
    public float bobbingAmount = 0.1f;
    
    // Posisi awal koin di sumbu Y
    private float startY;

    private void Start()
    {
        // Simpan posisi awal koin di sumbu Y
        startY = transform.position.y;
    }

    private void Update()
    {
        // Rotasi koin di sumbu Y
        // Vector3.up menunjukkan sumbu Y, dan kita memutar dengan kecepatan 'rotationSpeed'
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Gerakan naik turun menggunakan fungsi sinus
        // Mathf.Sin menghasilkan nilai dari -1 hingga 1, sehingga kita kalikan dengan 'bobbingAmount' untuk menentukan tinggi gerakan
        float newY = startY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // Perbarui posisi koin di sumbu Y dengan nilai baru
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
