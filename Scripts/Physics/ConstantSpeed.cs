using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timespawn.Core.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class ConstantSpeed : MonoBehaviour
    {
        private Rigidbody MyRigidbody;
        private float OriginalSpeed;

        private void Start()
        {
            MyRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            OriginalSpeed = MyRigidbody.velocity.magnitude;
        }

        private void LateUpdate()
        {
            MyRigidbody.velocity = OriginalSpeed * MyRigidbody.velocity.normalized;
        }
    }
}