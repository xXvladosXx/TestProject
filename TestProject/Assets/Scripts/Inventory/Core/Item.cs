using UnityEngine;

namespace Inventory.Core
{
    public abstract class Item : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public ItemData ItemData { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public string Hash { get; set; }
    }
    
    public enum ItemType
    {
        Item = 1,
        Usable = Item | 1 << 2,
    }
}