using System.Collections.Generic;
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

        public string UncheckedRequirementText => _requirementText;

        public List<Requirement> Requirements { get; private set; } = new List<Requirement>();

        private void Start()
        {
            HashRequirement.Hash = Hash;
            Requirements.AddRange(ItemRequirements);
            Requirements.Add(HashRequirement);
        }

        public string Hash { get; set; }
    }
}