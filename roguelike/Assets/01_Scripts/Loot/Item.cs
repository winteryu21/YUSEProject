using UnityEngine;

/// <summary>
/// 인게임 오브젝트에 부착되어 아이템의 상태와 로직을 관리
/// </summary>
public class Item : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]
    private ItemData data;
    
    [Header("Item State")]
    [SerializeField]
    private int durability = 1; // 현재 보유 개수 (또는 내구도)
    
    [SerializeField]
    private float currentCooldown = 0f;

    #region Properties
    public ItemData Data => data;
    public int Durability => durability;
    public float CurrentCooldown => currentCooldown;
    #endregion

    public void Initialize(ItemData itemData)
    {
        data = itemData;
        durability = itemData.MaxDurability;
        currentCooldown = 0f;
    }

    /// <summary>
    /// 아이템을 사용
    /// </summary>
    public bool Activate()
    {
        if (data == null) return false;

        // 1. 쿨다운 확인
        if (currentCooldown > 0f)
        {
            Debug.Log($"아이템 쿨다운 중: {currentCooldown:F1}초 남음");
            return false;
        }

        // 2. 개수 확인
        if (durability <= 0)
        {
            Debug.Log("아이템 소진됨.");
            return false;
        }

        // 3. 사용 처리
        currentCooldown = data.Cooldown;
        durability--;
        
        Debug.Log($"아이템 {data.ItemName} 사용! (남은 개수: {durability})");
        
        // (TODO) 실제 아이템 효과 적용 (예: PlayerManager.Heal 등)
        // 효과는 ItemData에 정의된 타입이나 별도 컴포넌트를 통해 처리 가능
        
        return true;
    }

    /// <summary>
    /// 쿨다운 시간을 갱신합니다.
    /// </summary>
    public void UpdateCooldown(float deltaTime)
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= deltaTime;
            if (currentCooldown < 0f)
            {
                currentCooldown = 0f;
            }
        }
    }
}