using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public float rabbit_spawn_rate = 1f;
    public GameObject Rabbit;
    public GameObject Fox;
    public GameObject Grass;
    // Start is called before the first frame update
    void Start()
    {
        // Get Rabbit
        Rabbit = GameObject.Find("Rabbit");
        // Get Fox
        Fox = GameObject.Find("Fox");
        // Get Grass
        Grass = GameObject.Find("Grass");

        // Spawn 20 Foxes at random positions and random rotations
        for (int i = 0; i < 20; i++)
        {
            Instantiate(Fox, new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            // Get random speed endurance and reproduction
            float speed = Random.Range(5f, 15f);
            // Set the speed endurance and reproduction
            Fox.GetComponent<Fox>().speed = speed;
        }

        // Spawn 20 Rabbits at random positions and random rotations
        for (int i = 0; i < 20; i++)
        {
            Instantiate(Rabbit, new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            // Get random speed endurance and reproduction
            float speed = Random.Range(5f, 15f);
            // Set the speed endurance and reproduction
            Rabbit.GetComponent<Rabbit>().speed = speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn Grass randomly
        if (Random.Range(0f, 1f) < 0.01f)
        {
            Instantiate(Grass, new Vector3(Random.Range(-50f, 50f), 0.5f, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        }
    }
}
