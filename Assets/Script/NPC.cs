using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);

public class NPC : Character
{
    public event HealthChanged healthChanged;

    public virtual void DeSelect()
    {
               
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
}
