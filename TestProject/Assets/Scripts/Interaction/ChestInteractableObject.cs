using Interaction.Core;
using UnityEngine;

namespace Interaction
{
    public class ChestInteractableObject : InteractableObject
    {
        [field: SerializeField] private float _timeToDestroyItem = .5f;
        [field: SerializeField] private ParticleSystem _particle;
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Interact(IInteractor interactor)
        {
            base.Interact(interactor);
            
            var particle = Instantiate(_particle, transform);
            Destroy(particle, _timeToDestroyItem);
        }
    }
}