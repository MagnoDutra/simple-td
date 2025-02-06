using UnityEngine;

public interface IStatus
{
    void Apply(Enemy target);
    void Tick(Enemy target, float deltaTime);
}
