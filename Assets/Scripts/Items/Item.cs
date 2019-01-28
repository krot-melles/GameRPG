using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;
    
    [SerializeField]
    private Quality quality;


    private SlotScript slot; 

    public Sprite MyIcon
    {

        get
        {
           return icon;
        }
    }
    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }
    public SlotScript MySlot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }

    public Quality MyQuality { get => quality; }
    public string MyTitle { get => title; }

    public virtual string GetDescription()
    {
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality],MyTitle);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
