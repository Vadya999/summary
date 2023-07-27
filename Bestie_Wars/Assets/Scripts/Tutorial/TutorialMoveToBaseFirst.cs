using System;
using System.Linq;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;
using UnityEngine;

[Serializable]
public class TutorialMoveToBaseFirst : MoveToTargetStep
{
    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.EnterRoadMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(1,"move_to_base_1");
    }
}

[Serializable]
public class TutorialMoveToBuyParkingZone : MoveToTargetStep
{
    public override void OnUpdate()
    {
        if (Bootstrap.Instance.GameData.IsBuyingParking)
        {
            Complete();
        }
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.UnlockParking;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(2,"buying_parking_area");
    }
}

[Serializable]
public class TutorialMoveToRoadFirst : MoveToTargetStep
{
    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.RoadMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(3,"move_to_road_1");
    }
}


public class TutorialOpenQuestPannel : TutorialStep
{
    public override void OnUpdate()
    {
        throw new NotImplementedException();
    }

    protected override void OnBegin()
    {
        throw new NotImplementedException();
    }

    protected override void OnComplete()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class TutorialMoveToCarFirst : MoveToTargetStep
{
    private AttachCarController m_attachCarController;

    public override void OnUpdate()
    {
        if (m_attachCarController.IsAttach)
        {
            Complete();
        }
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        m_attachCarController = Bootstrap.Instance.GetSystem<CarSpawnSystem>().SpawnTutorialCar(false);
        target = m_attachCarController.Car.transform;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(4,"come_to_car_1");
    }
}

[Serializable]
public class TutorialMoveToBaseSecond : MoveToTargetStep
{
    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.EnterRoadMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(5,"move_to_base_2");
    }
}

[Serializable]
public class TutorialSellCar : MoveToTargetStep
{
    private AttachCarController m_attachCarController;

    public override void OnUpdate()
    {
        if (Vector3.Distance(player.transform.position, target.transform.position) < distance &&
            m_attachCarController.IsAttach == false)
        {
            Complete();
        }
    }

    protected override void OnComplete()
    {
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(6,"sell_car1");
        base.OnComplete();
        Bootstrap.Instance.GameData.IsSelling
            = false;
    }

    protected override void SetTarget()
    {
        Bootstrap.Instance.GameData.IsSelling
            = true;
        m_attachCarController = GameObject.FindObjectsOfType<AttachCarController>().Where(t => t.IsAttach).ToList()[0];
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.ParkingZone;
    }
}

[Serializable]
public class WaitMoney : TutorialStep
{
    public override void OnUpdate()
    {
        if (Bootstrap.Instance.PlayerData.Money >= 50)
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        Bootstrap.Instance.GameData.IsSelling = true;
    }

    protected override void OnComplete()
    {
        Bootstrap.Instance.GameData.IsSelling = false;
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(6,"take_money");
    }
}

[Serializable]
public class MoveToDestoryBuying : MoveToTargetStep
{
    public override void OnUpdate()
    {
        if (Bootstrap.Instance.GameData.IsBuyingDestroy)
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        ActivateTutorialObject.Instance.BuyDestroyZone.gameObject.SetActive(true);
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(7,"buying_destroyer");
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.BuyDestroyZone.transform;
    }
}

[Serializable]
public class TutorialMoveToRoadSecond : MoveToTargetStep
{
    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.RoadMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(8,"move_to_road_2");
    }
}

[Serializable]
public class TutorialMoveToCarSecond : MoveToTargetStep
{
    private AttachCarController m_attachCarController;

    public override void OnUpdate()
    {
        if (m_attachCarController.IsAttach)
        {
            Complete();
        }
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        m_attachCarController = Bootstrap.Instance.GetSystem<CarSpawnSystem>().SpawnTutorialCar(false);
        target = m_attachCarController.Car.transform;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(9,"come_to_car_2");
    }
}

