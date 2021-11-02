using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    PlayerInputEventManager input;
    public MaterialManager.MaterialType materialType;
    public int teir; 
    public int damage;

    private void Awake() {
        input = FindObjectOfType<PlayerInputEventManager>();
    }
    void OnEnable(){
        input.leftMouseButton += OnLeftClick;
    }
    private void OnDisable() {
        input.leftMouseButton -= OnLeftClick;
    }
    void OnLeftClick(){
        RaycastHit ray = PlayerHelperFunctions.ViewRay(3f, Layers.resource);
        if (ray.point != Vector3.zero){
            MaterialManager _mm = ray.transform.gameObject.GetComponent<MaterialManager>();
            if(_mm.equipmentTeirRequired <= teir && materialType == _mm.materialType) ray.transform.gameObject.GetComponent<IMaterial>().Damage(damage);
        }
    }
}
