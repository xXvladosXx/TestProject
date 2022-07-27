using System;
using System.Globalization;
using TMPro;
using UI.Core;
using UnityEngine;
using Utilities;

namespace UI.TimeCounter
{
    public class TimeCounterUI : StaticUIElement
    {
        [SerializeField] private TextMeshProUGUI _time;

        public override void Init(UIData uiData)
        {
            base.Init(uiData);

            UIData.GameContext.OnGameStarted += StartCount;
        }

        private void StartCount()
        {
            UIData.GameContext.OnGameStarted -= StartCount;
        }

        private void Update()
        {
            var time = UIData.GameContext.Time;
            _time.text = TextFormatter.FormatToTwoDecimalAfterPoint(time);
        }
    }
}