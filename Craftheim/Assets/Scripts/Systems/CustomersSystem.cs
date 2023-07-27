using DG.Tweening;
using Kuhpik;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class CustomersSystem : GameSystem
    {
        [SerializeField] private float customerDownPos = -40f;
        [SerializeField] private float customerUpPos = -13.7f;
        [SerializeField] private float animTime = 1.5f;
        
        public override void OnInit()
        {
            Supyrb.Signals.Get<NewCustomerSignal>().AddListener(CreateCustomer);
            CreateCustomer(true);
        }

        private void CreateCustomer(bool needFast)
        {
            var anim = DOTween.Sequence();
            anim.Append(game.customerComponent.transform.DOMoveY(customerDownPos, needFast ? 0 : animTime));
            anim.AppendCallback(game.customerComponent.SetRandomModel);
            anim.Append(game.customerComponent.transform.DOMoveY(customerUpPos, animTime).SetEase(Ease.OutSine));
            anim.OnComplete(() => Supyrb.Signals.Get<LoadScoreboardSignal>().Dispatch());
        }
    }
}