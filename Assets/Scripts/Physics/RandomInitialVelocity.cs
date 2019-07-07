using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timespawn.Core.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class RandomInitialVelocity : MonoBehaviour
    {
        [SerializeField] private Vector2 InitialSpeedRange;

        private void Start()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            Debug.Assert(rigidbody, "Rigidbody not found.");
            if (rigidbody)
            {
                rigidbody.velocity = Random.insideUnitSphere * Random.Range(InitialSpeedRange.x, InitialSpeedRange.y);
            }
        }
    }
}