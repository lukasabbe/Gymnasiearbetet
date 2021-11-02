using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour, IMaterial
{
    MaterialManager materialManager;

    public GameObject rock, rockBroken;
    public Collider rockCollider;
    private void Awake() {
        materialManager = GetComponent<MaterialManager>();
    }
    public void Damage(int damage){
        materialManager.health -= damage;
        if(materialManager.health <= 0) Break();
    }
    public void Break(){
        rockCollider.enabled = false;

        rock.SetActive(false);
        rockBroken.SetActive(true);
    }
}
