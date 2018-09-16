using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timespawn.Core.FX
{
    [CreateAssetMenu(fileName = "CameraShakeParams", menuName = "Timespawn/FX/CameraShakeParams", order = 0)]
    public class CameraShakeParams : ScriptableObject
    {
        [Header("Duration")]
        public float Duration;
        public float EaseInDuration;
        public float EaseOutDuration;

        [Header("Shake")]
        public ShakeParamsVector3 PositionShakeParams;
        public ShakeParamsVector3 RotationShakeParams;
    }

    [Serializable]
    public struct ShakeParamsVector3
    {
        public ShakeParams x;
        public ShakeParams y;
        public ShakeParams z;
    }

    [Serializable]
    public struct ShakeParams
    {
        public float Amplitude;
        public float Frequency;
    }
}