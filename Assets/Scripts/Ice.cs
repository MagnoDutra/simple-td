using UnityEngine;

public class Ice : Bullet
{
    protected override void DoDamage()
    {
        base.DoDamage();
        target.ApplyStatuses(new SlowStatus());
    }
}
