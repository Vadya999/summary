using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class PathComponent : MonoBehaviour
{
    public Vector3[] path { get; private set; }

    private void Awake()
    {
        path = transform.GetChildrens().Select(x => x.transform.position).ToArray();
    }

    private void OnDrawGizmos()
    {
        var childrens = transform.GetChildrens();
        if (childrens.Count() > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < childrens.Count() - 1; i++)
            {
                Gizmos.DrawLine(childrens.ElementAt(i).position, childrens.ElementAt(i + 1).position);
            }
        }
    }

    public static implicit operator Vector3[](PathComponent pathComponent)
    {
        return pathComponent.path;
    }

    public static implicit operator List<Vector3>(PathComponent pathComponent)
    {
        return pathComponent.path.ToList();
    }
}
