using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ArmorType {Helmet, Shoulders, Chest, Gloves, Feet, MainHand, OffHand, TwoHand}
[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;
    [SerializeField]
    private int intellect;
    [SerializeField]
    private int strenght;
    [SerializeField]
    private int stamina;

   
    public override string GetDescription()
    {
        string stats = string.Empty;
        if (intellect > 0)
        {
            stats += string.Format("\n +{0} Интелект", intellect);
        }
        if (strenght> 0)
        {
            stats += string.Format("\n +{0} Сила", strenght);
        }
        if (stamina > 0)
        {
            stats += string.Format("\n +{0} Выносливость", stamina);
        }

        return base.GetDescription() + stats;
    }

}
