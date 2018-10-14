using System.Collections;
using System.Collections.Generic;
using Timespawn.Core.Utils;
using UnityEngine;

namespace Timespawn.Core.FX
{
    public class TimeScaler : Singleton<TimeScaler>
    {
        private TimeScaleParams CurrentTimeScaleParams;

        private float ElapsedDuration;
        private float EaseStep;
        private float StartTimeScale = 1.0f;

        private void Update()
        {
            if (CurrentTimeScaleParams)
            {
                float duration = CurrentTimeScaleParams.Duration;
                bool isInfinite = duration < 0;
                if ((ElapsedDuration < duration) || isInfinite)
                {
                    UpdateEaseInOut();
                    UpdateTimeScale();

                    ElapsedDuration += Time.unscaledDeltaTime;

                    // End
                    if (ElapsedDuration >= duration)
                    {
                        Time.timeScale = 1.0f;
                    }
                }
            }
        }

        public void StartScaleTime(TimeScaleParams scaleParams)
        {
            Debug.Assert(scaleParams, "TimeScaleParams is null.");
            Debug.Assert(scaleParams.TimeScale >= 0, "TimeScale should be >= 0.");

            CurrentTimeScaleParams = scaleParams;
            ElapsedDuration = 0.0f;
            EaseStep = CurrentTimeScaleParams.EaseInDuration > 0 ? 0.0f : 1.0f;

            StartTimeScale = Time.timeScale;
        }

        public void StartScaleTime(float timeScale, float duration = -1.0f, float easeInDuration = 0.0f, float easeOutDuration = 0.0f)
        {
            TimeScaleParams scaleParams = ScriptableObject.CreateInstance<TimeScaleParams>();
            scaleParams.TimeScale = timeScale;
            scaleParams.Duration = duration;
            scaleParams.EaseInDuration = easeInDuration;
            scaleParams.EaseOutDuration = easeOutDuration;

            StartScaleTime(scaleParams);
        }

        private void UpdateEaseInOut()
        {
            float easeInDuration = CurrentTimeScaleParams.EaseInDuration;
            float easeOutDuration = CurrentTimeScaleParams.EaseOutDuration;
            if ((easeInDuration > 0) && (ElapsedDuration < easeInDuration))
            {
                // Ease in
                EaseStep += Time.unscaledDeltaTime / easeInDuration;
            }
            else if ((easeOutDuration > 0) && ((CurrentTimeScaleParams.Duration - ElapsedDuration) < easeOutDuration))
            {
                // Ease out
                EaseStep -= Time.unscaledDeltaTime / easeOutDuration;
            }

            // Reset StartTimeScale after fully eased in
            if (EaseStep >= 1.0f)
            {
                StartTimeScale = 1.0f;
            }

            EaseStep = Mathf.Clamp01(EaseStep);
        }

        private void UpdateTimeScale()
        {
            Time.timeScale = Mathf.Lerp(StartTimeScale, CurrentTimeScaleParams.TimeScale, EaseStep);
        }
    }
}