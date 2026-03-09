using UnityEngine;

public interface IGhostFactory
{
    public GameObject SpawnNewGhostObject(GameObject modelPrefab);

    public void SetPlaceable(bool isPlaceable);
    public float GetModelOffset();
}
