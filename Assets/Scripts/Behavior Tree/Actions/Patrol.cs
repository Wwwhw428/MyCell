using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Mine")]
[TaskDescription("Patrol On Ground.")]
public class Patrol : Action
{
    #region Public Variable

    [BehaviorDesigner.Runtime.Tasks.Tooltip("Target GameObject")]
    public SharedGameObject TargetGameObject;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("The speed of the Move")]
    public SharedFloat Speed;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Face Direction")]
    public SharedInt CurrentFaceingDirection;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Wall Check Transform")]
    public SharedTransform WallCheck;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Ledge Check Transform")]
    public SharedTransform LedgeCheck;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Check Distance")]
    public SharedFloat CheckDistance;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("What Is Wall")]
    public SharedLayerMask WhatIsWall;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("What Is Ledge")]
    public SharedLayerMask WhatIsLedge;

    #endregion

    private Rigidbody2D _rb;
    private Vector2 _vector2WorkSpace;

    public override void OnStart()
    {
        _rb = TargetGameObject.Value.GetComponent<Rigidbody2D>();
    }

    public override TaskStatus OnUpdate()
    {
        if (WallFront || !LedgeFront)
        {
            Flip();
            _vector2WorkSpace.Set(0, 0);
        }
        else
        {
            _vector2WorkSpace.Set(Speed.Value * CurrentFaceingDirection.Value, _rb.velocity.y);
        }

        _rb.velocity = _vector2WorkSpace;

        return TaskStatus.Success;
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.Value.position, Vector2.right * CurrentFaceingDirection.Value, CheckDistance.Value, WhatIsWall.Value);
    }

    public bool LedgeFront
    {
        get => Physics2D.Raycast(LedgeCheck.Value.position, Vector2.down, CheckDistance.Value, WhatIsLedge.Value);
    }

    public void Flip()
    {
        TargetGameObject.Value.transform.Rotate(0f, 180f, 0f);
        CurrentFaceingDirection.Value *= -1;
    }

    public override void OnReset()
    {
        Speed = 0;
        CurrentFaceingDirection = 1;
        WallCheck = null;
        LedgeCheck = null;
        CheckDistance = 0;
    }

}

