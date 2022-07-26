using System;
using System.Globalization;
using UnityEngine;

namespace Saving
{
    [Serializable]
    public class SaveContext
    {
        public float CurrentTime { get; private set; }

        private bool _hasResult = false;
        
        private const string BEST_TIME = "BestTime";

        public void TryToRegisterBestResult(SaveData saveData)
        {
            CurrentTime = saveData.Time;

            if (!_hasResult)
            {
                PlayerPrefs.SetFloat(BEST_TIME, CurrentTime);
                return;
            }

            if (PlayerPrefs.GetFloat(BEST_TIME) > saveData.Time)
            {
                PlayerPrefs.SetFloat(BEST_TIME, CurrentTime);
            }
        }

        public string GetBestTime()
        {
            if (!_hasResult)
                return "Whoops!\n You don`t have a result.";

            return PlayerPrefs.GetFloat(BEST_TIME).ToString(CultureInfo.InvariantCulture);
        }

      
    }
}