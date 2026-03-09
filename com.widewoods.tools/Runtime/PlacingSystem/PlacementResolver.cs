using UnityEngine;

public class PlacementResolver : MonoBehaviour, IPlacementResolver
{
    [Range(0f, 1f)]
    [SerializeField] private float dotThreshold;
    public PlacementResult GetPlacement(RaycastHit hit, float offset)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        Vector3 position = hit.point + hit.normal * offset;

        bool canBePlaced = true;
        if (Vector3.Dot(hit.normal, Vector3.up) < dotThreshold) canBePlaced = false;

        return new PlacementResult(canBePlaced, position, rotation);
    }
}
