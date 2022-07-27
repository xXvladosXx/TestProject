using System;
using Interaction;
using Interaction.Core;
using Inventory.Items;
using TMPro;
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

        [SerializeField] private TextMeshProUGUI _textOfInteraction;
    
        public void TryToInteract(IInteractable interactable, IInteractor interactor)
        {
            _textOfInteraction.text = interactable.InteractionText();
            
            DeactivateButtons();

            if (interactable is RequirementInteractableObject requirementInteractableObject)
            {
                foreach (var requirement in requirementInteractableObject.Requirements)
                {
                    if (!requirement.AllRequirementsChecked(interactor))
                    {
                        _textOfInteraction.text = requirementInteractableObject.UncheckedRequirementText;
                        
                        _ok.gameObject.SetActive(true);
                        _ok.onClick.AddListener(Hide);
                        return;
                    }
                }
            }
            
            _yes.gameObject.SetActive(true);
            _no.gameObject.SetActive(true);
            
            _yes.onClick.AddListener(() =>
            {
                Hide();

                interactable.Interact(interactor);

                if (interactable is IGameEnder)
                {
                    UIData.GameContext.EndGame();
                }
            });
            
            _no.onClick.AddListener(Hide);
        }

        private void DeactivateButtons()
        {
            _ok.gameObject.SetActive(false);
            _yes.gameObject.SetActive(false);
            _no.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _yes.onClick.RemoveAllListeners();
            _no.onClick.RemoveAllListeners();
            _ok.onClick.RemoveAllListeners();
        }
    }
}