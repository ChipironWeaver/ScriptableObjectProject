using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThumbnailController : MonoBehaviour
{
    [SerializeField] private ThumbnailData _firstThumbnail;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _choicePanelTransform;
    [SerializeField] private Image _thumbnail;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _reputationText;
    [SerializeField] private Vector2 _reputationTextColorRange;
    [SerializeField] private Gradient _reputationGradient;

    public int reputation;
    private void Start()
    {
        DisplayThumbnail(_firstThumbnail, new ChoiceData());
    }
    
    private void DisplayThumbnail(ThumbnailData data, ChoiceData choice)
    {
        _thumbnail.sprite = data.ThumbnailImage;
        _description.text = data.Description;
        
        reputation += choice.ReputationReward;
        UpdateReputationText();
            
        foreach (Transform child in _choicePanelTransform)
        {
            Destroy(child.gameObject);
        }

        if (choice.ItemReward != null)
        {
            foreach (ItemData item in choice.ItemReward)
            {
                ItemController.Instance.AddItem(item);
            }
        }

        if (choice.RequiredItem != null)
        {
            foreach (ItemBehaviour item in choice.RequiredItem)
            {
                if (item.Special == ItemBehaviour.SpecialBehaviour.ConsumeItem)
                {
                    ItemController.Instance.RemoveItem(item.Item);
                }
            }  
        } 
        
        foreach (ChoiceData choiceData in data.ChoiceData)
        {
            GameObject instantiated = Instantiate(_buttonPrefab, _choicePanelTransform);
            
            instantiated.GetComponentInChildren<TextMeshProUGUI>().text = choiceData.Choice;
            instantiated.GetComponent<Button>().onClick.AddListener(() => { FindThumbnail(choiceData); });
            
            foreach (ItemBehaviour item in choiceData.RequiredItem)
            {
                bool hasItem = ItemController.Instance.CheckIfItemExists(item.Item);
                if (item.Special == ItemBehaviour.SpecialBehaviour.BlockChoice) hasItem = !hasItem;
                
                if (item.Special == ItemBehaviour.SpecialBehaviour.HideChoice && !hasItem)
                {
                    Destroy(instantiated);
                }
                
                else
                {
                    instantiated.GetComponent<Button>().interactable = hasItem;
                    break;
                }
            }
        }
    }
    
    private void FindThumbnail(ChoiceData choiceData)
    {
        ThumbnailData data = choiceData.LinkedThumbnail;
        switch (choiceData.Condition)
        {
            case(ConditionalSecondThumbnail.HigherReputation):
                if (reputation <= choiceData.ReputationRequirement) data = choiceData.SecondLinkedThumbnail;
                break;
            case(ConditionalSecondThumbnail.LowerReputation):
                if (reputation >= choiceData.ReputationRequirement) data = choiceData.SecondLinkedThumbnail;
                break;
            case(ConditionalSecondThumbnail.Random):
                if (choiceData.RandomOdd < Random.Range(0, 100)) data = choiceData.SecondLinkedThumbnail;
                break;
        }
        DisplayThumbnail(data, choiceData);
    }
    
    private void UpdateReputationText()
    {
        _reputationText.text = reputation.ToString();
        _reputationText.color = _reputationGradient.Evaluate(Mathf.Clamp((reputation - _reputationTextColorRange.x)/(-_reputationTextColorRange.x + _reputationTextColorRange.y) , 0, 1));
    }
}
