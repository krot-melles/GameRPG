using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
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
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            bag.Use();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            HealthPotion hpotion = (HealthPotion)Instantiate(items[1]);
            AddItem(hpotion);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ManaPotion mpotion = (ManaPotion)Instantiate(items[2]);
            AddItem(mpotion);
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
    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    /// <summary>
    /// Добавление обьектов в инвентарь
    /// </summary>
    /// <param name="item">обьект добавления</param>
    public void AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return;
            }
        }
        PlaceInEmpty(item);

    }

    private void PlaceInEmpty(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                return;
            }
        }
    }

    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in bags)
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
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
}
