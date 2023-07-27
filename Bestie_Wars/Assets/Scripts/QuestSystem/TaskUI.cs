using System;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private Image sliderImage;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private Image spriteImage;
    [SerializeField] private Image checkBox;

    private TaskQuest currentTask;

    private void OnEnable()
    {
        UpdateTask();
    }

    public bool IsTaskUIClosed => currentTask == null;
    public void SetTaskQuest(TaskQuest taskQuest)
    {
        taskPanel.SetActive(true);
        currentTask = taskQuest;
        UpdateTask();
        taskQuest.ProcessUpdated += UpdateTask;
    }

    public void TaskFinish()
    {
        currentTask.ProcessUpdated -= UpdateTask;
        currentTask = null;
        taskPanel.SetActive(false);
    }

    public void UpdateTask()
    {
        if (currentTask == null) return;
        sliderImage.fillAmount = currentTask.ProcessDescription();
        textDescription.text = currentTask.Description;
        spriteImage.sprite = currentTask.Sprite;
        checkBox.enabled = sliderImage.fillAmount == 1;
    }
}