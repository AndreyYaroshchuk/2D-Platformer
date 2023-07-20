using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralacs : MonoBehaviour
{
    private float lenght;
    private float startpos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;  
        
    }
    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float newDis = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + newDis, transform.position.y, transform.position.z);

        if (temp > startpos + lenght) startpos += lenght;
        else if (temp < startpos - lenght) startpos -= lenght;

    }
}
