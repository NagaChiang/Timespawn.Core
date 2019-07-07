using System;
using System.Collections;
using System.Collections.Generic;
using Timespawn.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Timespawn.Core.Demo
{
    public class ProgressBarDemo : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private int ValueMax = 100;
        [SerializeField] private Vector2Int RandomValueRange = new Vector2Int(5, 30);

        [Header("UI")]
        [SerializeField] private ProgressBar Bar;
        [SerializeField] private Text ProgressText;

        private int Value;

        private void Awake()
        {
            Value = ValueMax;
        }

        private void Start()
        {
            Bar.SetPercent((float)Value / ValueMax);
            SetProgressText(Value, ValueMax);
        }

        public void IncreaseProgressButton_OnClick()
        {
            int valueChange = UnityEngine.Random.Range(RandomValueRange.x, RandomValueRange.y);
            Value = Mathf.FloorToInt(Mathf.Clamp(Value + valueChange, 0.0f, ValueMax));
            Bar.SetPercent((float)Value / ValueMax);
            SetProgressText(Value, ValueMax);
        }

        public void DecreaseProgressButton_OnClick()
        {
            float valueChange = UnityEngine.Random.Range(RandomValueRange.x, RandomValueRange.y);
            Value = Mathf.FloorToInt(Mathf.Clamp(Value - valueChange, 0.0f, ValueMax));
            Bar.SetPercent((float)Value / ValueMax);
            SetProgressText(Value, ValueMax);
        }

        private void SetProgressText(int value, int valueMax)
        {
            ProgressText.text = value.ToString() + " / " + valueMax.ToString();
        }
    }
}