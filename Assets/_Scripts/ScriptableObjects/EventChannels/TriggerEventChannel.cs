﻿using System;
using UnityEngine;

namespace MyCell.SO.EventChannels
{
    [CreateAssetMenu(fileName = "newTriggerChannel", menuName = "Event Channels/Trigger")]
    public class TriggerEventChannel : EventChannelsSO<EventArgs>
    {
    }
}