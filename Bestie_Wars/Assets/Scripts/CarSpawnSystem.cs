using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawnSystem : GameSystem
{
    [SerializeField] private bool ShowSettingPosition;

    [SerializeField] [BoxGroup("Setting")] [ShowIf("ShowSettingPosition")]
    private int idShowSetting;

    [SerializeField] [BoxGroup("Setting")] [ShowIf("ShowSettingPosition")]
    private List<GameObject> spawnedCar;

    [SerializeField] private Transform tutorialSpawnPos;
    [SerializeField] private Transform tutorialCar;
    [SerializeField] private Transform tutorialVip;

    [SerializeField] private int amountEvents;
    [SerializeField] private List<CarEventController> m_carEventController;
    [SerializeField] private List<SpawnCarConfigurations> spawnCarConfigurationses;
    private CarAttacher carAttacher;
    private Podium Podium;
    private List<CarEventController> carEventControllers = new List<CarEventController>();

    public override void OnInit()
    {
        Podium = FindObjectOfType<Podium>();
        carAttacher = FindObjectOfType<CarAttacher>();
        StartCoroutine(SpawnCar());
        StartCoroutine(CheckSpawnedCar());
        StartCoroutine(EventGenerator());
        if (player.saveCar != null)
        {
            foreach (var saveCar in player.saveCar)
            {
                SpawnLegacyCar(saveCar);
            }
        }
    }

    public AttachCarController SpawnTutorialCar(bool isVip)
    {
        if (isVip == false)
        {
            var carTut = Instantiate(tutorialCar);
            carTut.transform.position = tutorialSpawnPos.position;
            carTut.transform.rotation = tutorialSpawnPos.rotation;
            return carTut.GetComponent<AttachCarController>();
        }
        else
        {
            var carTut = Instantiate(tutorialVip);
            carTut.transform.position = tutorialSpawnPos.position;
            carTut.transform.rotation = tutorialSpawnPos.rotation;
            return carTut.GetComponent<AttachCarController>();
        }
    }

    private void SpawnLegacyCar(int idCar)
    {
        foreach (var spawnCarConfigurationse in spawnCarConfigurationses)
        {
            foreach (var attachCarController in spawnCarConfigurationse.CarPrefabs)
            {
                if (attachCarController.ID == idCar)
                {
                    var newCar = Instantiate(attachCarController);
                    newCar.transform.position = new Vector3(-100, -100, -100); //position random
                    newCar.ToSave();
                    Podium.Attach(newCar);
                    return;
                }
            }
        }
    }

    private IEnumerator EventGenerator()
    {
        while (true)
        {
            if (player.IsTutorialFinish == false)
            {
                yield return new WaitForSeconds(2f);
                continue;
            }

            if (carEventControllers.Count < amountEvents)
            {
                var eventNew = Instantiate(m_carEventController[Random.Range(
                    0, m_carEventController.Count)]);
                carEventControllers.Add(eventNew);
            }
            else
            {
                RecalculateEvents();
            }

            yield return new WaitForSeconds(120f);
        }
    }

    private void RecalculateEvents()
    {
        foreach (var carEventController in carEventControllers)
        {
            if (carEventController.IsEventFinished)
            {
                carEventControllers.Remove(carEventController);
                Destroy(carEventController);
                RecalculateEvents();
                break;
            }
        }
    }

    public AttachCarController SpawnCar(int id)
    {
        foreach (var spawnCarConfigurationse in spawnCarConfigurationses)
        {
            foreach (var attachCarController in spawnCarConfigurationse.CarPrefabs)
            {
                if (attachCarController.ID == id)
                {
                    var newCar = Instantiate(attachCarController);
                    newCar.transform.position = new Vector3(-100, -100, -100); //position random
                    newCar.ToSave();
                    Podium.Attach(newCar);
                    return newCar;
                }
            }
        }

        return null;
    }

    private IEnumerator SpawnCar()
    {
        while (true)
        {
            if (player.IsTutorialFinish == false)
            {
                yield return new WaitForSeconds(2f);
                continue;
            }

            var randomAttached = spawnCarConfigurationses[Random.Range(0, spawnCarConfigurationses.Count)];
            if (randomAttached.MAXCar == randomAttached.MAXAmountSpawnCar) break;
            var activatedCarPos = randomAttached.SpawnPositions.Where(t =>
                randomAttached.SpawnedCar.ContainsKey(t) == false &&
                Vector3.Distance(t.transform.position, carAttacher.transform.position) > 7).ToList();
            if (activatedCarPos.Count == 0) continue;
            var randomPos = activatedCarPos[Random.Range(0, activatedCarPos.Count)];
            var newCar = Instantiate(randomAttached.CarPrefabs[Random.Range(0, randomAttached.CarPrefabs.Count)],
                transform);
            newCar.TransformObject.position = randomPos.position + new Vector3(0, 0.61f, 0);
            newCar.TransformObject.rotation =
                Quaternion.Euler(randomPos.rotation.eulerAngles + new Vector3(0, 90, 0));
            newCar.transform.DOShakeScale(0.3f);
            randomAttached.MAXAmountSpawnCar++;
            randomAttached.SpawnedCar.Add(randomPos, newCar);
        }
    }

    private IEnumerator CheckSpawnedCar()
    {
        while (true)
        {
            if (player.IsTutorialFinish == false)
            {
                yield return new WaitForSeconds(2f);
                continue;
            }

            var randomAttached = spawnCarConfigurationses[Random.Range(0, spawnCarConfigurationses.Count)];
            if (randomAttached.MAXCar == randomAttached.MAXAmountSpawnCar) yield return new WaitForSeconds(3f);
            var activatedCarPos = randomAttached.SpawnPositions.Where(t =>
                randomAttached.SpawnedCar.ContainsKey(t) == false &&
                Vector3.Distance(t.transform.position, carAttacher.transform.position) > 7).ToList();
            if (activatedCarPos.Count == 0) continue;
            var randomPos = activatedCarPos[Random.Range(0, activatedCarPos.Count)];
            var newCar = Instantiate(randomAttached.CarPrefabs[Random.Range(0, randomAttached.CarPrefabs.Count)],
                transform);
            newCar.TransformObject.position = randomPos.position + new Vector3(0, 0.61f, 0);
            newCar.TransformObject.rotation =
                Quaternion.Euler(randomPos.rotation.eulerAngles + new Vector3(0, 90, 0));
            newCar.transform.DOShakeScale(0.3f);
            randomAttached.MAXAmountSpawnCar++;
            randomAttached.SpawnedCar.Add(randomPos, newCar);
        }
    }

    [Button("Show spawn positions")]
    [ShowIf("ShowSettingPosition")]
    public void ShowPositions()
    {
        foreach (var spawnPosition in spawnCarConfigurationses[idShowSetting].SpawnPositions)
        {
            spawnPosition.gameObject.SetActive(true);
        }
    }

    [Button("Disable spawn positions")]
    [ShowIf("ShowSettingPosition")]
    public void DisablePositions()
    {
        foreach (var spawnPosition in spawnCarConfigurationses[idShowSetting].SpawnPositions)
        {
            spawnPosition.gameObject.SetActive(false);
        }
    }

    [Button("Test spawn car")]
    [ShowIf("ShowSettingPosition")]
    public void SpawnRandomCar()
    {
        DisablePositions();
        foreach (var spawnPosition in spawnCarConfigurationses[idShowSetting].SpawnPositions)
        {
            var randomCar = spawnCarConfigurationses[idShowSetting]
                .CarPrefabs[Random.Range(0, spawnCarConfigurationses[idShowSetting].CarPrefabs.Count)];
            var newCar = Instantiate(randomCar);
            newCar.TransformObject.position = spawnPosition.position + new Vector3(0, 0.61f, 0);
            newCar.TransformObject.rotation =
                Quaternion.Euler(spawnPosition.rotation.eulerAngles + new Vector3(0, 90, 0));
            spawnedCar ??= new List<GameObject>();
            spawnedCar.Add(newCar.gameObject);
        }
    }

    [Button("Test destroy car")]
    [ShowIf("ShowSettingPosition")]
    public void RemoveSpawnedCar()
    {
        foreach (var sp in spawnedCar)
        {
            DestroyImmediate(sp);
        }

        spawnedCar = null;
    }
}