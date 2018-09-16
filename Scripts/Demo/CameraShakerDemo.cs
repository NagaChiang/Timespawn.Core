using System.Collections;
using System.Collections.Generic;
using Timespawn.Core.FX;
using UnityEngine;

namespace Timespawn.Core.Demo
{
    public class CameraShakerDemo : MonoBehaviour
    {
        [SerializeField] private CameraShakeParams SmallShakeParams;
        [SerializeField] private CameraShakeParams LargeShakeParams;

        public void SmallCameraShakeButton_OnClick()
        {
            CameraShaker.Instance().PlayShake(SmallShakeParams);
        }

        public void LargeCameraShakeButton_OnClick()
        {
            CameraShaker.Instance().PlayShake(LargeShakeParams);
        }
    }
}