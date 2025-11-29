using UnityEngine;
using TMPro;
using UnityEditor.ShaderGraph.Internal;


public enum UpgradeType
{
    Hp,              // hpUpgrade
    Speed,          // speedUpgrade
    AttackDamage,       // attackUpgrade
    AttackSpeed,        // attackSpeedUpgrade
    Cooldown,           // coolDownUpgrade
    Defense,            // defendUpgrade
    MagnetRange,        // magnetUpgrade
    CritChance,         // criticalProbabilityUpgrade
    CritDamage,         // criticalDamageUpgrade
    ExpMultiplier,      // expMultUpgrade
    GoldMultiplier,     // goldMultUpgrade
    DamageReduction,    // reduceDamageUpgrade
    StartWeaponCount,  // startWeaponPlus 
    Hp_item
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("ui tooltip")]
    public TMP_Text contentText;
    public RectTransform TooltipRect;
    public Vector2 offset = new Vector2(150, 10);


    [Header("gold ui")]
    public int currentGold;
    public TMP_Text goldText;
    


    [Header("플레이어 능력치 업그레이드")]
    public float hpUpgrade;
    public float speedUpgrade;
    public float attackUpgrade;
    public float attackSpeedUpgrade;
    public float coolDownUpgrade;
    public float defendUpgrade;
    public float magnetUpgrade;
    public float criticalProbabilityUpgrade;
    public float criticalDamageUpgrade;
    public float Hp_item_Upgrade;
    public float expMultUpgrade;
    public float goldMultUpgrade;
    public float reduceDamageUpgrade;
    public int startWeaponPlus;

  
    

    #region Unity life Cycle
    private void Awake()
    {
        Instance = this;
       

    }
    void Start()
    {
        // currentGold = PlayerPrefs.GetInt("KEY_GOLD"); 저장한거받기
        //테스트
        currentGold = 130;
        UpdateGoldUI();
    }

    // Update is called once per frame
    void Update()
    {
        //툴팁 ui가 켜져잇으면 항상 마우스 쫓아가게
        if(TooltipRect != null && TooltipRect.gameObject.activeSelf)
        {
            UpdateTooltipPosition();
        }
    }

    #endregion

    #region UITooltip 로직
    private void UpdateTooltipPosition()
    {
        Vector2 mousePos= Input.mousePosition;

        float width = TooltipRect.rect.width;
        float height = TooltipRect.rect.height;

        float finalX =mousePos.x+offset.x;
        float finalY =mousePos.y+offset.y;
        //화면밖 오른쪽으로 나가면
        if(finalX +width >Screen.width)
        {
            finalX=mousePos.x-width-offset.x;
        }

        if(finalY-height<0)
        {
            finalY=mousePos.y+height-offset.y;
        }


        TooltipRect.position=new Vector2(finalX,finalY);
    }

    public void ShowTooltip(string text)
    {
        TooltipRect.gameObject.SetActive(true);
        contentText.text = text;
        UpdateTooltipPosition();
    }

    public void HideTooltip()
    {
        TooltipRect.gameObject.SetActive(false);
    }

    #endregion

    //이것은 능력치가 오르는 스위치문
    public void ApplyUpgrade(UpgradeType type, float amount)
    {
        switch (type)
        {
            case UpgradeType.Hp:               
                Debug.Log($"최대 체력 {amount} 증가");
                break;

            case UpgradeType.Speed:
                Debug.Log($"이동 속도 {amount} 증가");
                break;

            case UpgradeType.AttackDamage:
                Debug.Log($"공격력 {amount} 증가");
                break;

            case UpgradeType.AttackSpeed:
                Debug.Log($"공격 속도 {amount} 증가");
                break;

            case UpgradeType.Cooldown:
                Debug.Log($"쿨타임 {amount} 감소");
                break;

            case UpgradeType.Defense:
                Debug.Log($"방어력 {amount} 증가");
                break;

            case UpgradeType.MagnetRange:
                Debug.Log($"자석 범위 {amount} 증가");
                break;

            case UpgradeType.CritChance:
                Debug.Log($"치명타 확률 {amount}% 증가");
                break;

            case UpgradeType.CritDamage:
                Debug.Log($"치명타 피해 {amount}% 증가");
                break;

            case UpgradeType.ExpMultiplier:
                Debug.Log($"경험치 획득량 {amount}% 증가");
                break;

            case UpgradeType.GoldMultiplier:
                Debug.Log($"골드 획득량 {amount}% 증가");
                break;

            case UpgradeType.DamageReduction:
                Debug.Log($"피해 감소율 {amount}% 증가");
                break;

            case UpgradeType.StartWeaponCount:
                Debug.Log($"투사체/무기 수 {(int)amount}개 증가");
                break;

            case UpgradeType.Hp_item:
                Debug.Log($"체력 아이템 회복량{amount}% 증가");
                break;

            default:
                Debug.Log("알 수 없는 업그레이드 타입입니다.");
                break;
        }
    }



    #region 상단 골드 바 UI로직


    //돈쓰기
    public bool SpendGold(int price)
    {
        if(currentGold>=price)
        {
            currentGold -=price;
            
            UpdateGoldUI(); 

            return true;
        }

        else
        {
            Debug.Log("잔액 부족");
            return false;
        }
        
    }

    //돈환불
    public void refund(int price)
    {
        currentGold += price;
        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        goldText.text =currentGold.ToString();
    }

    #endregion



}
