using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ScenesLoad : MonoBehaviour
{

    [Header("Загружаемая сцена")]
    public int sceneID;
    [Header("Дополнительные объекты")]
    public Image loadingImg;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AsyncLoad());
    }


    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImg.fillAmount = progress;
            yield return null;
        }
        }
}
