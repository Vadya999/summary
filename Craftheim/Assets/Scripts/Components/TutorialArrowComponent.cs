using DG.Tweening;
using Kuhpik;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class TutorialArrowComponent : MonoBehaviour
    {
        [SerializeField] private float distanceForTarget = 8f;
        [SerializeField] private float moveDuration = 0.3f;
        [SerializeField] private Transform target;

        private Transform playerTransform;
        private float arrowTargetYPos;
        private bool nearThePlayer;
        private bool switchnig;

        private void Update()
        {
            if (target != null && target.gameObject.activeSelf)
            {
                playerTransform = Bootstrap.Instance.GameData.playerComponent.transform;
                
                if (Vector3.Distance(target.position, playerTransform.position) > distanceForTarget)
                {        
                    if (switchnig) return;

                    MoveToPlayer();
                    transform.LookAt(target, Vector3.up);
                }
                else
                {
                    if (switchnig) return;
                    
                    MoveToTarget();
                }
            }
        }

        public void SetTarget(Transform targetTransform, float arrowYPosTarget = 3)
        {
            if (targetTransform != null)
            {
                arrowTargetYPos = arrowYPosTarget;
                target = targetTransform;
                playerTransform = Bootstrap.Instance.GameData.playerComponent.transform;
                gameObject.SetActive(true);  
                
                var needMoveToPlayer = Vector3.Distance(target.position, playerTransform.position) > distanceForTarget;
                if (needMoveToPlayer) MoveToPlayer(true);
                else MoveToTarget(true);
            }
        }

        public void Deactivate()
        {
            transform.parent = null;
            target = null;
            gameObject.SetActive(false);
            YoYo(false);
        }

        private void YoYo(bool status)
        {
            DOTween.Kill(transform);
            if (status) transform.DOMoveY(transform.position.y + 0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        private void MoveToPlayer(bool anyway = false)
        {
            if (!nearThePlayer || anyway)
            {
                switchnig = true;
                nearThePlayer = true;
                transform.parent = playerTransform;
                
                var anim = DOTween.Sequence();
                anim.Append(transform.DODynamicLookAt(target.position, moveDuration));
                anim.Join(transform.DOLocalMove(new Vector3(0,4,0), moveDuration));
                anim.OnComplete(() =>
                {
                    YoYo(false);
                    switchnig = false;
                });
            }
        }
        
        private void MoveToTarget(bool anyway = false)
        {
            if (nearThePlayer || anyway)
            {
                switchnig = true;
                nearThePlayer = false;
                transform.parent = target;

                var anim = DOTween.Sequence();
                anim.Append(transform.DOLocalRotate(new Vector3(90,0,0), moveDuration));
                anim.Join(transform.DOLocalMove(new Vector3(0,arrowTargetYPos,0), moveDuration));
                anim.OnComplete(() =>
                {
                    YoYo(true);
                    switchnig = false;
                });
            }
        }
    }
}