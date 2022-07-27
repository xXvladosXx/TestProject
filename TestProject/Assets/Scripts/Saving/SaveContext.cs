using System;
using System.Globalization;
using UnityEngine;
using Utilities;

namespace Saving
{
    [Serializable]
    public class SaveContext
    {
        public float CurrentTime { get; private set; }

        private const string BEST_TIME = "BestTime";
        private const string HAS_BEST_TIME = "HasBestTime";

        public event Action OnBestTimeChanged;
        
        public void TryToRegisterBestResult(SaveData saveData)
        {
            CurrentTime = saveData.Time;

            if (PlayerPrefs.GetInt(HAS_BEST_TIME, 0) == 0)
            {
                PlayerPrefs.SetFloat(BEST_TIME, CurrentTime);
                PlayerPrefs.SetInt(HAS_BEST_TIME, 1);
                OnBestTimeChanged?.Invoke();
                
                return;
            }

            if (PlayerPrefs.GetFloat(BEST_TIME) > saveData.Time)
            {
                PlayerPrefs.SetFloat(BEST_TIME, CurrentTime);
                OnBestTimeChanged?.Invoke();
            }
        }

        public string GetBestTime()
        {
            if (PlayerPrefs.GetInt(HAS_BEST_TIME, 0) == 0)
                return "Whoops!\n You don`t have a result.";

            return TextFormatter.FormatToTwoDecimalAfterPoint(PlayerPrefs.GetFloat(BEST_TIME));
        }

      
    }
}