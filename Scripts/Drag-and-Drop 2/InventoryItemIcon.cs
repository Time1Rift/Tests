using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemIcon : MonoBehaviour
{
    public void SetItem(Sprite item)
    {
        Image iconImage = GetComponent<Image>();

        if (item == null)
        {
            iconImage.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = item;
        }
    }

    public Sprite GetItem()
    {
        Image iconImage = GetComponent<Image>();

        if (iconImage.enabled == false)
            return null;

        return iconImage.sprite;
    }
}