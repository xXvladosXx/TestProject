using System.Collections.Generic;
using Interaction.Core;
using Interaction.Requirements;
using UnityEngine;

namespace Interaction
{
    public class RequirementInteractableObject : InteractableObject, IGameEnder
    {
        [field: SerializeField] private List<ItemRequirement> ItemRequirements { get; set; }

        [TextArea]
        [SerializeField] private string _requirementText;

        public string UncheckedRequirementText => _requirementText;

        public List<Requirement> Requirements { get; private set; } = new List<Requirement>();

        private void Start()
        {
            Requirements.AddRange(ItemRequirements);
        }
    }
}