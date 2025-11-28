/*
 * [Passive.cs]
 * [패키지 2] 플레이어 로직 - 패시브 시스템
 * 능력치 증가 등 패시브 효과를 담당하는 클래스입니다.
 */

using UnityEngine;

public class Passive : EquipmentBase
{
    #region Public Properties
    public PassiveData PassiveData => _data as PassiveData;
    #endregion

    #region Public Methods
    public override void Initialize(PlayerManager player, EquipmentData data)
    {
        base.Initialize(player, data);
        ApplyStatBonus();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        ApplyStatBonus(); // 레벨업 시 스탯 재적용 (누적 방식인지 재계산 방식인지에 따라 다름)
    }
    #endregion

    #region Private Methods
    private void ApplyStatBonus()
    {
        if (PassiveData == null || _player == null) return;

        // PlayerStats에 접근하여 스탯 적용
        // 현재 PlayerStats 구조를 정확히 모르므로 주석 처리 또는 가상 코드로 작성
        // _player.Stats.AddBonus(PassiveData.StatType, PassiveData.StatValue * _level);
        
        Debug.Log($"Applied Passive Bonus: {PassiveData.StatType} + {PassiveData.StatValue * _level}");
    }
    #endregion
}
