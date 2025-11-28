using UnityEngine;

/// <summary>
/// 소모성 아이템 데이터 (ScriptableObject)
/// </summary>
[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objects/Item/Consumable Data")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _itemName;
    [SerializeField] [TextArea] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab; // 아이템 로직이 담긴 프리팹 (Item 컴포넌트 포함)

    [Header("Item Stats")]
    [SerializeField] private float _cooldown = 5f;
    [SerializeField] private int _maxDurability = 1; // 최대 보유 가능 개수 (또는 내구도)

    public string ItemName => _itemName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public float Cooldown => _cooldown;
    public int MaxDurability => _maxDurability;
}
