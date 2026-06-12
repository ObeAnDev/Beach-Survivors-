using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject upgradePanel;
    public GameObject gameOverPanel;

    [Header("UI")]
    public Slider expSlider;
    public Slider healthSlider;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        Instance = this;

        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        PlayerEventBus.OnLevelUp += OpenUpgradePanel;
        PlayerEventBus.OnPlayerDeath += OpenGameOverPanel;

        PlayerEventBus.OnExpChanged += UpdateExp;
        PlayerEventBus.OnHealthChanged += UpdateHealth;
        PlayerEventBus.OnLevelChanged += UpdateLevel;
        PlayerEventBus.OnCoinChange += UpdateCoins;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnLevelUp -= OpenUpgradePanel;
        PlayerEventBus.OnPlayerDeath -= OpenGameOverPanel;

        PlayerEventBus.OnExpChanged -= UpdateExp;
        PlayerEventBus.OnHealthChanged -= UpdateHealth;
        PlayerEventBus.OnLevelChanged -= UpdateLevel;
        PlayerEventBus.OnCoinChange -= UpdateCoins;
    }

    public void OpenUpgradePanel()
    {
        Time.timeScale = 0f;

        upgradePanel.SetActive(true);

        upgradePanel.GetComponent<UpgradePanelUI>()
            .GenerateCards();
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OpenGameOverPanel()
    {
        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void UpdateExp(float value)
    {
        expSlider.value = value;
    }

    void UpdateHealth(float value)
    {
        healthSlider.value = value;
    }

    void UpdateLevel(int level)
    {
        levelText.text = "Level " + level;
    }

    void UpdateCoins(int coins)
    {
        coinText.text = coins.ToString();
    }
}
