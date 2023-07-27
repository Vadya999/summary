using DG.Tweening;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrapTaskScreen : UIScreen
{
    [SerializeField] private GameObject _completedRoot;
    [SerializeField] private GameObject _normalRoot;

    [SerializeField] private GameObject _checkMark;

    [SerializeField] private List<TMP_Text> _tasks;

    private string _currentText;

    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(_currentText) && !string.IsNullOrEmpty(text))
        {
            ShowNew(text);
            return;
        }
        if (!string.IsNullOrEmpty(_currentText) && string.IsNullOrEmpty(text))
        {
            ShowComplete();
            return;
        }
        if (!string.IsNullOrEmpty(_currentText) && !string.IsNullOrEmpty(text))
        {
            CompleteAndShowNext(text);
        }
    }

    private void ShowNew(string text)
    {
        ShowNormal();
        SetTextInternal(text);
        StopAllCoroutines();
        StartCoroutine(DoBounce(_normalRoot));
    }

    private void CompleteAndShowNext(string text)
    {
        StopAllCoroutines();
        StartCoroutine(CompleteAndShowNextRoutine(text));
    }

    private IEnumerator CompleteAndShowNextRoutine(string text)
    {
        ShowCompleted();
        yield return DoBounce(_checkMark);
        ShowNormal();
        SetTextInternal(text);
        StopAllCoroutines();
        StartCoroutine(DoBounce(_normalRoot));
    }

    private void ShowComplete()
    {
        StopAllCoroutines();
        StartCoroutine(ShowCompleteRoutine());
    }

    private IEnumerator ShowCompleteRoutine()
    {
        ShowCompleted();
        yield return DoBounce(_checkMark);
        Hide();
    }

    private void SetTextInternal(string text)
    {
        foreach (var task in _tasks)
        {
            task.text = text;
        }
        _currentText = text;
    }

    private void Hide()
    {
        _completedRoot.SetActive(false);
        _normalRoot.SetActive(false);
    }

    private void ShowNormal()
    {
        _completedRoot.SetActive(false);
        _normalRoot.SetActive(true);
    }

    private void ShowCompleted()
    {
        _completedRoot.SetActive(true);
        _normalRoot.SetActive(false);
    }

    private IEnumerator DoBounce(GameObject go)
    {
        go.transform.DOKill();
        yield return go.transform.DOShakeScale(0.5f, 0.5f, 10).WaitForCompletion();
    }
}