[Serializable]
public class TutorialMoveToBaseThird : MoveToTargetStep
{
    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.EnterRoadMarker;
    }

    protected override void OnComplete()
    {
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(10,"move_to_base_3");
    }
}

[Serializable]
public class TutorialMoveToDestroy : MoveToTargetStep
{
    private AttachCarController m_attachCarController;

    protected override void OnBegin()
    {
        base.OnBegin();
        Bootstrap.Instance.GameData.IsDestroy = true;
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(player.transform.position, target.transform.position) < distance &&
            m_attachCarController.IsAttach == false)
        {
            Complete();
        }
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.GameData.IsDestroy = true;
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(11,"waiting_for_destroyed_car");
    }

    protected override void SetTarget()
    {
        m_attachCarController = GameObject.FindObjectsOfType<AttachCarController>().Where(t => t.IsAttach).ToList()[0];
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.ParkingZone;
    }
}

[Serializable]
public class WaitDestroyCar : TutorialStep
{
    private AttachCarController attachCarController;

    public override void OnUpdate()
    {
        if (attachCarController.IsCanBeDestroy) Complete();
    }

    protected override void OnBegin()
    {
        Bootstrap.Instance.GameData.IsDestroy = true;
        attachCarController = GameObject.FindObjectOfType<AttachCarController>();
    }

    protected override void OnComplete()
    {
        Bootstrap.Instance.GameData.IsDestroy = false;
    }
}

[Serializable]
public class TakeDestroyCar : MoveToTargetStep
{
    private AttachCarController a;

    public override void OnUpdate()
    {
        if (a.IsAttach) Complete();
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        a = GameObject.FindObjectOfType<AttachCarController>();
        target = a.Car;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(12,"take_destroyed_car");
    }
}

[Serializable]
public class MoveToDestroy : MoveToTargetStep
{
    public override void OnUpdate()
    {
        if (Bootstrap.Instance.GetCurrentGamestateID() == GameStateID.CranController)
        {
            Complete();
        }
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.DestroyMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(13,"move_to_destroyer");
    }
}

[Serializable]
public class DestoryCarGuidFirst : TutorialStep
{
    private CranRotateController Controller;

    public override void OnUpdate()
    {
        if (Controller.IsFirstImageFinishTutorial) Complete();
    }

    protected override void OnBegin()
    {
        ActivateTutorialObject.Instance.CranFirst.SetActive(true);
        Controller = GameObject.FindObjectOfType<CranRotateController>();
    }

    protected override void OnComplete()
    {
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(14,"take_car_by_crane");
        ActivateTutorialObject.Instance.CranFirst.SetActive(false);
    }
}

[Serializable]
public class DestoryCarGuidSecond : TutorialStep
{
    private CranRotateController Controller;

    public override void OnUpdate()
    {
        if (Controller.IsSecondImageFinishTutorial) Complete();
    }

    protected override void OnBegin()
    {
        ActivateTutorialObject.Instance.CranSecond.SetActive(true);
        Controller = GameObject.FindObjectOfType<CranRotateController>();
    }

    protected override void OnComplete()
    {
        ActivateTutorialObject.Instance.CranSecond.SetActive(false);
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(15,"destroy_car");
    }
}

[Serializable]
public class MoveToBuyVip : MoveToTargetStep
{
    public override void OnUpdate()
    {
        if (Bootstrap.Instance.GameData.IsBuyingLegacy) Complete();
    }

    protected override void SetTarget()
    {
        ActivateTutorialObject.Instance.BuyLegacyZone.gameObject.SetActive(true);
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.LegacyMarker;    
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(16,"move_vip_area");
    }
}

[Serializable]
public class TutorialMoveToRoadThird : MoveToTargetStep
{
    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.RoadMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(17,"move_to_road_3");
    }
}


[Serializable]
public class TutorialMoveToCarThird : MoveToTargetStep
{
    private AttachCarController m_attachCarController;

