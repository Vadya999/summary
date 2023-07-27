using System;
using UnityEngine;

[Serializable]
public class SerializableTransform
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public SerializableTransform()
    {
        position = Vector3.zero;
        rotation = Vector3.zero;
        scale = Vector3.one;
    }

    public SerializableTransform(Transform origin)
    {
        position = origin.position;
        rotation = origin.rotation.eulerAngles;
        scale = origin.localScale;
    }

    public void ApplyTo(Transform transform)
    {
        transform.position = position;
        transform.eulerAngles = rotation;
        transform.localScale = scale;
    }
}
