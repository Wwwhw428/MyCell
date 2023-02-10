using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

[TaskCategory("Mine")]
[TaskDescription("FollowTarget.")]
public class Follow : Action
{
    #region Public Variable

    public SharedTransform Target;
    public SharedMovement Movement;
    public SharedCollisionScene CollisionScene;
    public SharedEnemyAnimationHandler AnimationHandler;
    public SharedString IdleAnimName;
    public SharedString MoveAnimName;

    #endregion

    private Animator _anim;
    private int _idleHashID;
    private int _moveHashID;
    private bool _animationFinished;

    public override void OnAwake()
    {
        _anim = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        _idleHashID = Animator.StringToHash(IdleAnimName.Value);
        _moveHashID = Animator.StringToHash(MoveAnimName.Value);
        AnimationHandler.Value.OnAnimationFinished += SetBool;
    }

    public override TaskStatus OnUpdate()
    {
        if (_animationFinished)
        {
            _animationFinished = false;
            _anim.SetBool(_idleHashID, false);

            if (!CollisionScene.Value.TargetOverlapArea)
                return TaskStatus.Failure;
            if (!CollisionScene.Value.TargetRaycastFront)
            {
                Movement.Value.Flip();
            }
            return TaskStatus.Success;
        }
        else
        {
            Movement.Value.SetVelocityX(0);
            _anim.SetBool(_idleHashID, true);
            _anim.SetBool(_moveHashID, false);

            return TaskStatus.Running;
        }
    }

    public override void OnEnd()
    {
        AnimationHandler.Value.OnAnimationFinished -= SetBool;
    }

    public override void OnReset()
    {
        Target = null;
        Movement = null;
        CollisionScene = null;
        IdleAnimName = null;
        MoveAnimName = null;
    }

    private void SetBool()
    {
        _animationFinished = true;
    }

}
