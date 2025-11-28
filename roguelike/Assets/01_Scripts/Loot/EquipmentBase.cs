/*
 * [EquipmentBase.cs]
 * [패키지 2] 플레이어 로직 - 장비 시스템
 * 모든 장비가 상속받는 기본 클래스입니다. (SDS 3.2.4)
 * 공통 데이터와 메서드를 정의합니다.
 */

using UnityEngine;

public abstract class EquipmentBase : MonoBehaviour
{
    #region Serialized Fields
    [Header("Basic Info")]
    [SerializeField] protected EquipmentData _data;
    #endregion

    #region Protected Fields
    protected int _level = 1;
    protected PlayerManager _player; // 장비 소유자 참조
    #endregion

    #region Public Properties
    public EquipmentData Data => _data;
    public string EquipmentName => _data != null ? _data.EquipmentName : "Unknown";
    public int Level => _level;
    #endregion

    #region Unity LifeCycle
    // 장비가 생성될 때 초기화 (PlayerManager가 호출해 줄 수도 있음)
    public virtual void Initialize(PlayerManager player, EquipmentData data)
    {
        _player = player;
        _data = data;
        
        // 초기 위치 설정 (플레이어 자식으로 들어갈 경우 로컬 위치 초기화)
        transform.localPosition = Vector3.zero; 
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// (SDS 3.2.4) 장비 레벨을 증가시킵니다.
    /// </summary>
    public virtual void LevelUp()
    {
        if (_data != null && _level >= _data.MaxLevel)
        {
            Debug.Log($"{EquipmentName} is already at max level.");
            return;
        }

        _level++;
        Debug.Log($"{EquipmentName} Level Up! Current Level: {_level}");
        
        // 레벨업에 따른 스탯 변화 로직은 자식 클래스에서 override하여 구현하거나,
        // 여기서 공통적인 처리를 할 수 있습니다.
        // 오버라이드로 처리하는 게 좋을 듯.
    }
    #endregion
}
