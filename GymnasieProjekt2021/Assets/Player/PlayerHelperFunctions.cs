using UnityEngine;

public static class PlayerHelperFunctions{
    public static RaycastHit ViewRay(RaycastHit ray, float maxDist, LayerMask layer){
        Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit _ray, maxDist, layer);
        return _ray;
    }   
}
