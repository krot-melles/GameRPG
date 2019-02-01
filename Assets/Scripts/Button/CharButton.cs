using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ArmorType armoryType;

    private Armor equipedArmor;

    [SerializeField]
    private Image icon;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;
                if (tmp.MyArmorType == armoryType)
                {
                    EquipArmor(tmp);
                }
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        armor.Remove();
        if (equipedArmor != null)
        {
            armor.MySlot.AddItem(equipedArmor);
            UIManager.MyInstance.RefreshTooltip(equipedArmor);
        }
        else
        {
            UIManager.MyInstance.HideTooltip();
        }
        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        this.equipedArmor = armor;
        if (HandScript.MyInstance.MyMoveable == (armor as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equipedArmor != null)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(0,0), transform.position, equipedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
