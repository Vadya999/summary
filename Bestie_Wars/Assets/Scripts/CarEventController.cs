using System;
using System.Collections;
using UnityEngine;

public abstract class CarEventController : MonoBehaviour
{
    public virtual bool IsEventFinished { get; protected set; }
}