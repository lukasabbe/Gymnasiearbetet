using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuctureFinder : MonoBehaviour
{
    private void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.openStructKey += openStructure;
    }

    void openStructure()
    {
        Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit ray, 3, Layers.structure);
        if (ray.point != Vector3.zero)
        {
            ray.transform.gameObject.GetComponent<StructureBasePlateUI>().openInventory(ray);
        }
    }
}
