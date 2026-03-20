using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
                if (item.Consumable)
                {
                    ItemController.Instance.RemoveItem(item.Item);
                }
            }  
        } 
        
        foreach (ChoiceData choiceData in data.ChoiceData)
        {

            bool displayChoice = true;
            GameObject instantiated = Instantiate(_buttonPrefab, _choicePanelTransform);
            
            instantiated.GetComponentInChildren<TextMeshProUGUI>().text = choiceData.Choice;
            instantiated.GetComponent<Button>().onClick.AddListener(() => { FindThumbnail(choiceData); });
            
            foreach (ItemBehaviour item in choiceData.RequiredItem)
            {
                if (!ItemController.Instance.CheckIfItemExists(item.Item)) displayChoice = false;
                
            }
  
            if (choiceData.Behavior.HasFlag(SpecialBehaviour.ReputationRequirement) && reputation < choiceData.ReputationRequirement) displayChoice = false;
            
            if (choiceData.Behavior.HasFlag(SpecialBehaviour.BlockChoice)) displayChoice = !displayChoice;

            if (choiceData.Behavior.HasFlag(SpecialBehaviour.HideChoice) && !displayChoice) Destroy(instantiated);
            else instantiated.GetComponent<Button>().interactable = displayChoice;
            
        }
    }
    
    private void FindThumbnail(ChoiceData choiceData)
    {
        ThumbnailData data = choiceData.LinkedThumbnail;
                
        if (choiceData.RandomOdd < Random.Range(0, 100) && choiceData.Behavior.HasFlag(SpecialBehaviour.Random)) data = choiceData.SecondLinkedThumbnail;
        
        DisplayThumbnail(data, choiceData);
    }
    
    private void UpdateReputationText()
    {
        _reputationText.text = reputation.ToString();
        _reputationText.color = _reputationGradient.Evaluate(Mathf.Clamp((reputation - _reputationTextColorRange.x)/(-_reputationTextColorRange.x + _reputationTextColorRange.y) , 0, 1));
    }
}