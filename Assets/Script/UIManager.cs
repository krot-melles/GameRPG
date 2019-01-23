using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
{
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
}
    [Header("Панель скилов")]
    [SerializeField]
    private Button[] actionButton;

    private KeyCode action1, action2, action3;

    [Header("Таргет панель")]
    [SerializeField]
    private GameObject targetFrame;

    private Stat healthStat;
    
    // Start is called before the first frame update
    void Start()
    {
        healthStat = targetFrame.GetComponentInChildren<Stat>();


        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2)) 
        {
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3)) 
        {
            ActionButtonOnClick(2);
        }

    }
    private void ActionButtonOnClick(int btnIndex)
    {
        actionButton[btnIndex].onClick.Invoke();
    }
    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);
        healthStat.Initialized(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);
        target.healthChanged += new HealthChanged(UpdateTargetFrame);

    }
    public void HideTargetFrame()
    {

        targetFrame.SetActive(false);
    }
    public void UpdateTargetFrame(float healthValue)
    {
        healthStat.MyCurrentValue = healthValue;
    }
}
