using UnityEngine;

public interface IRaycastProvider
{
    bool TryGetRaycastHit(out RaycastHit raycastHit, int mask);
}
