using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine;

namespace MyCell.Enemy
{
    public class EnemyAnimationHandler : MonoBehaviour
    {
        public event Action OnAnimationFinished;
       
        public void AnimationFinishTrigger()
        {
            OnAnimationFinished?.Invoke();
        }
    }
}
