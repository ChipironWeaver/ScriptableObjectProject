using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public List<ItemData> items =  new List<ItemData>();
    
    public delegate void OnInventoryUpdate();
    public static event OnInventoryUpdate onInventoryUpdate;
    
    public delegate void OnItemReward(ItemData item);
    public static event OnItemReward onItemReward;
    
    public static ItemController Instance { get; private set; }
    
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    
    public bool CheckIfItemExists(ItemData item)
    {
        return items.Contains(item);
    }

    public void AddItem(ItemData item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            onInventoryUpdate?.Invoke();
            onItemReward?.Invoke(item);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            onInventoryUpdate?.Invoke();
        }
    }

    public void ForceUpdate()
    {
        onInventoryUpdate?.Invoke();
    }
}
