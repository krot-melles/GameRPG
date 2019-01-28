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
    public override string GetDescription()
    {

        return base.GetDescription() + string.Format("\n<color=#d7d4ae>Применение:</color> Мгновенно \n<color=#d7d4ae>Востанавливает: </color><color=#dac644>{0}</color> <color=#d7d4ae>ед. маны</color>", mana);
    }
}
