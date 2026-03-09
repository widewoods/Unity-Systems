using UnityEngine;

public interface IPlacementResolver
{
    public PlacementResult GetPlacement(RaycastHit hit, float offset);
}

public struct PlacementResult
{
    public bool CanPlace { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }

    public PlacementResult(bool canPlace, Vector3 pos, Quaternion rot)
    {
        CanPlace = canPlace;
        Position = pos;
        Rotation = rot;
    }
}
