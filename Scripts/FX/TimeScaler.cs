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

        public void StartScale(TimeScaleParams scaleParams)
        {
            Debug.Assert(scaleParams, "TimeScaleParams is null.");
            Debug.Assert(scaleParams.TimeScale >= 0, "TimeScale should be >= 0.");

            CurrentTimeScaleParams = scaleParams;
            ElapsedDuration = 0.0f;
            EaseStep = CurrentTimeScaleParams.EaseInDuration > 0 ? 0.0f : 1.0f;

            Time.timeScale = 1.0f;
        }

        public void StartScale(float timeScale, float duration = -1.0f, float easeInDuration = 0.0f, float easeOutDuration = 0.0f)
        {
            TimeScaleParams scaleParams = new TimeScaleParams();
            scaleParams.TimeScale = timeScale;
            scaleParams.Duration = duration;
            scaleParams.EaseInDuration = easeInDuration;
            scaleParams.EaseOutDuration = easeOutDuration;

            StartScale(scaleParams);
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

            EaseStep = Mathf.Clamp01(EaseStep);
        }

        private void UpdateTimeScale()
        {
            Time.timeScale = Mathf.Lerp(1.0f, CurrentTimeScaleParams.TimeScale, EaseStep);
        }
    }
}