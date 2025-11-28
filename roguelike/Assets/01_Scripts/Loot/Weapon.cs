/*
 * [Weapon.cs]
 * [패키지 2] 플레이어 로직 - 무기 시스템
 * 공격형 장비의 기본 클래스입니다.
 */

using UnityEngine;

public abstract class Weapon : EquipmentBase
{
    #region Protected Fields
    protected float _currentCooldown = 0f;
    #endregion

    #region Public Properties
    public WeaponData WeaponData => _data as WeaponData;
    #endregion

    #region Unity LifeCycle
    protected virtual void Update()
    {
        UpdateCooldown(Time.deltaTime);

        if (_currentCooldown <= 0f)
        {
            PerformAttack();
            _currentCooldown = WeaponData.BaseCooldown;
        }
    }
    #endregion

    #region Public Methods
    public override void Initialize(PlayerManager player, EquipmentData data)
    {
        base.Initialize(player, data);
        _currentCooldown = 0f; // 시작 시 쿨타임 0
    }

    public virtual void UpdateCooldown(float deltaTime)
    {
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= deltaTime;
        }
    }
    #endregion

    #region Abstract Methods
    /// <summary>
    /// 실제 공격 로직을 구현합니다.
    /// 쿨타임이 0이 되면 Update에서 자동으로 호출됩니다.
    /// 공격 수행 후 반드시 _currentCooldown을 재설정해야 합니다.
    /// </summary>
    protected abstract void PerformAttack();
    #endregion
}
