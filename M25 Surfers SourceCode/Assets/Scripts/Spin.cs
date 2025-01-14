using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    ParticleSystem particles;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
               particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particles.isPlaying)
        {
            particles.transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.z + speed *Time.deltaTime);
        }
    }
}
