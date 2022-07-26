using UnityEngine;

namespace Inventory.Core
{
    [CreateAssetMenu (menuName = "InventorySystem/Inventory")]
    public class Inventory : ScriptableObject
    {
        [field: SerializeField] public ItemContainer ItemContainer { get; private set; } = new ItemContainer(10);
    }
}