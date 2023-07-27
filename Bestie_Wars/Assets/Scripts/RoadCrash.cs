using System.Collections.Generic;
using UnityEngine;

public class RoadCrash : CarEventController
{
    [SerializeField] private GameObject effect;
    [SerializeField] private List<AttachCarController> prefabs;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform effectPos_1;
    [SerializeField] private Transform effectPos_2;

    private AttachCarController car_First;
    private AttachCarController car_Second;
    private Transform effect_1;
    private Transform effect_2;

    private bool IsFirstCarFinish;
    private bool IsSecondCarFinish;

    public override bool IsEventFinished => IsFirstCarFinish && IsSecondCarFinish;

    private void Awake()
    {
        car_First = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
        car_Second = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
        effect_1 = Instantiate(effect).transform;
        effect_2 = Instantiate(effect).transform;

        car_First.transform.position = pos1.position;
        car_First.transform.rotation = pos1.rotation;

        car_Second.transform.position = pos2.position;
        car_Second.transform.rotation = pos2.rotation;

        effect_1.transform.position = effectPos_1.position;
        effect_1.transform.rotation = effectPos_1.rotation;

        effect_2.transform.position = effectPos_2.position;
        effect_2.transform.rotation = effectPos_2.rotation;

        var randomEffect = Random.Range(0, 2);
        if (randomEffect == 0)
        {
            effect_2.GetComponent<ParticleSystem>().Stop();
        }
    }

    private void Update()
    {
        if (car_First.IsAttach && IsFirstCarFinish == false)
        {
            IsFirstCarFinish = true;
            effect_1.GetComponent<ParticleSystem>().Stop();
        }

        if (car_Second.IsAttach && IsSecondCarFinish == false)
        {
            IsSecondCarFinish = true;
            effect_2.GetComponent<ParticleSystem>().Stop();
        }
    }
}