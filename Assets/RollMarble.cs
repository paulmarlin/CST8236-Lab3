using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RollMarble : MonoBehaviour
{
    public ParticleSystem[] particleSystemList;
    public float explosionRadius = 5.0f;
    public float explosionPower = 1.0f;
    public bool reset = false;
    private int currentSystem=0;
    // for initialization
    void Start() {
    }

    // called once per frame
    void Update() {
        Vector3 direction = new Vector3();

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            direction.z = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            direction.z = -1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            direction.x = 1;
        }

        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, direction, out hit,direction.magnitude);

        if (didHit){
            Vector3 explosionPosition = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
            foreach(Collider collider in colliders)
            {
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
                
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.AddExplosionForce(explosionPower, explosionPosition, explosionRadius, 3.0f);
                }
            }

            AudioSource explosionNoise = GetComponent<AudioSource>();
            explosionNoise.Play();

            particleSystemList[currentSystem].transform.position = explosionPosition;
           // Debug.Log(particleSystemList[currentSystem].name + "is located at" + particleSystemList[currentSystem].transform.position);
            particleSystemList[currentSystem].Play();
            currentSystem = (currentSystem + 1) % particleSystemList.Length;
            transform.position += (hit.distance * direction.normalized);
            

        }
        else {
            transform.position += direction;
        }
    }
}