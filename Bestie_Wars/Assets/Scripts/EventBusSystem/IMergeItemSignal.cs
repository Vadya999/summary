using System.Collections;
using UnityEngine;

namespace EventBusSystem
{
    public interface ISpawnEffectSignal : IGlobalSubscriber
    {
        Transform SpawnEffect(EffectType effectType, Transform attach, bool isAttach = true);
        void SpawnEffect(EffectType effectType, Transform position);
    }

    public interface IUpdateMoney : IGlobalSubscriber
    {
        void UpdateMoney();
    }

    public interface IBoughtNewZone : IGlobalSubscriber
    {
        void BoughtZone();
    }
    public interface IRecalculateAllShopPanelSignal : IGlobalSubscriber
    {
        void Recalculate();
    }

    public interface IFreeCarDetach : IGlobalSubscriber
    {
        void CarDetach();
    }
    
    public interface IVipeCarDetach : IGlobalSubscriber
    {
        void CarDetach();
    }public interface IDestroyCarDetach : IGlobalSubscriber
    {
        void CarDetach();
    }
}