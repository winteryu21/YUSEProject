/*
 * [TestEquip.cs]
 * [패키지 2] 플레이어 로직 - 초기 장비
 * 플레이어 주변을 회전하며 충돌한 적에게 데미지를 입히는 장비입니다.
 */

using UnityEngine;

public class TestEquip : Weapon
{
    #region Serialized Fields
    [Header("TestEquip Specific")]
    [SerializeField] private float rotationSpeed = 100f; // 회전 속도
    [SerializeField] private float distanceFromPlayer = 2f; // 플레이어로부터의 거리
    [SerializeField] private Transform visualSprite; // 실제 회전할 스프라이트 객체
    #endregion

    #region Private Fields
    // private float _currentAngle = 0f; // 사용 안함
    #endregion

    #region Unity LifeCycle
    
    private void Start()
    {
        if (visualSprite != null)
            visualSprite.localPosition = new Vector3(distanceFromPlayer, 0, 0);
    }

    public override void Initialize(PlayerManager player, EquipmentData data)
    {
        base.Initialize(player, data);
        
        // 초기 위치 설정
        if (visualSprite != null)
        {
            visualSprite.localPosition = new Vector3(distanceFromPlayer, 0, 0);
        }
    }

    protected override void Update()
    {
        base.Update(); // 쿨다운 관리 및 PerformAttack 호출
        
        RotateAroundPlayer();

        if (visualSprite != null)
            visualSprite.localRotation = Quaternion.identity;
    }

    // 충돌 감지 (반드시 Collider2D가 있어야 함, IsTrigger 체크 권장)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // (패키지 3) Monster 태그를 가진 오브젝트인지 확인
        if (collision.CompareTag("Enemy"))
        {
            Monster monster = collision.GetComponentInParent<Monster>();
            
            if (monster != null)
            {
                // 데미지 입히기 (WeaponData의 BaseDamage 사용)
                float damage = WeaponData != null ? WeaponData.BaseDamage : 10f;
                // 레벨에 따른 데미지 증가 로직 추가 가능
                damage += (_level - 1) * 5f; 

                monster.TakeDamage(damage);
                Debug.Log($"TestEquip hit {monster.name} for {damage} damage.");
            }
        }
    }
    #endregion

    #region Private Methods
    private void RotateAroundPlayer()
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.back * rotationStep);
    }
    #endregion

    #region Weapon Overrides
    protected override void PerformAttack()
    {
        // 이 장비는 지속형(회전)이라 특정 시점에 발사하지 않으므로 비워둡니다.
        // 하지만 Weapon 클래스 구조상 쿨다운이 돌면 이 함수가 호출됩니다.
        // 필요하다면 여기서 추가적인 효과(예: 잠시 빨라짐)를 줄 수 있습니다.
        
        // 쿨다운 재설정 (WeaponData가 없으면 기본값 1초)
        _currentCooldown = WeaponData != null ? WeaponData.BaseCooldown : 1f;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        
        // 레벨 업 시 스탯 강화 (예: 회전 속도 증가, 데미지 증가)
        rotationSpeed += 20f;
        
        // (선택) 크기 증가
        if (visualSprite != null)
        {
            visualSprite.localScale *= 1.1f;
        }
    }
    #endregion
}
