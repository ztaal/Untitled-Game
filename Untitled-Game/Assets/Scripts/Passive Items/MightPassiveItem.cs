using UnityEngine;

public class MightPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}