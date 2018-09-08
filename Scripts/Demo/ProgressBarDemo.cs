using System;
using System.Collections;
using System.Collections.Generic;
using Timespawn.Core.UI;
using UnityEngine;

namespace Timespawn.Core.Demo
{
    public class ProgressBarDemo : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float ValueMax = 100.0f;
        [SerializeField] private Vector2 RandomValueRange = new Vector2(10.0f, 80.0f);

        [Header("UI")]
        [SerializeField] private ProgressBar Bar;

        private float Value = 0.0f;

        private void Awake()
        {
            Value = ValueMax;
        }

        private void Start()
        {
            Bar.SetPercent(Value / ValueMax);
        }

        public void IncreaseProgressButton_OnClick()
        {
            float valueChange = UnityEngine.Random.Range(RandomValueRange.x, RandomValueRange.y);
            Value = Mathf.Clamp(Value + valueChange, 0.0f, ValueMax);
            Bar.SetPercent(Value / ValueMax);
        }

        public void DecreaseProgressButton_OnClick()
        {
            float valueChange = UnityEngine.Random.Range(RandomValueRange.x, RandomValueRange.y);
            Value = Mathf.Clamp(Value - valueChange, 0.0f, ValueMax);
            Bar.SetPercent(Value / ValueMax);
        }
    }
}