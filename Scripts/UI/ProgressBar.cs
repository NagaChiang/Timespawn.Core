using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Timespawn.Core.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private bool EnableBackFillImage = true;
        [SerializeField] private float FrontLerpDuration = 0.2f;
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
        private float SinceLastSetPercent;
        private float BackFadeOutStep;

        private void Awake()
        {
            Debug.Assert(FrontLerpDuration > 0, "Duration should not be <= 0.");
            Debug.Assert(FrontFillImage, "Front image is null.");

            if (EnableBackFillImage)
            {
                Debug.Assert(BackDelayDuration > 0, "Duration should not be <= 0.");
                Debug.Assert(BackFadeOutDuration > 0, "Duration should not be <= 0.");
                Debug.Assert(BackFillImage, "Back image is null.");
            }

            if (!EnableBackFillImage)
            {
                SetBackFillImageAlpha(0.0f);
            }
        }
        
        private void Update()
        {
            UpdateFrontLerp();

            if (EnableBackFillImage)
            {
                UpdateBackFadeOut();
            }
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

            FrontLerpStep += Time.deltaTime / FrontLerpDuration;
            FrontFillImage.fillAmount = Mathf.Lerp(LastPercent, Percent, FrontLerpStep);
        }

        private void UpdateBackFadeOut()
        {
            if (SinceLastSetPercent < BackDelayDuration)
            {
                SinceLastSetPercent += Time.deltaTime;
                return;
            }

            BackFadeOutStep += Time.deltaTime / BackFadeOutDuration;
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
