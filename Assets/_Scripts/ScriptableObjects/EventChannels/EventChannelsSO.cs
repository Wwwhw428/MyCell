using System;
using UnityEngine;

namespace MyCell.SO.EventChannels
{
    public abstract class EventChannelsSO<T> : ScriptableObject
    {
        [SerializeField]
        public event Action<object, T> OnEvent;

        public void RaiseEvent(object sender, T context)
        {
            OnEvent?.Invoke(sender, context);
        }
    }
}
