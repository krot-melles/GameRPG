using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;
    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    //Для теста
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get { return bags.Count < 5; }
    }
    
    public int MyEmptySlotCount
    {


        get
        {
            int count = 0;
            foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }
            return count;
        }
    }

   public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }

            return count;
        }

    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount-MyEmptySlotCount;
        }

    }


    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }
        set
        {
            fromSlot = value;
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(16);
        bag.Use();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Armor armor = (Armor)Instantiate(items[3]);
            AddItem(armor);
            Armor armor1 = (Armor)Instantiate(items[4]);
            AddItem(armor1);
            Armor armor2 = (Armor)Instantiate(items[5]);
            AddItem(armor2);
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ManaPotion mpotion = (ManaPotion)Instantiate(items[2]);
            AddItem(mpotion);
            HealthPotion hpotion = (HealthPotion)Instantiate(items[1]);
            AddItem(hpotion);
        }
    }

    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                bag.MyBagButton = bagButton;
                break;
            }
        }
    }

    public void AddBag(Bag bag, BagButton bagButton)
    {
        bags.Add(bag);
        bagButton.MyBag = bag;
    }

    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCoun = (MyTotalSlotCount - oldBag.Slots) + newBag.Slots;

        if (newSlotCoun - MyFullSlotCount >= 0)
        {
            // Do Swap

            List<Item> bagItems = oldBag.MyBagScript.GetItems();
            RemoveBag(oldBag);
            newBag.MyBagButton = oldBag.MyBagButton;
            newBag.Use();
            foreach (Item item in bagItems)
            {
                if (item != newBag)
                {
                    AddItem(item);
                }
            }

            AddItem(oldBag);
            HandScript.MyInstance.Drop();
            MyInstance.fromSlot = null;
        }
    }

    /// <summary>
    /// Добавление обьектов в инвентарь
    /// </summary>
    /// <param name="item">обьект добавления</param>
    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }
       return PlaceInEmpty(item);

    }

    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                return true;
            }
        }
        return false;
    }

    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in bags)
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;
    }




    /// <summary>
    /// Открыть или закрыть все сумки
    /// </summary>
    public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        //если bag == true, открыть все закрытые сумки 
        //если bag == false, закрыть все открытые сумки 

        foreach (Bag bag in bags)
        {

            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }
    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in bags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }
        return useables;
    }
    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
