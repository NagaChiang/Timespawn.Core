using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timespawn.Core.FX
{
    [CreateAssetMenu(fileName = "TimeScaleParams", menuName = "Timespawn/Time/TimeScaleParams", order = 0)]
    public class TimeScaleParams : ScriptableObject
    {
        [Header("Scale")]
        public float TimeScale;

        [Header("Duration")]
        public float Duration;
        public float EaseInDuration;
        public float EaseOutDuration;
    }
}