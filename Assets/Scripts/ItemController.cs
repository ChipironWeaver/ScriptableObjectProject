using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public List<ItemData> items =  new List<ItemData>();
    public static ItemController Instance { get; private set; }
    
    public delegate void OnInventoryUpdate();
    public static event OnInventoryUpdate onInventoryUpdate;
    
    public delegate void OnItemReward(ItemData item);
    public static event OnItemReward onItemReward;
    
    
    
    private void Awake()
    {
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
        }
        onInventoryUpdate?.Invoke();
        onItemReward?.Invoke(item);
    }

    public void RemoveItem(ItemData item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
        onInventoryUpdate?.Invoke();
    }
}
