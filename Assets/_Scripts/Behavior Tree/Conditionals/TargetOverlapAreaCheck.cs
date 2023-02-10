using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

[TaskCategory("Mine")]
[TaskDescription("FollowTarget.")]
public class TargetOverlapAreaCheck : Conditional
{
    public SharedCollisionScene CollisionScene;

    public override TaskStatus OnUpdate()
    {
        return CollisionScene.Value.TargetOverlapArea ? TaskStatus.Success : TaskStatus.Failure;
    }

    public override void OnReset()
    {
        CollisionScene = null;
    }
}
