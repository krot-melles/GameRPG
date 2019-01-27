using UnityEngine;
using System.Collections;

[CreateAssetMenu (fileName ="ManaPotion", menuName ="Items/ManaPotion", order =1)]
public class ManaPotion : Item, IUseable
{
    [SerializeField]
    private int mana;

    public void Use()
    {
        if (Player.MyInstance.MyMana.MyCurrentValue < Player.MyInstance.MyMana.MyMaxValue)
        {
            Remove();
            Player.MyInstance.MyMana.MyCurrentValue += mana;
        }

    }
}
