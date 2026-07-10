using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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

    [SerializeField] private TextMeshProUGUI timerText;
    private float survivalTime;
    public float SurvivalTime => survivalTime;

    [SerializeField] private TextMeshProUGUI survivalTimeText;

    [SerializeField] private TextMeshProUGUI killText;

    private void Update()
    {
        UpdateTimer();
    }

    private void Awake()
    {
        Instance = this;

        survivalTime = 0f;

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
        PlayerEventBus.OnKillCountChanged += UpdateKills;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnLevelUp -= OpenUpgradePanel;
        PlayerEventBus.OnPlayerDeath -= OpenGameOverPanel;

        PlayerEventBus.OnExpChanged -= UpdateExp;
        PlayerEventBus.OnHealthChanged -= UpdateHealth;
        PlayerEventBus.OnLevelChanged -= UpdateLevel;
        PlayerEventBus.OnCoinChange -= UpdateCoins;
        PlayerEventBus.OnKillCountChanged -= UpdateKills;

    }

    void UpdateTimer()
    {
        survivalTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);

        if (timerText != null)
        {
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);

        return $"{minutes:00}:{seconds:00}";
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

        if (survivalTimeText != null)
        {
            survivalTimeText.text = "You survived: " + GetFormattedTime();
        }

        if (killText != null)
        {
            killText.text =
                "Enemies killed: " + KillManager.instance.KillCount;
        }

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
    void UpdateKills(int kills)
    {
        if (killText != null)
        {
            killText.text = "Kills: " + kills;
        }
    }
}
