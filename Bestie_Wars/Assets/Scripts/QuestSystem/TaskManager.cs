using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : GameSystem
{
    [SerializeField] private GameObject claimIcon;
    [SerializeField] private Button claimButton;
    [SerializeField] private Button openMenuButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject panel;
    [SerializeField] private List<TaskController> taskControllers;
    [SerializeField] private Sprite sprite_1;
    [SerializeField] private Sprite sprite_2;
    [SerializeField] private Image claimImage;

    public override void OnInit()
    {
        foreach (var taskController in taskControllers)
        {
            taskController.Initialize(player);
        }
    }

    private void OnEnable()
    {
        claimButton.onClick.AddListener(ClaimAll);
        openMenuButton.onClick.AddListener(ShowMenu);
        closeButton.onClick.AddListener(DisableMenu);
    }

    private void OnDisable()
    {
        claimButton.onClick.RemoveListener(ClaimAll);
        openMenuButton.onClick.RemoveListener(ShowMenu);
        closeButton.onClick.RemoveListener(DisableMenu);
    }

    private void FixedUpdate()
    {
        claimIcon.gameObject.SetActive(taskControllers.Count(t => t.IsTaskFinish) != 0);
        claimButton.interactable = taskControllers.Count(t => t.IsTaskFinish) != 0;
        claimImage.sprite = taskControllers.Count(t => t.IsTaskFinish) != 0 ? sprite_1 : sprite_2;
    }

    public void ClaimAll()
    {
        foreach (var taskController in taskControllers)
        {
            if (taskController.IsTaskFinish) taskController.QuestFinish();
        }
    }

    public void ShowMenu()
    {
        panel.SetActive(true);
    }

    public void DisableMenu()
    {
        panel.SetActive(false);
    }
}