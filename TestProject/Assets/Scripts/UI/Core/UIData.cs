using System;
using Entities;
using Saving;

namespace UI.Core
{
    [Serializable]
    public class UIData
    {
        public Player Player { get; set; }
        public SaveContext SaveContext { get; set; }
        public GameContext GameContext { get; set; }
    }
}