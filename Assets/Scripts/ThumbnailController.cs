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
        DisplayThumbnail(_firstThumbnail);
    }
    
    private void DisplayThumbnail(ThumbnailData data, ItemData item = null)
    {
        _thumbnail.sprite = data.ThumbnailImage;
        _description.text = data.Description;

        foreach (Transform child in _choicePanelTransform)
        {
            Destroy(child.gameObject);
        }
        
        if (item != null) ItemController.Instance.items.Add(item);
        
        foreach (ChoiceData choiceData in data.ChoiceData)
        {
            GameObject instantiated = Instantiate(_buttonPrefab, _choicePanelTransform);
            instantiated.GetComponentInChildren<TextMeshProUGUI>().text = choiceData.Choice;
            instantiated.GetComponent<Button>().onClick.AddListener(() =>
            {
                DisplayThumbnail(choiceData.LinkedThumbnail, choiceData.ItemReward);
            });
            if (choiceData.RequiredItem != null)
            {
                instantiated.GetComponent<Button>().interactable = ItemController.Instance.CheckIfItemExists(choiceData.RequiredItem);
            }
        }
    }
}
