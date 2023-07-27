using UnityEngine;

namespace Source.Scripts.Components
{
    public class ItemComponent : DragComponent
    {
        public string Id { get; private set; }
        public bool IsScanned { get; private set; }

        public void SetItemId(string id)
        {
            Id = id;
        }

        public void Scanning()
        {
            IsScanned = true;
        }
    }
}