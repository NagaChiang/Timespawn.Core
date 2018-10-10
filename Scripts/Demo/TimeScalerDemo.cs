using System.Collections;
using System.Collections.Generic;
using Timespawn.Core.FX;
using UnityEngine;

public class TimeScalerDemo : MonoBehaviour
{
    [SerializeField] private TimeScaleParams SlowMotionParams;
    [SerializeField] private TimeScaleParams FreezeParams;

    public void SlowMotion()
    {
        TimeScaler.Instance().StartScale(SlowMotionParams);
    }

    public void Freeze()
    {
        TimeScaler.Instance().StartScale(FreezeParams);
    }
}
