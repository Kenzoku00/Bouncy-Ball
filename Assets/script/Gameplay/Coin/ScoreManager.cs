using UnityEngine;
using TMPro; // Tambahkan namespace TMP

public class ScoreManager : MonoBehaviour
{
    // Menghubungkan TextMeshPro melalui Inspector
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Menyimpan skor saat ini
    private int score;

    private void Awake()
    {
        // Pastikan ada instance tunggal dari ScoreManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ScoreManager Instance { get; private set; }

    private void Start()
    {
        // Atur skor awal dan perbarui tampilan
        score = 0;
        UpdateScoreText();
    }

    // Menambah skor dan memperbarui tampilan
    public void IncrementScore()
    {
        score++;
        UpdateScoreText();
    }

    // Memperbarui teks skor di UI
    private void UpdateScoreText()
    {
        scoreText.text = "Konting: " + score;
    }
}
