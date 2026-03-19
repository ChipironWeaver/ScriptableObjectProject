using System;
using System.Collections.Generic;

[Serializable]
public struct ChoiceData
{
    public string Choice;
    public ThumbnailData LinkedThumbnail;
    public List<ItemData> ItemReward;
    public List<ItemBehaviour> RequiredItem;
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
        HideChoice = 4
    }
}

