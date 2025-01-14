using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioSource pop;
    private BoxCollider bc;
    private MeshRenderer Mesh;
    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        Mesh = bc.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pop.Play();
            bc.enabled = false;
            Mesh.enabled = false;
        }
    }

    public void showMesh()
    {
        bc.enabled = true;
        Mesh.enabled = true;
    }
}
