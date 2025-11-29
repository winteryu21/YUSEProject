using UnityEngine;
using System;

/// 모든 몬스터의 기본이 되는 추상 클래스
/// SDS의 Monster 명세(HP, Move, TakeDamage, Die)대로 일단 만듬
public abstract class Monster : MonoBehaviour
{
    #region Serialized Fields

    [Header("Stats")] [SerializeField] protected float maxHp = 10f;
    [SerializeField] protected float moveSpeed = 2f;

    #endregion

    public GameObject expOrbPrefab;

    #region Private Fields

  
    protected float _currentHp;

 
    protected Transform _target; // 플레이어(추적 대상)


    // 몬스터가 죽었을 때 호출할 콜백 -> 풀로 복귀
    protected Action<Monster> _returnToPoolAction;

    #endregion


    #region Unity LifeCycle

    protected virtual void Awake()
    {
        
    }


    protected virtual void Start()
    {
        _currentHp = maxHp;
    }


    protected virtual void Update()
    {
        if (_target != null)
        {
            // 매 프레임 타겟 방향으로 이동
            Move(_target.position);
            
        }


        //TakeDamage(1); //풀링 테스트 시 순식간에 죽길래 비활성화 하고 테스트 했습니다.
    }

    #endregion


    #region Public Methods

    /// 스폰 시점에 타겟(플레이어)을 주입받는 초기화 메소드
    public void Init(Transform target, Action<Monster> returnToPoolCallback = null)

    {
        _target = target;
        _returnToPoolAction = returnToPoolCallback; // 콜백 저장

        // 재활용될 때 HP를 다시 채워야 합니다!
        _currentHp = maxHp;
    }


    public void TakeDamage(float amount)

    {
        _currentHp -= amount;

        // TODO: 데미지 표시 UI 로직 추가 하기
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public abstract void Move(Vector2 targetPosition);


    public virtual void Die()

    {
        // 사망 처리 (보상 드롭 등 나중에 추가하기)
        DropExpOrb();
        UpGold(1);


        // 수정.. Destroy 대신 풀로 반환 로직 실행
        if (_returnToPoolAction != null)
        {
            _returnToPoolAction.Invoke(this); // 나를 풀로 돌려보냄
        }
        else
        {
            // 콜백이 없으면 그냥 파괴
            Destroy(gameObject);
        }
    }


    public void DropExpOrb()
    {
        if (expOrbPrefab != null)
        {
            Instantiate(expOrbPrefab, transform.position, Quaternion.identity);
        }
    }

    //일단 죽으면 플레이어의 골드 증가
    public void UpGold(int amount)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Player.GainGold(amount);
        }
    }

    #endregion
   
}