    public override void OnUpdate()
    {
        if (m_attachCarController.IsAttach)
        {
            Complete();
        }
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        m_attachCarController = Bootstrap.Instance.GetSystem<CarSpawnSystem>().SpawnTutorialCar(true);
        target = m_attachCarController.Car.transform;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(18,"move_to_vip_car");
    }
}

[Serializable]
public class TutorialMoveToBaseFive : MoveToTargetStep
{
    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.EnterRoadMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(19,"move_to_base_5");
    }
}

[Serializable]
public class TutorialMoveToLegacy : MoveToTargetStep
{
    private AttachCarController m_attachCarController;

    protected override void OnBegin()
    {
        base.OnBegin();
        Bootstrap.Instance.GameData.IsLegacy = true;
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(player.transform.position, target.transform.position) < distance &&
            m_attachCarController.IsAttach == false)
        {
            Complete();
        }
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.GameData.IsLegacy = true;
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(20,"wait_vip_car");
    }

    protected override void SetTarget()
    {
        m_attachCarController = GameObject.FindObjectsOfType<AttachCarController>().Where(t => t.IsAttach).ToList()[0];
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.ParkingZone;
    }
}

[Serializable]
public class WaitLegacyCar : TutorialStep
{
    private AttachCarController attachCarController;

    public override void OnUpdate()
    {
        if (attachCarController.IsCanBeSave) Complete();
    }

    protected override void OnBegin()
    {
        Bootstrap.Instance.GameData.IsLegacy= true;
        attachCarController = GameObject.FindObjectOfType<AttachCarController>();
    }

    protected override void OnComplete()
    {
        Bootstrap.Instance.GameData.IsLegacy = false;
    }
}

[Serializable]
public class TakeLegacyCar : MoveToTargetStep
{
    private AttachCarController a;

    public override void OnUpdate()
    {
        if (a.IsAttach) Complete();
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        a = GameObject.FindObjectOfType<AttachCarController>();
        target = a.Car;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(21,"take_vip_car");
    }
}

[Serializable]
public class MoveToLegacyPos : MoveToTargetStep
{
    private AttachCarController a;

    public override void OnUpdate()
    {
        if (a.IsAttach == false)
        {
            Complete();
        }
    }

    protected override void SetTarget()
    {
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        a = GameObject.FindObjectOfType<AttachCarController>();
        target = ActivateTutorialObject.Instance.LegacyMarker;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(22,"put_vip_car_on_podium");
    }
}

[Serializable]
public class MoveToUpgrade : MoveToTargetStep
{
    public override void OnUpdate()
    {
        if (Bootstrap.Instance.GetCurrentGamestateID() == GameStateID.Menu) Complete();
    }

    protected override void SetTarget()
    {
        ActivateTutorialObject.Instance.UpgradeTrigger.gameObject.SetActive(true);
        player = GameObject.FindObjectOfType<AttachCarQueueController>().transform;
        target = ActivateTutorialObject.Instance.UpgradeMarker;
    }
}

[Serializable]
public class BuyFirstUpgrade : TutorialStep
{
    public override void OnUpdate()
    {
        if (Bootstrap.Instance.PlayerData.UpgadeLevel[UpgradeType.CarLevel] == 2) Complete();
    }

    protected override void OnBegin()
    {
        ActivateTutorialObject.Instance.Shop.SetActive(true);
        ActivateTutorialObject.Instance.CloseShop.SetActive(false);
    }

    protected override void OnComplete()
    {
        ActivateTutorialObject.Instance.Shop.SetActive(false);
        ActivateTutorialObject.Instance.CloseShop.SetActive(true);
        Bootstrap.Instance.PlayerData.IsTutorialFinish = true;
        Bootstrap.Instance.SaveGame();
        Bootstrap.Instance.PlayerData.TryToSaveTutorSteps(23,"buying_first_upgrade");
        HomaBelly.Instance.TrackDesignEvent($"tutorial_completed");
    }
}