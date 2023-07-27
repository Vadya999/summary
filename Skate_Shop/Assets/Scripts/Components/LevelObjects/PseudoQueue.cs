using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class PseudoQueue : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private int _rowCount;

    private List<Vector3> _points;
    private List<(Transform, Vector3)> _usedPoints = new List<(Transform, Vector3)>();

    private void Awake()
    {
        _points = BuildPoints();
    }

    private List<Vector3> BuildPoints()
    {
        return Enumerable
            .Range(0, _rowCount)
            .SelectMany(x => GetLine(x))
            .ToList();

        List<Vector3> GetLine(float forward)
        {
            return Enumerable
                .Range(0, 4)
                .Select(x => _root.position + transform.forward * forward + transform.right * ((float)x).Remap(0, 4, -2, 2))
                .ToList();
        }
    }

    public Vector3 GetPoint(Component component)
    {
        return GetPoint(component.transform);
    }

    public Vector3 GetPoint(Transform transform)
    {
        var result = _points.OrderBy(x => Vector3.Distance(transform.position, x)).FirstOrDefault();
        _points.Remove(result);
        _usedPoints.Add((transform, result));
        return result;
    }

    public void ReturnPoint(Component component)
    {
        ReturnPoint(component.transform);
    }

    public void ReturnPoint(Transform transform)
    {
        var point = _usedPoints.First(x => x.Item1 == transform);
        _usedPoints.Remove(point);
        _points.Add(point.Item2);
    }

    private void OnDrawGizmos()
    {
        var points = BuildPoints();
        Gizmos.color = Color.cyan;
        points.ForEach(x => Gizmos.DrawCube(x, Vector3.one * 0.5f));
    }
}
