using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace UI.Core
{
    public class UIController : MonoBehaviour
    {
        protected UIData UIData;
        protected List<StaticUIElement> StaticUIElements = new List<StaticUIElement>();
        protected List<PopupUIElement> PopupUIElements  = new List<PopupUIElement>();
        
        public void Init(UIData uiData)
        {
            StaticUIElements = GetComponentsInChildren<StaticUIElement>().ToList();
            PopupUIElements = GetComponentsInChildren<PopupUIElement>().ToList();

            UIData = uiData;
            
            foreach (var popupUIElement in PopupUIElements)
            {
                popupUIElement.Init(UIData);
            }

            foreach (var staticUIElement in StaticUIElements)
            {
                staticUIElement.Init(UIData);
            }
        }

    }
}