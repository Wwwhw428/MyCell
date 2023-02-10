using System;
using UnityEngine;

namespace MyCell.Interfaces
{
    public interface IHitbox
    {
        event Action<RaycastHit2D[]> OnDetected;
    }
}