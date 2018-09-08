using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Timespawn.Core.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] protected float LerpDuration = 0.5f;

        [Header("Images")]
        [SerializeField] protected Image FillImage;

        protected float Percent;
        protected float LastPercent;
        protected float LerpStep;

        protected virtual void Update()
        {
            UpdateFillAmount();
        }

        public float GetPercent()
        {
            return Percent;
        }

        public void SetPercent(float percent)
        {
            LastPercent = Percent;
            Percent = percent;
            LerpStep = 0.0f;
        }

        private void UpdateFillAmount()
        {
            if (LerpStep >= 1.0f)
            {
                return;
            }

            LerpStep += Time.deltaTime / LerpDuration;
            FillImage.fillAmount = Mathf.Lerp(LastPercent, Percent, LerpStep);
        }
    }
}
