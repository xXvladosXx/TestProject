using System;

namespace Interaction.Core
{
    [Serializable]
    public abstract class Requirement
    {
        public abstract bool AllRequirementsChecked(IInteractor interactor);
    }
}