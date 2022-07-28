using System.Collections.Generic;
using Camera;
using Interaction.Core;
using Interaction.Requirements;
using UnityEngine;

namespace Interaction
{
    public class RequirementInteractableObject : InteractableObject, IGameEnder, IHashable
    {
        [field: SerializeField] private List<ItemRequirement> ItemRequirements { get; set; }
        [field: SerializeField] private HashRequirement HashRequirement { get; set; }

        [TextArea]
        [SerializeField] private string _requirementText;

        [SerializeField] private float _shakeTime = .1f;
        [SerializeField] private float _shakeMagnitude = .1f;
        
        public string UncheckedRequirementText => _requirementText;

        public List<Requirement> Requirements { get; private set; } = new List<Requirement>();

        private void Start()
        {
            HashRequirement.Hash = Hash;
            Requirements.AddRange(ItemRequirements);
            Requirements.Add(HashRequirement);
        }

        public override void OnMouseClicked(IInteractor interactor)
        {
            base.OnMouseClicked(interactor);

            foreach (var requirement in Requirements)
            {
                if (!requirement.AllRequirementsChecked(interactor))
                {
                    CameraShaker.Instance.StartShaking(_shakeTime, _shakeMagnitude);
                }
            }
        }

        public string Hash { get; set; }
    }
}