using System;
using System.Collections.Generic;
using UnityEngine;

public class QuartalController : MonoBehaviour
{
    [SerializeField] private Material invinsibleMat;
    [SerializeField] private List<Material> allowedMat;

    private float currentValue;
    private Dictionary<int, List<MeshRenderer>> savedMesh = new Dictionary<int, List<MeshRenderer>>();

    private int amount;

    private void Awake()
    {
        var meshRenderer = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRendererCurrent in meshRenderer)
        {
            var currentMeshId = GetId(meshRendererCurrent);
            if (currentMeshId == -1) continue;
            if (savedMesh.ContainsKey(currentMeshId) == false)
            {
                Debug.Log("Save");
                savedMesh.Add(currentMeshId, new List<MeshRenderer>());
            }

            savedMesh[currentMeshId].Add(meshRendererCurrent);
        }
    }

    private int GetId(MeshRenderer meshRenderer)
    {
        for (var index = 0; index < allowedMat.Count; index++)
        {
            var material = allowedMat[index];
            if (material.name == RemoveInstance(meshRenderer.material.name)) return index;
        }

        return -1;
    }

    private string RemoveInstance(string text)
    {
        var newxt = "";
        foreach (var tx in text)
            if (tx == ' ')
                return newxt;
            else newxt += tx;
        return newxt;
    }

    private void Update()
    {
        if (currentValue == 0)
        {
            return;
        }

        if (currentValue > 0)
        {
            currentValue -= Time.deltaTime;
        }

        if (currentValue <= 0)
        {
            SetVisible(true);
            currentValue = 0;
        }
    }

    public void SetDisable()
    {
        SetVisible(false);
        currentValue = 1;
    }

    private void SetVisible(bool isVisible)
    {
        foreach (var meshRenderer in savedMesh)
        {
            foreach (var meshRendererCurrent in meshRenderer.Value)
            {
                meshRendererCurrent.material = isVisible ? allowedMat[meshRenderer.Key] : invinsibleMat;
            }
        }
    }
}