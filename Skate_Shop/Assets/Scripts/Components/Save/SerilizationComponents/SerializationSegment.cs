using Kuhpik;
using UnityEngine;

public abstract class SerializationSegment : MonoBehaviour
{
    public Bootstrap bootstrap { get; set; }

    public abstract void Load(SaveData saveData);
    public abstract void Save(SaveData saveData);
}