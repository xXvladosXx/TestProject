using System;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Warning
{
    public class WarningUI : PopupUIElement
    {
        [SerializeField] private Button _yes;
        [SerializeField] private Button _no;
        [SerializeField] private Button _ok;

        public event Action OnYes;
        public event Action OnOk;
        public event Action OnNo;
        
        
    }
}