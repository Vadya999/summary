using UnityEngine;

namespace Source.Scripts.Components
{
    public class MoneyComponent : DragComponent
    {
        [field:SerializeField] public int CashIndex { get; private set; }
    }
}