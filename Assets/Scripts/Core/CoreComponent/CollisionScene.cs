using UnityEngine;

namespace MyCell.Core.CoreComponent
{
    public class CollisionScene : CoreComponent
    {
        private Movement Movement
        {
            get => movement ?? core.GetCoreComponent(ref movement);
        }
        private Movement movement;
        #region Check Transform

        public Transform GroundCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_groundCheck, core.transform.parent.name);
            private set => _groundCheck = value;
        }
        public Transform WallCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_wallCheck, core.transform.parent.name);
            private set => _wallCheck = value;
        }
        public Transform LedgeCheckHorizontal
        {
            get => GenericNotImplementedError<Transform>.TryGet(_ledgeCheckHorizontal, core.transform.parent.name);
            private set => _ledgeCheckHorizontal = value;
        }
        public Transform LedgeCheckVertical
        {
            get => GenericNotImplementedError<Transform>.TryGet(_ledgeCheckVertical, core.transform.parent.name);
            private set => _ledgeCheckVertical = value;
        }
        public Transform CeilingCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_ceilingCheck, core.transform.parent.name);
            private set => _ceilingCheck = value;
        }
        public float GroundCheckRadius { get => _groundCheckRaidus; set => _groundCheckRaidus = value; }
        public float WallCheckDistance { get => _wallCheckDistance; set => _wallCheckDistance = value; }
        public LayerMask WhatIsGround { get => _whatIsGround; set => _whatIsGround = value; }

        [SerializeField]
        private Transform _groundCheck;
        [SerializeField]
        private Transform _wallCheck;
        [SerializeField]
        private Transform _ledgeCheckHorizontal;
        [SerializeField]
        private Transform _ledgeCheckVertical;
        [SerializeField]
        private Transform _ceilingCheck;
        [SerializeField]
        private float _groundCheckRaidus;
        [SerializeField]
        private float _wallCheckDistance;
        [SerializeField]
        private LayerMask _whatIsGround;

        #endregion

        public bool Ground
        {
            get => Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
        }

        public bool WallFront
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.CurrentFaceDirection, WallCheckDistance, WhatIsGround);
        }

        public bool Ceiling
        {
            get => Physics2D.OverlapCircle(CeilingCheck.position, GroundCheckRadius, WhatIsGround);
        }

        public bool LedgeHorizontal
        {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.CurrentFaceDirection, WallCheckDistance, WhatIsGround);
        }

        public bool LedgeVertical
        {
            get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, WallCheckDistance, WhatIsGround);
        }

        //public bool WallBack
        //{
        //    get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
        //}

    }
}
