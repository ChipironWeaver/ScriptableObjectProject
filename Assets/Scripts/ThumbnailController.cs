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

    private void Start()
    {
        DisplayThumbnail(_firstThumbnail, new ChoiceData());
    }
    
    private void DisplayThumbnail(ThumbnailData data, ChoiceData choice)
    {
        _thumbnail.sprite = data.ThumbnailImage;
        _description.text = data.Description;

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
            instantiated.GetComponent<Button>().onClick.AddListener(() => { DisplayThumbnail(choiceData.LinkedThumbnail, choiceData); });
            
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
}
