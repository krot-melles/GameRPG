using UnityEngine;
using System.Collections;

[CreateAssetMenu (fileName ="HealthPotion", menuName ="Items/HealthPotion", order =1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int health;

    public void Use()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        {
            Remove();
            Player.MyInstance.MyHealth.MyCurrentValue += health;
        }

    }

    public override string GetDescription()
    {

        return base.GetDescription() + string.Format("\n<color=#d7d4ae>Применение:</color> Мгновенно \n<color=#d7d4ae>Востанавливает: </color><color=#dac644>{0}</color> <color=#d7d4ae>ед. жизни.</color>", health);
    }
}
