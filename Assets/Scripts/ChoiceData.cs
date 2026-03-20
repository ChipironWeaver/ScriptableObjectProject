using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ChoiceData
{
    public string Choice;
    public ThumbnailData LinkedThumbnail;
    public ThumbnailData SecondLinkedThumbnail;
    public List<ItemData> ItemReward;
    public int ReputationReward;
    public List<ItemBehaviour> RequiredItem;
    public ConditionalSecondThumbnail Condition ;
    [Range(0,100)]public int RandomOdd;
    public int ReputationRequirement;
}
[Serializable]
public struct ItemBehaviour
{
    public ItemData Item;
    public SpecialBehaviour Special;
    [Flags]
    public enum SpecialBehaviour
    {
        ConsumeItem = 1,
        BlockChoice = 2,
        HideChoice = 4,
    }
}

public enum ConditionalSecondThumbnail
{
    None,
    LowerReputation,
    HigherReputation,
    Random
}

