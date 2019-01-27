using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    private Bag bag;

    [SerializeField]
    private Sprite full, empty;

    public Bag MyBag
    {
        get
        {
           return bag;
        }
        set
        {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                HandScript.MyInstance.TakeMoveable(MyBag);
            }
        }
       else if (bag != null)
        {
            bag.MyBagScript.OpenClose();
        }
    }

    public void RemoveBag()
    {

        InventoryScript.MyInstance.RemoveBag(MyBag);

        MyBag.MyBagButton = null;
        foreach (Item item in MyBag.MyBagScript.GetItems())
        {
            InventoryScript.MyInstance.AddItem(item);
        }
        MyBag = null;
    }

}
