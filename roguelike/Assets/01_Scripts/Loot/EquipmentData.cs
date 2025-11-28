using UnityEngine;
/// <summary>
/// 모든 장비 데이터의 공통 부모 클래스 (ScriptableObject)
/// </summary>
public abstract class EquipmentData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _equipmentName;
    [SerializeField] [TextArea] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _maxLevel = 5;
    public string EquipmentName => _equipmentName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public int MaxLevel => _maxLevel;
}