using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour,ISaveManager
{
    public static Inventory instance;

    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;



    private UI_ItemSlot[] inventoryItemsSlot;
    private UI_EquipmentSlot[] equipmentItemsSlot;
    private UI_StatSlot[] statSlot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemsSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentItemsSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

    }

    private void UpdateSlotUI()
    {

        for (int i = 0; i < equipmentItemsSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentItemsSlot[i].slotType)
                {
                    equipmentItemsSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < inventoryItemsSlot.Length; i++)
        {
            inventoryItemsSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItemsSlot[i].UpdateSlot(inventoryItems[i]);
        }

        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (CanAddItem())
        {
            if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
            {
                value.AddStack();
            }
            else
            {
                InventoryItem newItem = new InventoryItem(_item);
                inventoryItems.Add(newItem);
                inventoryDictionary.Add(_item, newItem);

            }
            UpdateSlotUI();
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(_item);
        ItemData_Equipment itemToRemove = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                itemToRemove = item.Key;
            }
        }
        if (itemToRemove != null)
        {
            UnequipItem(itemToRemove);
            AddItem(itemToRemove);
        }
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();
        RemoveItem(_item);
        UpdateSlotUI();
    }

    public void UnequipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {

            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    public bool CanAddItem()
    {
        if (inventoryItems.Count >= inventoryItemsSlot.Length)
        {
            return false;
        }
        return true;
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }
        UpdateSlotUI();
    }

    public ItemData_Equipment GetEquipment(EquipmentType type)
    {
        ItemData_Equipment equipedItem = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == type)
            {
                equipedItem = item.Key;
            }
        }
        return equipedItem;
    }

    public void LoadData(GameData _data)
    {
        Debug.Log("load data");
    }   

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        Debug.Log(_data.inventory.Count);
        foreach(KeyValuePair<ItemData,InventoryItem> pair in inventoryDictionary)
        {   
            Debug.Log(pair.Key.itemId+" "+ pair.Value.stackSize);
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize); 
        }
    }
}
