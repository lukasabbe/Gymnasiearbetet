using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, IMaterial
{
    public MaterialManager.MaterialType materialType;
    public int teir;

    MaterialManager materialManager;

    public Collider TreeCollider;
    public GameObject log;
    public GameObject trunk;
    private void Awake() {
        materialManager = GetComponent<MaterialManager>();
        materialManager.equipmentTeirRequired = teir;
    }
    public void Break(){
        TreeCollider.enabled = false;

        Rigidbody logRigidbody = log.GetComponent<Rigidbody>();
        logRigidbody.isKinematic = false;
        logRigidbody.AddForce((transform.position - GameManager.player.transform.position) * 100, ForceMode.Impulse);

        log.GetComponent<Collider>().enabled = true;
        
        trunk.GetComponent<Collider>().enabled = true;
    }
}
