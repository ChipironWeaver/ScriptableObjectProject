using System;

[Serializable]
public struct ChoiceData
{
    public string Choice;
    public ThumbnailData LinkedThumbnail;
    public ItemData ItemReward;
    public ItemData RequiredItem;
    public bool ConsumerItem;
}