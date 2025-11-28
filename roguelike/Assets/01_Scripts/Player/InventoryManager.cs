/*
 * [InventoryManager.cs]
 * [패키지 2] 플레이어 로직 - 인벤토리 시스템
 * 플레이어가 보유한 장비(무기, 패시브)를 관리합니다.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    #region Serialized Fields

    [Header("Dependencies")] [SerializeField]
    private PlayerManager _playerManager;

    [SerializeField] private Transform weaponParent; // 무기가 생성될 부모 트랜스폼 (보통 플레이어 자신)

    [Header("Settings")] 
    [SerializeField] private int maxWeaponSlots = 6;
    [SerializeField] private int maxPassiveSlots = 6;
    [SerializeField] private int maxItemSlots = 3;

    #endregion

    #region Private Fields

    private List<Weapon> _weapons = new List<Weapon>(); // TODO: HUDManager에서 참조해서 장비 아이콘 띄우는 기능 구현 필요.  
    private List<Passive> _passives = new List<Passive>();
    private List<Item> _consumables = new List<Item>();

    #endregion

    #region Events

    public event Action OnInventoryChanged; // UI 갱신용

    #endregion

    #region Public Properties

    public List<Weapon> Weapons => _weapons;
    public List<Passive> Passives => _passives;

    #endregion

    #region Unity LifeCycle

    public void Initialize(PlayerManager player)
    {
        _playerManager = player;
        if (weaponParent == null)
        {
            weaponParent = player.transform;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 장비를 추가하거나 레벨업합니다.
    /// </summary>
    public void Add(EquipmentData data)
    {
        if (data == null) return;
        Debug.Log("Add 메서드 진입");
        // 1. 이미 보유 중인지 확인
        EquipmentBase existingItem = FindItem(data);
        if (existingItem != null)
        {
            existingItem.LevelUp();
            OnInventoryChanged?.Invoke();
            return;
        }

        // 2. 신규 추가
        if (data is WeaponData weaponData)
        {
            AddWeapon(weaponData);
        }
        else if (data is PassiveData passiveData)
        {
            AddPassive(passiveData);
        }
    }

    public void Add(ItemData data)
    {
        if (data == null) return;
        AddConsumable(data);
    }

    public EquipmentBase FindItem(EquipmentData data)
    {
        // 무기 검색
        foreach (var weapon in _weapons)
        {
            if (weapon.Data == data) return weapon;
        }

        // 패시브 검색
        foreach (var passive in _passives)
        {
            if (passive.Data == data) return passive;
        }

        return null;
    }

    #endregion

    #region Private Methods

    private void AddWeapon(WeaponData data)
    {
        if (_weapons.Count >= maxWeaponSlots)
        {
            Debug.Log("Weapon slots are full!");
            return;
        }

        // 프리팹 생성
        GameObject go = Instantiate(data.Prefab, weaponParent);
        go.name = data.EquipmentName;

        Weapon weapon = go.GetComponent<Weapon>();
        if (weapon == null)
        {
            Debug.LogError($"Weapon prefab {data.name} does not have a Weapon component!");
            Destroy(go);
            return;
        }

        weapon.Initialize(_playerManager, data);
        _weapons.Add(weapon);

        Debug.Log($"Added Weapon: {data.EquipmentName}");
        OnInventoryChanged?.Invoke();
    }

    private void AddPassive(PassiveData data)
    {
        if (_passives.Count >= maxPassiveSlots)
        {
            Debug.Log("Passive slots are full!");
            return;
        }

        // 프리팹 생성
        GameObject go = new GameObject(data.EquipmentName);
        go.transform.SetParent(transform); // InventoryManager 아래에 둠

        Passive passive = go.AddComponent<Passive>();
        passive.Initialize(_playerManager, data);
        _passives.Add(passive);

        Debug.Log($"Added Passive: {data.EquipmentName}");
        OnInventoryChanged?.Invoke();
    }

    private void AddConsumable(ItemData data)
    {
        // 이미 보유 중인 소모품은 나오지 않음

        // 이미 있는 아이템인지 확인
        foreach (var item in _consumables)
        {
            if (item.Data == data)
            {
                Debug.Log($"이미 보유 중인 아이템!");
                return;
            }
        }

        if (_consumables.Count >= maxItemSlots)
        {
            Debug.Log("Consumable slots are full!");
            return;
        }

        GameObject go = Instantiate(data.Prefab, transform); // InventoryManager 자식으로 생성
        go.name = data.ItemName;

        Item newItem = go.GetComponent<Item>();
        if (newItem == null)
        {
            Debug.LogError($"Item prefab {data.name} does not have an Item component!");
            Destroy(go);
            return;
        }

        newItem.Initialize(data);
        _consumables.Add(newItem);

        Debug.Log($"Added Consumable: {data.ItemName}");
        OnInventoryChanged?.Invoke();
    }

    #endregion

    #region Public Methods (Consumables)

// [InventoryManager.cs] 내부

    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _consumables.Count) return;

        Item item = _consumables[slotIndex];
        if (item != null)
        {
            // 1. 아이템 사용 시도 (쿨다운, 남은 횟수 체크는 Item 내부에서 함)
            bool isUsed = item.Activate();

            if (isUsed)
            {
                // 2. 사용에 성공했다면, 남은 횟수 확인
                if (item.Durability <= 0)
                {
                    // 3. 횟수를 다 썼다면 인벤토리 목록에서 제거 및 오브젝트 파괴
                    _consumables.RemoveAt(slotIndex);
                    Destroy(item.gameObject);
                
                    Debug.Log("아이템을 다 사용하여 파괴되었습니다.");
                }
            
                OnInventoryChanged?.Invoke();
            }
        }
    }

    #endregion

    private void Update()
    {
        // 소모품 쿨다운 갱신
        float deltaTime = Time.deltaTime;
        foreach (var item in _consumables)
        {
            item.UpdateCooldown(deltaTime);
        }
    }
}