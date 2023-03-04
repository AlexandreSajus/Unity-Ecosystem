using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public float start_rabbit;
    public float start_fox;
    public float grass_spawn_rate;
    public float rabbit_min_speed;
    public float rabbit_max_speed;
    public float fox_min_speed;
    public float fox_max_speed;
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
            float speed = Random.Range(fox_min_speed, fox_max_speed);
            // Set the speed endurance and reproduction
            Fox.GetComponent<Fox>().speed = speed;
            // Set random energy
            Fox.GetComponent<Fox>().energy = Random.Range(0f, 100f);
        }

        // Spawn 20 Rabbits at random positions and random rotations
        for (int i = 0; i < 20; i++)
        {
            Instantiate(Rabbit, new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            // Get random speed endurance and reproduction
            float speed = Random.Range(rabbit_min_speed, rabbit_max_speed);
            // Set the speed endurance and reproduction
            Rabbit.GetComponent<Rabbit>().speed = speed;
            // Set random energy
            Rabbit.GetComponent<Rabbit>().energy = Random.Range(0f, 100f);
        }

        // Spawn 20 Grass at random positions
        for (int i = 0; i < 20; i++)
        {
            Instantiate(Grass, new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn Grass randomly
        if (Random.Range(0f, 1f) < grass_spawn_rate)
        {
            Instantiate(Grass, new Vector3(Random.Range(-50f, 50f), 1f, Random.Range(-50f, 50f)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        }
    }
}
