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
    public GameObject cover;

    public LogToMaterial logToMaterial;
    private void Awake() {
        materialManager = GetComponent<MaterialManager>();
        materialManager.equipmentTeirRequired = teir;
    }
    public void Damage(int damage){
        materialManager.health -= damage;
        if(materialManager.health <= 0) Break();
    }
    public void Break(){
        TreeCollider.enabled = false;

        Rigidbody logRigidbody = log.GetComponent<Rigidbody>();
        logRigidbody.isKinematic = false;
        logRigidbody.AddForce((transform.position - GameManager.player.transform.position) * 1000, ForceMode.Impulse);

        log.GetComponent<Collider>().enabled = true;
        trunk.GetComponent<Collider>().enabled = true;

        logToMaterial.enabled = true;

        if (cover != null) Destroy(cover);
    }
}
