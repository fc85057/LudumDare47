using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Slider healthSlider;
    [SerializeField] Text scoreText;

    void Start()
    {
        PlayerController.OnHealthChanged += SetHealth;
        GameManager.OnScoreChanged += SetScore;
    }

    void SetHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void SetScore(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }

}
