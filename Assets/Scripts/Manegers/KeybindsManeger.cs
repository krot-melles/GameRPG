using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindsManeger : MonoBehaviour
{
    private static KeybindsManeger instance;

    public static KeybindsManeger MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeybindsManeger>();
            }
            return instance;
        }
    }
    private string bindName;


    public Dictionary<string, KeyCode> KeyBinds { get; private set; }

    public Dictionary<string, KeyCode> ActionBinds { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        KeyBinds = new Dictionary<string, KeyCode>();
        ActionBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.W);
        BindKey("LEFT", KeyCode.A);
        BindKey("DOWN", KeyCode.S);
        BindKey("RIGHT", KeyCode.D);

        BindKey("ACT1", KeyCode.Alpha1);
        BindKey("ACT2", KeyCode.Alpha2);
        BindKey("ACT3", KeyCode.Alpha3);
        BindKey("ACT4", KeyCode.Alpha4);

    }

    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = KeyBinds;

        if (key.Contains("ACT"))
        {
            currentDictionary = ActionBinds;
        }
        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[myKey] = KeyCode.None;
            UIManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }
        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {

        this.bindName = bindName;
    }
    private void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;
            if (e.isKey)
            {

                BindKey(bindName, e.keyCode);
            }
        }
    }
}
