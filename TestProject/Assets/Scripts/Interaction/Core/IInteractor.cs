using System;
using Inventory;
using UnityEngine;

namespace Interaction.Core
{
    public interface IInteractor
    {
        public event Action<IInteractable, IInteractor> OnInteracted;
        ItemPicker ItemPicker { get; }
        Vector3 Position { get; }
    }
}