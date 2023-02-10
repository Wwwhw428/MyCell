using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine;

public class BehaviorAnimationHandler : MonoBehaviour
{
    public event Action OnAnimationFinished;

    public void AnimationFinishTrigger()
    {
        OnAnimationFinished?.Invoke();
    }
}
