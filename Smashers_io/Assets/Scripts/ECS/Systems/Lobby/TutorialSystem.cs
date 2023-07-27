using Kuhpik;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialSystem : GameSystemWithScreen<TrapTaskScreen>
{
    [SerializeReference, SubclassSelector] private List<TutorialStep> _tutorialSteps;

    public readonly UnityEvent Started = new UnityEvent();
    public readonly UnityEvent<int, string> StepCompleted = new UnityEvent<int, string>();
    public readonly UnityEvent Completed = new UnityEvent();

    private TutorialStep currentStep => (_tutorialSteps != null && currentStepID < _tutorialSteps.Count) ? _tutorialSteps[currentStepID] : null;
    private int stepCount => _tutorialSteps.Count;
    public bool isTutorialActive => currentStep != null;
    public bool isTutorialHasText => !progress.completed && currentStep != null && !string.IsNullOrEmpty(currentStep.label);

    private TargetPointerSystem _targetPointerSystem;

    private int currentStepID
    {
        get => progress.stageID;
        set => progress.stageID = value;
    }

    private TutorialProgress progress => game.tutorialProgress;

    public override void OnGameStart()
    {
        _targetPointerSystem = GetSystem<TargetPointerSystem>();
    }

    public override void OnInit()
    {
        Started?.Invoke();
        currentStepID = Mathf.Min(currentStepID, _tutorialSteps.Count);
        if (_tutorialSteps.Count > 0 && !progress.completed)
        {
            StartCurrentStep();
        }
        else
        {
            progress.completed = true;
        }
    }

    private void Update()
    {
        if (_tutorialSteps is null) return;
        if (currentStep != null)
        {
            currentStep.Update();
        }
    }

    private void OnTutorialStepCompleted()
    {
        StopCurrentStep();
        currentStepID++;
        if (currentStepID < stepCount)
        {
            StartCurrentStep();
        }
        else
        {
            Completed?.Invoke();
            screen.SetText(null);
            progress.completed = true;
        }
    }

    private void StopCurrentStep()
    {
        currentStep.Completed -= OnTutorialStepCompleted;
        currentStep.ChangeStepID -= ForceStepID;
        currentStep.Exit();
        _targetPointerSystem.SetTarget(null);
        LogStep(currentStep);
    }

    private void StartCurrentStep()
    {
        currentStep.Completed += OnTutorialStepCompleted;
        currentStep.ChangeStepID += ForceStepID;
        currentStep.Enter();
        screen.SetText(currentStep.label);
    }

    private void ForceStepID(int stepID, bool relatively)
    {
        StopCurrentStep();
        var newStepID = relatively ? currentStepID + stepID : stepID;
        currentStepID = newStepID;
        StartCurrentStep();
    }

    private void LogStep(TutorialStep step)
    {
        if (step.hasSDKName)
        {
            var sdkID = GetStepID(_tutorialSteps.IndexOf(step));
            StepCompleted?.Invoke(sdkID + 1, step.sdkName);
        }

        int GetStepID(int stepID)
        {
            var result = 0;
            for (int i = 0; i < stepID; i++)
            {
                if (_tutorialSteps[i].hasSDKName)
                {
                    result++;
                }
            }
            return result;
        }
    }
}
