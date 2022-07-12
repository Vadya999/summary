using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyCar : MonoBehaviour//Базовый абстрактный класс - полиморфизм
{
    private float _price;
    private string _color;

    protected void CheckDiagnosis()
    {
        //Check diagnosis
    }

    protected MyCar()
    {
        _price = 10f;
    }
}

public class CarController//Отношения между сущностями, находятся на одном обьекте, могут взаимоисключаться, не связаны между собой
{
    private float _numberOfWheels;//инкапсуляция
    private float _wheelTuringRadius;

    private void WheelTurn(float _wheelTuringRadius)//инкапсуляция 
    {
        //Wheel turn method
    }
}

public class TripCar
{
    private Vector3 _startPos;
    private Vector3 _targetPos;

    private float _speed;

    private void Trip(Vector3 startPos, Vector3 targetPos, float speed)
    {
        //Trip method
    }
}

public class CarHealth
{
    private float _health;
    private float _damage;

    private void OnModifyHealth(float health, float damage)
    {
        //On damage method
    }
}

public class ByCar
{
    private List<MyCar> _cars;

    private float _money;

    private void ByCars(List<MyCar> cars, float money)
    {
        //By car
    }

    private void CarChoice(List<MyCar> cars)
    {
        //Choice car
    }
}

public class Maintenance : MyCar//Наследование сущности, построена небольшая иерархия 
{
    private CarHealth _carHealth;

    private float _repairNum;
    
    private void Check()
    {
        base.CheckDiagnosis();
    }

    private void CarRepair(float repairNum)
    {
        //Car repair
    }
}


