using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesPlus : MonoBehaviour
{
    public GameObject Zombie;
    private Transform _transform;
    private float tiempo = 7f;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {

        tiempo += Time.deltaTime;
        if (tiempo >= 7)
        {
            Instantiate(Zombie, _transform.position, Quaternion.identity);
            tiempo = 0;
        }
    }
}
