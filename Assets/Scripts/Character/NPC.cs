using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);
public delegate void ChracterRemoved();

public class NPC : Character
{
    public event HealthChanged healthChanged;

    public event ChracterRemoved characterRemoved;


    [SerializeField]
    private Sprite portrait;

    public Sprite MyPortrait
    { get

        {
            return portrait;
        }
    }

    public virtual void DeSelect()
    {
       healthChanged -= new HealthChanged(UIManager.MyInstance.UpdateTargetFrame);
       characterRemoved-= new ChracterRemoved(UIManager.MyInstance.HideTargetFrame);

    }
    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
       
    }
    public void OnChracterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }

        Destroy(gameObject);

    }
    public virtual void Interact()
    {
        Debug.Log("loot");
    }

}
