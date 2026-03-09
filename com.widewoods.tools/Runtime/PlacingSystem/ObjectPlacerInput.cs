using UnityEngine;

public class ObjectPlacerInput : MonoBehaviour
{
    [SerializeField] private ObjectPlacer objectPlacer;
    void Start()
    {
        if (objectPlacer == null) objectPlacer = FindFirstObjectByType<ObjectPlacer>();
    }

    void OnPlacementToggle()
    {
        objectPlacer.TogglePlacing();
    }

    void OnChangePlacingObject()
    {
        objectPlacer.ChangePlacingObject();
    }

    void OnPlace()
    {
        objectPlacer.PlaceObject();
    }
}
