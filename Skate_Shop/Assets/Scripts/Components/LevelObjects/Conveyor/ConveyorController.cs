using DG.Tweening;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;
using UnityTools.Helpers;

public class ConveyorController : MonoBehaviour
{
    [SerializeField] private int _segmentID;
    [SerializeField] private SkateSkinComponent skateGOPrefabNotPaint;
    [SerializeField] private SkateData _skateToPaint;
    [SerializeField] private ConveyourUI _ui;
    [SerializeField] private BoxStackComponent _boxStack;
    [SerializeField] private ConveyorUpgrade _upgrades;

    [Header("Load Conveyor Settings")]
    [SerializeField] private float delayTranslateBoxesInArms;

    [Header("Instance skate")]
    [SerializeField] private Transform endPositionStackBoxes;
    [SerializeField] private Transform _skateStartPosition;
    [SerializeField] private float delayTranslateBoxesInLoadZone;
    [SerializeField] private float speedTranslateBoxesInLoadZone;
    [SerializeField] private Transform endPositionSkates;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform end1PositionSkates;
    [SerializeField] private Transform endPositionNotPaintSkate;

    public float speedTranslateSkatesInConveyor { get; set; } = 1;

    private bool isStartCoroutineTranslateBoxesInLoadZone;
    private bool isStartCoroutineBoxesDestroyAndInstanSkates;

    public ConveyorUpgrade upgrade => _upgrades;

    public SkateData data => _skateToPaint;
    public int segmentID => _segmentID;

    public EndConveyorLoad EndConveyorLoad;

    private BoxStackComponent playerBoxStack => GameData.player.boxRoot;

    public BoxStackComponent boxStack => _boxStack;

    private SkateComponent _skateOnConveyor;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        FindObjectsOfType<SkateShelfComponent>(true)
            .Where(x => x.requireSkate == _skateToPaint && x.segmentID == _segmentID)
            .ForEach(x => x.level = _upgrades.level);
        _ui.SetSkateImage(_skateToPaint.spriteConveyour[_upgrades.level]);
        _ui.UpdateCapacityText(_boxStack);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.HasComponent<PlayerComponent>())
        {
            if (!isStartCoroutineTranslateBoxesInLoadZone)
            {
                StartCoroutine(TranslateBoxesInLoadZone());
            }
        }
    }

    private void Update()
    {
        if (_boxStack.hasItem)
        {
            StartCoroutine(BoxesDestroyAndInstanceSkates());
        }
    }

    private IEnumerator TranslateBoxesInLoadZone()
    {
        isStartCoroutineTranslateBoxesInLoadZone = true;
        yield return new WaitForSeconds(delayTranslateBoxesInArms);

        if (playerBoxStack.hasItem && !_boxStack.isFull)
        {
            playerBoxStack.MoveToAnotherStack(_boxStack);
            UpdateUI();
        }

        StopCoroutine(TranslateBoxesInLoadZone());
        isStartCoroutineTranslateBoxesInLoadZone = false;
    }

    private IEnumerator BoxesDestroyAndInstanceSkates()
    {
        if (isStartCoroutineBoxesDestroyAndInstanSkates) yield break;
        isStartCoroutineBoxesDestroyAndInstanSkates = true;
        yield return new WaitForSeconds(delayTranslateBoxesInLoadZone);

        if (_boxStack.hasItem && EndConveyorLoad.skateStack.count < _boxStack.capacity)
        {
            var destroyBox = _boxStack.TakeItem();
            destroyBox.animator.SetBool("is-opened", true);
            destroyBox.transform.DOMove(endPositionStackBoxes.position, speedTranslateBoxesInLoadZone)
                .OnComplete(() => DestroyBoxAndInstance(destroyBox));

            UpdateUI();
        }
        StopCoroutine(BoxesDestroyAndInstanceSkates());
        isStartCoroutineBoxesDestroyAndInstanSkates = false;
    }

    private void DestroyBoxAndInstance(BoxComponent box)
    {
        var position = endPositionStackBoxes.position;
        var rotation = Quaternion.Euler(0, -90, 0);
        var skate = Instantiate(skateGOPrefabNotPaint, position, rotation);
        skate.transform.localScale = TweenHelper.zeroSize;
        skate.transform.DOScale(Vector3.one, 0.4f);
        skate.transform.DOMove(_skateStartPosition.position, 0.5f).OnComplete(() => MoveOnConveyor(skate, box));
    }

    private void MoveOnConveyor(SkateSkinComponent skate, BoxComponent box)
    {
        Destroy(box.gameObject);
        var trueSpeed = Vector3.Distance(skate.transform.position, endPositionNotPaintSkate.position) / speedTranslateSkatesInConveyor;
        skate.transform.DOMove(endPositionNotPaintSkate.position, trueSpeed).OnComplete(() => ChangeSkatePaint(skate));
    }

    private void ChangeSkatePaint(SkateSkinComponent notPaintSkate)
    {
        Destroy(notPaintSkate.gameObject);

        var skate = _skateToPaint.InstanceSkate(_upgrades.level);
        skate.transform.position = endPositionNotPaintSkate.position;
        skate.transform.rotation = Quaternion.Euler(0, -90, 0);
        skate.outline.UpdateMaterialProperties();
        _skateOnConveyor = skate;
        var trueSpeed = Vector3.Distance(skate.transform.position, end1PositionSkates.position) / speedTranslateSkatesInConveyor;
        skate.transform.DOMove(end1PositionSkates.position, trueSpeed)
            .OnComplete(() => TakeCurrentPosSkate(skate));
    }

    private void TakeCurrentPosSkate(SkateComponent skate)
    {
        EndConveyorLoad.skateStack.AddToStack(skate);
    }

    public void UpdateSkatesOnLevel()
    {
        FindObjectsOfType<SkateShelfComponent>()
            .Where(x => x.segmentID == _segmentID)
            .ForEach(x => UpdateSkates(x.skateListInSheif));

        UpdateSkates(EndConveyorLoad.skateStack.stack);
        UpdateSkates(GameData.player.skatesRoot.stack);
        _skateOnConveyor?.SetLevel(_upgrades.level);

        void UpdateSkates(IEnumerable<SkateComponent> shelf)
        {
            shelf.Where(x => x.data == _skateToPaint).ForEach(x => x.SetLevel(_upgrades.level));
        }
    }
}
