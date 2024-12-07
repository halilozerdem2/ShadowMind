using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemTemplatePrefab; 
    public Transform contentParent;  

    public void UpdateInventoryUI()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Yeni itemleri listele
        foreach (var item in Inventory.items)
        {
            GameObject itemUI = Instantiate(itemTemplatePrefab, contentParent);

            var iconImage = itemUI.transform.Find("Icon").GetComponent<Image>();
            iconImage.sprite = item.Icon;

            var nameText = itemUI.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            nameText.text = item.Name;

            var typeText = itemUI.transform.Find("Type").GetComponent<TextMeshProUGUI>();
            typeText.text = item.Type;
        }
    }

    private void OnEnable()
    {
        UpdateInventoryUI();
    }


}
