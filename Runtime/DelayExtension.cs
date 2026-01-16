using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

namespace ShitUtils
{
    public static class DelayExtension
    {
        private static List<DelayStruct> _delayList;
        private static List<FrameDelayStruct> _frameDelayList;

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            _delayList = new();
            _frameDelayList = new();

            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            var list = new List<PlayerLoopSystem>(playerLoop.subSystemList)
            {
                new PlayerLoopSystem
                {
                    type = typeof(DelayExtension),
                    updateDelegate = Update
                }
            };

            playerLoop.subSystemList = list.ToArray();
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        // ===== TIME DELAY =====
        public static void Delay(Action action, float seconds)
        {
            if (seconds <= 0)
            {
                action?.Invoke();
                return;
            }

            _delayList.Add(new DelayStruct(action, seconds));
        }

        // ===== FRAME DELAY =====
        public static void FrameDelay(Action action, int frames)
        {
            if (frames <= 0)
            {
                action?.Invoke();
                return;
            }

            _frameDelayList.Add(new FrameDelayStruct(action, frames));
        }

        private static void Update()
        {
            UpdateTimeDelays();
            UpdateFrameDelays();
        }

        private static void UpdateTimeDelays()
        {
            for (int i = _delayList.Count - 1; i >= 0; i--)
            {
                var delay = _delayList[i];
                delay.RemainSeconds -= Time.deltaTime;

                if (delay.RemainSeconds <= 0)
                {
                    delay.Action?.Invoke();
                    _delayList.RemoveAt(i);
                }
                else
                {
                    _delayList[i] = delay;
                }
            }
        }

        private static void UpdateFrameDelays()
        {
            for (int i = _frameDelayList.Count - 1; i >= 0; i--)
            {
                var delay = _frameDelayList[i];
                delay.RemainingFrames--;

                if (delay.RemainingFrames <= 0)
                {
                    delay.Action?.Invoke();
                    _frameDelayList.RemoveAt(i);
                }
                else
                {
                    _frameDelayList[i] = delay;
                }
            }
        }

        private struct DelayStruct
        {
            public Action Action;
            public float RemainSeconds;

            public DelayStruct(Action action, float seconds)
            {
                Action = action;
                RemainSeconds = seconds;
            }
        }

        private struct FrameDelayStruct
        {
            public Action Action;
            public int RemainingFrames;

            public FrameDelayStruct(Action action, int frames)
            {
                Action = action;
                RemainingFrames = frames;
            }
        }
    }
}
