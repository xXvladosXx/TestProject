using TMPro;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenuUI : StaticUIElement
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private TextMeshProUGUI _time;

        public override void Init(UIData uiData)
        {
            base.Init(uiData);
            _time.text = uiData.SaveContext.GetBestTime();
        }
    }
}