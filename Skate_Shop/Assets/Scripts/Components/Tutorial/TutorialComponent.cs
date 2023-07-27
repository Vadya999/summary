using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialComponent : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private List<TutorialStep> _tutorialSteps;

    private TutorialStep currentStep => (_tutorialSteps != null && currentStepID < _tutorialSteps.Count) ? _tutorialSteps[currentStepID] : null;
    private int stepCount => _tutorialSteps.Count;

    private TutorialScreen screen => GameData.tutorialScreen;
    private TutorialPointer pointer => GameData.player.pointer;

    public bool completed => currentStepID >= stepCount;

    public int currentStepID { get; set; }

    private void Start()
    {
        if (!completed)
        {
            StartCurrentStep();
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
            SDKEvents.tutorial.TutorialCompleted();
        }
    }

    private void StopCurrentStep()
    {
        LogStepComplete();
        currentStep.Completed -= OnTutorialStepCompleted;
        currentStep.Exit();
        pointer.SetTarget(null);
        screen.SetTutorialText(string.Empty);
        currentStep.objectsToDisableOnStep.ForEach(x => x.enabled = true);
    }

    private void StartCurrentStep()
    {
        currentStep.Completed += OnTutorialStepCompleted;
        currentStep.Enter();
        screen.SetTutorialText(currentStep.discription);
        pointer.SetTarget(currentStep.target);
        currentStep.objectsToDisableOnStep.ForEach(x => x.enabled = false);
    }

    private void LogStepComplete()
    {
        if (!string.IsNullOrEmpty(currentStep.discription))
        {
            var trueIndex = _tutorialSteps.Where(x => !string.IsNullOrEmpty(x.discription)).ToList().IndexOf(currentStep);
            SDKEvents.tutorial.StepComplete(trueIndex, currentStep.discription);
        }
    }
}
