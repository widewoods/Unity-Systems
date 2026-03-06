using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private LayerMask placeableMask;
    [SerializeField] private GameObject[] placeableObjects;

    private IPlacementResolver placementResolver;
    private IRaycastProvider raycaster;
    private IGhostFactory ghostFactory;

    private int objectIndex;
    private GameObject objectToPlace;

    private bool isTryingToPlace = false;
    private GameObject placingObjectGhost;
    private float cachedOffset;

    private bool canBePlaced;

    public void Initialize()
    {
        objectToPlace = placeableObjects[0];
        placementResolver = GetComponent<IPlacementResolver>();
        raycaster = GetComponent<IRaycastProvider>();
        ghostFactory = GetComponent<IGhostFactory>();
    }

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        if (!isTryingToPlace) return;

        ShowGhostPosition();
    }

    public void TogglePlacing()
    {
        isTryingToPlace = !isTryingToPlace;
        if (isTryingToPlace)
        {
            placingObjectGhost = ghostFactory.SpawnNewGhostObject(objectToPlace);
            cachedOffset = ghostFactory.GetModelOffset();
        }
        else
        {
            if (placingObjectGhost != null) Destroy(placingObjectGhost);
        }
    }

    public void ChangePlacingObject()
    {
        objectIndex += 1;
        if (objectIndex >= placeableObjects.Length) objectIndex = 0;
        objectToPlace = placeableObjects[objectIndex];

        placingObjectGhost = ghostFactory.SpawnNewGhostObject(objectToPlace);
        cachedOffset = ghostFactory.GetModelOffset();
    }

    public void PlaceObject()
    {
        if (!canBePlaced) return;

        Transform ghostTransform = placingObjectGhost.transform;
        Instantiate(objectToPlace, ghostTransform.position, ghostTransform.rotation);

        Destroy(placingObjectGhost);
        isTryingToPlace = false;
    }

    private void ShowGhostPosition()
    {
        if (raycaster.TryGetRaycastHit(out RaycastHit hit, placeableMask))
        {
            PlacementResult placementResult = placementResolver.GetPlacement(hit, cachedOffset);

            ghostFactory.SetPlaceable(placementResult.CanPlace);
            canBePlaced = placementResult.CanPlace;

            placingObjectGhost.transform.rotation = placementResult.Rotation;
            // placingObjectGhost.transform.up = hit.normal;
            placingObjectGhost.transform.position = placementResult.Position;
        }
        else
        {
            ghostFactory.SetPlaceable(false);
            canBePlaced = false;
        }
    }
}
