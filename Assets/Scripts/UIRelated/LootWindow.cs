using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    private static LootWindow instance;

    [SerializeField]
    private LootButton[] lootButtons;

    private CanvasGroup canvasGroup;

    private List<List<Item>> pages = new List<List<Item>>();
    private List<Item> droppedLoot = new List<Item>();
    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;


    [SerializeField]
    private Item[] items;
    [SerializeField]
    private GameObject nextBtn, prevBtn;

    public bool IsOpen
    {
        get { return canvasGroup.alpha > 0; }
    }

    public static LootWindow MyInstance
    {
        get
        {
            if (MyInstance == null)
            {
               instance = GameObject.FindObjectOfType<LootWindow>();
               instance = GameObject.FindObjectOfType<LootWindow>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //List<Item> tmp = new List<Item>();
        //for (int i=0;i<items.Length;i++)
        //{

        //    tmp.Add(items[i]);
        //}
        //GreatePages(tmp);
    }
    public void CreatePages(List<Item> items)
    {
        if (IsOpen)
        {
            List<Item> page = new List<Item>();
            droppedLoot = items;
            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);
                if (page.Count == 4 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Item>();
                }
            }

            AddLoot();
            Open();
        }

    }

    private void AddLoot()
    {
        if (pages.Count > 0)
        {
            pageNumber.text = pageIndex + 1 + "/" + pages.Count;
            prevBtn.SetActive(pageIndex > 0);
            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);

            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;
                    lootButtons[i].MyLoot = pages[pageIndex][i];

                    lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);
                    lootButtons[i].MyTitle.text = title;
                }

            }
        }


    }

    public void ClearButton()
    {
        foreach (LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count - 1)
        {
            pageIndex++;
            ClearButton();
            AddLoot();
        }

    }
    public void PrevPage()
    {
        if (pageIndex >0)
        {
            pageIndex--;
            ClearButton();
            AddLoot();
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);

        droppedLoot.Remove(loot);

        if (pages[pageIndex].Count == 0)
        {
            pages.Remove(pages[pageIndex]);

            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }
            AddLoot();
        }
    }
    public void Close()
    {
        pages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButton();
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
