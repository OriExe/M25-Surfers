using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoad : MonoBehaviour
{
    [SerializeField] MeshRenderer roadMat;
    [Range(0f, 1f)]
    [SerializeField] private float speed; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        roadMat.materials[0].mainTextureOffset = new Vector2(0, roadMat.materials[0].mainTextureOffset.y%100 + speed / 10);
        
    }
}
