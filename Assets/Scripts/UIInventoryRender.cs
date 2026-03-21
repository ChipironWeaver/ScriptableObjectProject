using UnityEngine;
using  UnityEngine.UI;
public class UIInventoryRender : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;
    
    void OnEnable()
    {
        ItemController.onInventoryUpdate += InventoryUpdate;
    }

    void OnDisable()
    {
        ItemController.onInventoryUpdate -= InventoryUpdate;
    }

    private void InventoryUpdate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (ItemData item in ItemController.Instance.items)
        {
            InstantiateItems(item);
        }
    }

    private void InstantiateItems(ItemData item)
    {
        GameObject instantiate = Instantiate(_itemPrefab, transform);
        instantiate.GetComponent<Image>().sprite = item.icon;
    }
    
}
