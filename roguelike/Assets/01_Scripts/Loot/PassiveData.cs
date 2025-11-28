using UnityEngine;
/// <summary>
/// 패시브 아이템 전용 데이터 (ScriptableObject)
/// </summary>
[CreateAssetMenu(fileName = "New Passive Data", menuName = "Scriptable Objects/Equipment/Passive Data")]
public class PassiveData : EquipmentData
{
    // StatType은 PlayerStats.cs 등에 정의되어 있다고 가정하거나, 새로 정의해야 함.
    // 일단은 문자열이나 enum으로 처리. 여기서는 간단히 enum을 내부 정의하거나 외부 정의를 따름.
    // PlayerStats.cs를 확인해보니 StatType이 명시적으로 보이지 않았음. 
    // PlayerStats에 정의된 필드들을 참고하여 enum을 만들거나, 일단은 범용적인 StatType enum을 사용하는 것이 좋음.
    // 여기서는 StatType enum을 사용한다고 가정하고, 만약 없으면 나중에 추가.
    
    [Header("Passive Stats")]
    [SerializeField] private ItemStatType statType;
    [SerializeField] private float statValue; // 증가량 (예: 10, 0.1 등)
    public ItemStatType StatType => statType;
    public float StatValue => statValue;
}
// 임시 StatType 정의 (PlayerStats.cs나 별도 파일로 이동 권장)
public enum ItemStatType
{
    MaxHp,
    Speed,
    MagnetRange,
    // 필요한 스탯 추가
}