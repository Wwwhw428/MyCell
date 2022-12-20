using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Mine/Check")]
[TaskDescription("Check if ledge in front return success.")]
public class LedgeCheck : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("The Ledge Check GameObject")]
    public SharedTransform LedgeCheck_Tran;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("What is Ledge")]
    public SharedLayerMask WhatIsLedge;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Ledge Check Distance")]
    public SharedFloat LedgeCheckDistance;

    public override TaskStatus OnUpdate()
    {
        return Physics2D.Raycast(LedgeCheck_Tran.Value.position, Vector2.down, LedgeCheckDistance.Value, WhatIsLedge.Value) ? TaskStatus.Success : TaskStatus.Failure;
    }

    public override void OnReset()
    {
        LedgeCheck_Tran = null;
        WhatIsLedge = null;
        LedgeCheckDistance = 0;
    }
}

