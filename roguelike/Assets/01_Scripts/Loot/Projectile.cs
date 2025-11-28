/*
 * [Projectile.cs]
 * [패키지 2] 플레이어 로직 - 무기 시스템
 * 무기에서 발사되는 투사체의 이동 및 충돌 처리를 담당합니다.
 */

using UnityEngine;

/// <summary>
/// 무기 투사체 클래스
/// </summary>
public class Projectile : MonoBehaviour
{
    #region Private Fields
    private float _speed;
    private float _damage;
    private Vector2 _direction;
    private int _penetration;
    #endregion

    #region Unity LifeCycle
    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(_damage);
                _penetration--;

                if (_penetration <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (other.CompareTag("Wall")) // 벽에 부딪히면 파괴
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// 투사체를 초기화합니다.
    /// </summary>
    /// <param name="speed">투사체 속도</param>
    /// <param name="damage">투사체 데미지</param>
    /// <param name="direction">발사 방향</param>
    /// <param name="penetration">관통 횟수</param>
    public void Initialize(float speed, float damage, Vector2 direction, int penetration)
    {
        _speed = speed;
        _damage = damage;
        _direction = direction.normalized;
        _penetration = penetration;

        // 일정 시간 후 자동 파괴 (안전 장치)
        Destroy(gameObject, 5f);
    }
    #endregion
}
