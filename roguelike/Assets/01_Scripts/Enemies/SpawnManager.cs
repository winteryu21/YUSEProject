using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnManager : MonoBehaviour
{
    #region Serialized Fields

    [Header("References")] 
    [SerializeField] private NormalMonster normalMonsterPrefab; // 생성할 프리팹
    [SerializeField] private Transform playerTransform; // 플레이어 위치 참조


//에디터에서 입력한 값을 우선해서 적용하므로 유의하세요.

//에디터가 아니라 코드로 하려면 에디터 공개 구문 제거하고 코드 안에서만 되도록 하면 됩니다.

    [Header("Settings")] 
    [SerializeField] private float spawnInterval = 5.0f; // 스폰 주기
    [SerializeField] private float spawnRadius = 10.0f; // 플레이어 기준 스폰 거리
    [SerializeField] private int initialPoolSize = 100; //pool 크기

    #endregion

    #region Private Fields

    private float _timer;

    // 오브젝트 풀 큐랑, 에디터에서 흩어져 있으면 보기가 곤란하니
    // 그 밑에다가 담아둘 poolContainer
    private Queue<Monster> _monsterPool = new Queue<Monster>();
    private Transform _poolContainer;

    #endregion


    #region Unity LifeCycle

    private void Awake()

    {
        // 1. 컨테이너 생성 (Hierarchy 정리용)
        GameObject container = new GameObject("Monster Pool Container");
        _poolContainer = container.transform;

        // 2. 게임 시작 시 100개 미리 생성 (Pre-warming)
        PreloadMonsters();
    }

    private void Update()
    {
        UpdateSpawning(Time.time);
    }

    #endregion


    #region Public Methods

    // 시간에 맞는 몬스터 생성 처리 로직을 여기에 추가 하면 됩니다.
    public void UpdateSpawning(float gameTime)

    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            Vector2 spawnPos = CalculateSpawnPosition();
            SpawnMonster(normalMonsterPrefab, spawnPos);
            _timer = 0f;
        }
    }


    public void SpawnMonster(Monster prefab, Vector2 position)

    {
        Monster monster = null;

        // 1. 풀에 사용 가능한 몬스터가 있는지 확인
        if (_monsterPool.Count > 0)
        {
            monster = _monsterPool.Dequeue();
        }
        else
        {
            // 2. 없으면 새로 생성 (Instantiate)
            monster = CreateNewMonster(prefab);
        }

        // 3. 위치 설정 및 활성화
        monster.transform.position = position;
        monster.gameObject.SetActive(true);


        // 4. 초기화 (플레이어 위치 + 사망 시 풀로 돌아오는 콜백 전달)
        // ReturnToPool 메서드를 몬스터에게 알리기
        monster.Init(playerTransform, ReturnToPool);
    }


    // 몬스터가 죽었을 때 호출되어 풀로 반환
    public void ReturnToPool(Monster monster)
    {
        monster.gameObject.SetActive(false); // 비활성화
        _monsterPool.Enqueue(monster); // 큐에 반환
    }


    // 몬스터 생성 위치 계산 화면 밖 랜덤
    public Vector2 CalculateSpawnPosition()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 origin = playerTransform != null ? (Vector2)playerTransform.position : Vector2.zero;

        return origin + (randomDir * spawnRadius);
    }

    // 초기 100개 미리 만들기
    private void PreloadMonsters()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Monster monster = CreateNewMonster(normalMonsterPrefab);
            monster.gameObject.SetActive(false); // 미리 만들어두고 꺼두기
            _monsterPool.Enqueue(monster); // 큐에 넣기
        }
    }

    // 몬스터 생성
    private Monster CreateNewMonster(Monster prefab)
    {
        Monster monster = Instantiate(prefab, _poolContainer); // 컨테이너 자식으로 생성

        return monster;
    }

    #endregion
}