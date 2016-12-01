using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplodeWall : MonoBehaviour {

    public List<ParticleSystem> particleList;

    void OnTriggerEnter(Collider collider)
    {
        if (particleList.Count > 0)
        {
            foreach(ParticleSystem particleSystem in particleList)
            {

                particleSystem.transform.position = collider.transform.position;
                particleSystem.Play();
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("COLLIDING::"+collider.gameObject.name);
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("NOT COLLIDING::"+collider.gameObject.name);

    }
}
