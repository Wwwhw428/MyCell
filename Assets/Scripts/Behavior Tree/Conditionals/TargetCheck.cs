using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Mine/Check")]
[TaskDescription("Check If Player In Min Agro Range Return Success.")]
public class TargetCheck : Conditional
{
    #region Public Variable
    
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Target Check Transform")]
    public SharedTransform TargetCheckTransform;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Min Agro Distance")]
    public SharedFloat MinAgroDistance;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("What Is Target")]
    public SharedLayerMask WhatIsTarget;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Face Direction")]
    public SharedInt FacingDirection;

    #endregion

    public override TaskStatus OnUpdate()
    {
        return CheckPlayerInMinAgroRange() ? TaskStatus.Success : TaskStatus.Failure;
    }

    public bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(TargetCheckTransform.Value.position, Vector2.right * FacingDirection.Value, MinAgroDistance.Value, WhatIsTarget.Value);
    }

    public override void OnReset()
    {
        TargetCheckTransform = null;
        MinAgroDistance = 0;
        WhatIsTarget = null;
    }
}
