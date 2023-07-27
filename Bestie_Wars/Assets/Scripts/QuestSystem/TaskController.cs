using System;
using System.Collections.Generic;
using EventBusSystem;
using Kuhpik;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class TaskController
{
    [SerializeField] private List<TaskQuest> taskQuests;
    [SerializeField] private List<TaskQuest> taskCycleQuest;
    [SerializeField] private TaskUI taskUI;
    [SerializeField] private int taskQuestId;

    private PlayerData player;
    private bool isInMainQuestLine;
    private TaskQuest currentTask;

    public bool IsTaskFinish => currentTask.IsTaskCompleted();

    public void Initialize(PlayerData playerData)
    {
        player = playerData;
        PrepareToStartQuest();
    }

    private void PrepareToStartQuest()
    {
        if (player.saveQuestId[taskQuestId] >= taskQuests.Count)
        {
            StartQuest(taskCycleQuest[Random.Range(0, taskCycleQuest.Count)]);
        }
        else
        {
            isInMainQuestLine = true;
            StartQuest(taskQuests[player.saveQuestId[taskQuestId]]);
        }
    }

    private void StartQuest(TaskQuest taskQuest)
    {
        taskUI.SetTaskQuest(taskQuest);
        taskQuest.EnableTask();
        taskUI.UpdateTask();
        currentTask = taskQuest;
    }

    public void QuestFinish()
    {
        Bootstrap.Instance.PlayerData.Money += currentTask.AmountCoin;
        EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
        currentTask = null;
        if (isInMainQuestLine)
        {
            player.saveQuestId[taskQuestId]++;
            isInMainQuestLine = false;
        }

        Bootstrap.Instance.SaveGame();
        taskUI.TaskFinish();
        PrepareToStartQuest();
    }
}