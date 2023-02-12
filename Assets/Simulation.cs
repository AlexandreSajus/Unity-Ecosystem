using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public float rabbit_spawn_rate = 1f;
    public GameObject Rabbit;
    public GameObject Fox;
    // Start is called before the first frame update
    void Start()
    {
        // Get Rabbit
        Rabbit = GameObject.Find("Rabbit");
        // Get Fox
        Fox = GameObject.Find("Fox");

        // Spawn 20 Foxes at random positions and random rotations
        for (int i = 0; i < 20; i++)
        {
            Instantiate(Fox, new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            // Get random speed endurance and reproduction
            float speed = Random.Range(5f, 15f);
            float endurance = Random.Range(0.2f, 1f);
            float reproduction = Random.Range(1f, 5f);
            float vision = Random.Range(10f, 40f);
            // Set the speed endurance and reproduction
            Fox.GetComponent<Fox>().speed = speed;
            Fox.GetComponent<Fox>().endurance = endurance;
            Fox.GetComponent<Fox>().reproduction = reproduction;
            Fox.GetComponent<Fox>().vision = vision;
            Fox.GetComponent<Fox>().energy = 40f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn rabbits
        if (Random.Range(0f, 1f) < rabbit_spawn_rate * Time.deltaTime)
        {
            Instantiate(Rabbit, new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        }
    }
}
