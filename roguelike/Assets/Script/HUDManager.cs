using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public enum TypeInfo { hpBar, expBar, bossHpBar, timerText, goldText, killCountText }
    public TypeInfo type;
    public GameObject questInfoPanel;

    TextMeshProUGUI text;
    Slider slider;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        // PlayerManager hp 이벤트 구독
        if (type == TypeInfo.hpBar)
            PlayerManager.Instance.OnHpChanged += UpdateHpBar;
    }

    private void OnDestroy()
    {
        // 이벤트 해제
        if (type == TypeInfo.hpBar)
            PlayerManager.Instance.OnHpChanged -= UpdateHpBar;
    }

    // HP Bar 업데이트
    private void UpdateHpBar(float currentHp, float maxHp)
    {
        slider.value = currentHp / maxHp;
    }

    private void Update()
    {
        switch (type)
        {
            case TypeInfo.expBar:
                UpdateExpBar();
                break;

            case TypeInfo.timerText:
                UpdateTimer();
                break;

            case TypeInfo.goldText:
                UpdateGold();
                break;

            case TypeInfo.killCountText:
                UpdateKillCount();
                break;

            case TypeInfo.bossHpBar:
                
                break;
        }
    }
  
    void UpdateExpBar() 
    {
        int currentExp = PlayerManager.Instance.currentExp;
        int maxExp = PlayerManager.Instance.maxExp;
        slider.value = (float)currentExp / maxExp;
    }

    private void UpdateTimer()
    {
        float t = GameManager.Instance.gameTime;

        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);

        text.text = $"{minutes:00}:{seconds:00}";
    }

    void UpdateGold()
    {
        int currentGold = PlayerManager.Instance.gold;
        text.text = currentGold.ToString();
    }

    void UpdateKillCount()
    {
        int currentKillcount = PlayerManager.Instance.killCount;
        text.text = currentKillcount.ToString();
    }

    void ShowBossHpBar()
    {
    
    }

    void ToggleQuestInfo() 
    {
    
    }
}
