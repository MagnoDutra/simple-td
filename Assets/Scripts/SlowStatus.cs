using UnityEngine;

public class SlowStatus : IStatus
{
    private float originalSpeed;
    private float duration = 5;
    private float initialDuration = 5;

    public void Apply(Enemy target)
    {
        originalSpeed = target.Speed;
        target.Speed = originalSpeed * 0.5f;
    }

    public void Tick(Enemy target, float deltaTime)
    {
        duration -= deltaTime;

        if (duration <= 0)
        {
            target.Speed = originalSpeed;
            target.RemoveStatus(this);
        }
    }

    public void Reapply()
    {
        duration = initialDuration;
    }
}
