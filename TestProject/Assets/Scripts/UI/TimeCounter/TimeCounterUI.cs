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

        private bool _startCount;
        
        public override void Init(UIData uiData)
        {
            base.Init(uiData);

            _time.text = UIData.SaveContext.GetBestTime();
            
            UIData.GameContext.OnGameStarted += StartCount;
        }

        private void StartCount()
        {
            UIData.GameContext.OnGameStarted -= StartCount;

            _startCount = true;
        }

        private void Update()
        {
            if (_startCount)
            {
                var time = UIData.GameContext.Time;
                _time.text = TextFormatter.FormatToTwoDecimalAfterPoint(time);
            }
        }
    }
}