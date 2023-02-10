using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

[TaskCategory("Mine")]
[TaskDescription("Patrol.")]
public class Patrol : Action
{
    #region Public Variable

    public SharedEnemyAnimationHandler AnimationHandler;
    public SharedEnemyData EnemyData;
    public SharedMovement Movement;
    public SharedCollisionScene collisionScene;
    public SharedString MoveAnimName;
    public SharedString IdleAnimName;

    #endregion

    private Animator _anim;
    private bool _wallFront;
    private bool _ledgeFront;
    private bool _idle;
    private bool _animationFinished;
    private int _moveHashID;
    private int _idleHashID;

    public override void OnAwake()
    {
        _anim = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        _moveHashID = Animator.StringToHash(MoveAnimName.Value);
        _idleHashID = Animator.StringToHash(IdleAnimName.Value);
        AnimationHandler.Value.OnAnimationFinished += SetBoolHandler;
    }

    public override void OnEnd()
    {
        AnimationHandler.Value.OnAnimationFinished -= SetBoolHandler;
    }

    public override TaskStatus OnUpdate()
    {
        DoCheck();

        if (!_idle)
        {

            if (_wallFront || !_ledgeFront)
            {
                Movement.Value.SetVelocityX(0);
                _anim.SetBool(_moveHashID, false);
                _anim.SetBool(_idleHashID, true);
                _idle = true;
            }
            else
            {
                _anim.SetBool(_moveHashID, true);
                Movement.Value.SetVelocityX(EnemyData.Value.MovementVelocity * Movement.Value.CurrentFaceDirection);
            }
        }
        else
        {
            if (_animationFinished)
            {
                _anim.SetBool(_idleHashID, false);
                Movement.Value.Flip();
                _idle = false;
                _animationFinished = false;
            }
        }
            return TaskStatus.Running;

    }
    public override void OnReset()
    {
        AnimationHandler = null;
        EnemyData = null;
        Movement = null;
        collisionScene = null;
        MoveAnimName = null;
        IdleAnimName = null;
    }

    private void DoCheck()
    {
        _wallFront = collisionScene.Value.WallFront;
        _ledgeFront = collisionScene.Value.LedgeVertical;
    }

    private void SetBoolHandler()
    {
        _animationFinished = true;
    }

}

