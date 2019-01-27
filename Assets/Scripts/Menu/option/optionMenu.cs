using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class optionMenu: MonoBehaviour
{
    [Header("Настройка графики")]
    public Dropdown dropDown;
    [Header("Настройка звука")]
    public AudioMixer audioMixer;


    // Start is called before the first frame update
    void Start()
    {
        //QualitySettings.names;
        dropDown.ClearOptions();
        dropDown.AddOptions(QualitySettings.names.ToList());
        dropDown.value = QualitySettings.GetQualityLevel();

    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(dropDown.value);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    // Update is called once per frame
    void Update()
    { 
    }
}
