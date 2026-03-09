using System;
using UnityEngine;

public class GhostFactory : MonoBehaviour, IGhostFactory
{
    [SerializeField] private Material placeableMaterial;
    [SerializeField] private Material nonPlaceableMaterial;

    private GameObject placingObjectGhost;

    public GameObject SpawnNewGhostObject(GameObject modelPrefab)
    {
        if (placingObjectGhost != null) Destroy(placingObjectGhost);

        placingObjectGhost = Instantiate(modelPrefab);

        placingObjectGhost.layer = LayerMask.NameToLayer("Ignore Raycast");

        ApplyGhostMaterial(placeableMaterial);
        ApplyTrigger(placingObjectGhost);

        return placingObjectGhost;
    }

    public void SetPlaceable(bool isPlaceable)
    {
        if (isPlaceable)
        {
            ApplyGhostMaterial(placeableMaterial);
        }
        else
        {
            ApplyGhostMaterial(nonPlaceableMaterial);
        }
    }

    private void ApplyGhostMaterial(Material mat)
    {
        Renderer[] renderers = placingObjectGhost.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            Material[] ghostMats = new Material[renderer.materials.Length];
            Array.Fill(ghostMats, mat);
            renderer.materials = ghostMats;
        }
    }

    private void ApplyTrigger(GameObject obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                collider.isTrigger = true;
            }
        }
    }

    public float GetModelOffset()
    {
        return GetPivotToBottomOffset(placingObjectGhost);
    }

    float GetPivotToBottomOffset(GameObject ghost)
    {
        var renderers = ghost.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return 0f;

        var combined = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
            combined.Encapsulate(renderers[i].bounds);

        // local y position of the lowest bound point
        float localMinY = ghost.transform.InverseTransformPoint(combined.min).y;
        return -localMinY; // distance from pivot to bottom
    }
}
