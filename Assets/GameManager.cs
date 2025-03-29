using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int maxHits = 5; // Oyuncunun kaç hakkı var
    private int currentHits = 0; // Şu ana kadar kaç vuruş oldu

    public TextMeshProUGUI countdownText; // UI geri sayım yazısı
    public GameObject gameOverUI; // Game Over UI paneli

    private void Start()
    {
        UpdateCountdownUI();
        gameOverUI.SetActive(false); // Başlangıçta gizli
    }

    public void RegisterHit()
    {
        currentHits++; // Vuruş sayısını artır
        UpdateCountdownUI();

        if (currentHits >= maxHits)
        {
            GameOver();
        }
    }

    private void UpdateCountdownUI()
    {
        if (countdownText != null)
        {
            countdownText.text = "Remaining: " + (maxHits - currentHits);
        }
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true); // Game Over UI aç
        Time.timeScale = 0; // Oyunu durdur
    }

    

    public void RestartButton()
    {
        
    }
}