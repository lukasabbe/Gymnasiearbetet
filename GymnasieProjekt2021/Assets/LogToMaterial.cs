using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogToMaterial : MonoBehaviour
{
    public float height;
    Rigidbody _rigidbody;
    float t = 0;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(_rigidbody.velocity.magnitude <= 0.1f){
            t += Time.deltaTime;
        } else {
            t = 0;
        }

        if(t >= 1f){
            Split();
        }
    }

    public GameObject tempPrefab;

    void Split(){   
        Vector3 start = transform.up * (height / 2) + transform.position;
        Vector3 direction = (transform.position - start).normalized;
        for(int i = 0; i < (int)(height / 1.5f); i++){
            Instantiate(tempPrefab, start + (direction * i), Quaternion.LookRotation(direction, Vector3.right));
        }

        Destroy(gameObject);
    }
}
