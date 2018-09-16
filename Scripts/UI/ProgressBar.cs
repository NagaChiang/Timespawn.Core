using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Timespawn.Core.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Front Image")]
        [SerializeField] private float FrontLerpDuration = 0.2f;

        [Header("Back Image")]
        [SerializeField] private float BackDelayDuration = 1.5f;
        [SerializeField] private float BackFadeOutDuration = 0.5f;

        [Header("Images")]
        [SerializeField] private Image FrontFillImage;
        [SerializeField] private Image BackFillImage;

        private float Percent;
        private float LastPercent;

        // Front
        private float FrontLerpStep;

        // Back
        private bool EnableBackFillImage;
        private float SinceLastSetPercent;
        private float BackFadeOutStep;

        private void Awake()
        {
            Debug.Assert(FrontFillImage, "Front image is null.");

            EnableBackFillImage = BackDelayDuration > 0;
            if (EnableBackFillImage)
            {
                Debug.Assert(BackFillImage, "Back delay duration is > 0, but back image is null.");
            }
            else
            {
                // Hide back image if it exists
                if (BackFillImage)
                {
                    SetBackFillImageAlpha(0.0f);
                }
            }
        }
        
        private void Update()
        {
            UpdateFrontLerp();
            UpdateBackFadeOut();
        }

        public float GetPercent()
        {
            return Percent;
        }

        public void SetPercent(float percent)
        {
            LastPercent = Percent;
            Percent = percent;

            // Front
            FrontLerpStep = 0.0f;

            // Back
            if (EnableBackFillImage)
            {
                if ((Percent >= LastPercent) || (SinceLastSetPercent >= BackDelayDuration))
                {
                    BackFillImage.fillAmount = Mathf.Max(Percent, LastPercent);
                }

                SinceLastSetPercent = 0.0f;
                BackFadeOutStep = 0.0f;
                SetBackFillImageAlpha(1.0f);
            }
        }

        private void UpdateFrontLerp()
        {
            if (FrontLerpStep >= 1.0f)
            {
                return;
            }

            if (FrontLerpDuration > 0)
            {
                FrontLerpStep += Time.deltaTime / FrontLerpDuration;
            }
            else // FrontLerpDuration <= 0
            {
                // Instant
                FrontLerpStep = 1.0f;
            }

            FrontFillImage.fillAmount = Mathf.Lerp(LastPercent, Percent, FrontLerpStep);
        }

        private void UpdateBackFadeOut()
        {
            if (!EnableBackFillImage)
            {
                return;
            }

            if (SinceLastSetPercent < BackDelayDuration)
            {
                SinceLastSetPercent += Time.deltaTime;
                return;
            }

            if (BackFadeOutDuration > 0)
            {
                BackFadeOutStep += Time.deltaTime / BackFadeOutDuration;
            }
            else // BackFadeOutDuration <= 0
            {
                // Instant
                BackFadeOutStep = 1.0f;
            }

            float alpha = Mathf.Lerp(1.0f, 0.0f, BackFadeOutStep);
            SetBackFillImageAlpha(alpha);
        }

        private void SetBackFillImageAlpha(float alpha)
        {
            Color color = BackFillImage.color;
            color.a = alpha;
            BackFillImage.color = color;
        }
    }
}
