using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ChoiceData
{
    public string Choice;
    public ThumbnailData LinkedThumbnail;
    public ThumbnailData SecondLinkedThumbnail;
    public int ReputationReward;
    public List<ItemBehaviour> RequiredItem;
    public SpecialBehaviour Behavior ;
    [Range(0,100)]public int RandomOdd;
    public int ReputationRequirement;
}
[Serializable]
public struct ItemBehaviour
{
    public ItemData Item;
    public bool Consumable;
    
}
[Flags]
public enum SpecialBehaviour
{
    BlockChoice = 1,
    HideChoice = 2,
    ReputationRequirement = 8,
    Random = 16,
    OneItemRequirement = 32,
    ResetInventoryReputation = 64,
    GoBackMainMenu = 128,
}

