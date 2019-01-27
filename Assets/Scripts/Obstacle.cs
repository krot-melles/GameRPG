using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Obstacle : MonoBehaviour, IComparable<Obstacle>
{
    /// <summary>
    /// Рендеринг преград
    /// </summary>
    public SpriteRenderer MySpriterenderer { get; set; }
    private Color defaultColor;
    private Color fadeColor;

    public int CompareTo(Obstacle other)
    {
        if (MySpriterenderer.sortingOrder > other.MySpriterenderer.sortingOrder)
        {
            return 1;
        }
        else if (MySpriterenderer.sortingOrder < other.MySpriterenderer.sortingOrder)
        {
            return -1;
        }
        return 0;

    }

    // Start is called before the first frame update
    void Start()
    {
        MySpriterenderer = GetComponent<SpriteRenderer>();
        defaultColor = MySpriterenderer.color;
        fadeColor = defaultColor;
        fadeColor.a = 0.5f;



    }
    public void FadeOut()
    {
        MySpriterenderer.color = fadeColor;
    }

    public void FadeIn()
    {
        MySpriterenderer.color = defaultColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
