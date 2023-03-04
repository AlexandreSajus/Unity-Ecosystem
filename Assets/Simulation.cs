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
    private float time;
    // Create a list to save the number of foxes each second
    public List<int> foxes = new List<int>();
    // Create a list to save the number of rabbits each second
    public List<int> rabbits = new List<int>();
    // Create a list to save the average speed of foxes each second
    public List<float> fox_speed = new List<float>();
    // Create a list to save the average speed of rabbits each second
    public List<float> rabbit_speed = new List<float>();
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

        time += Time.deltaTime;

        // Each second
        if (time > 1)
        {
            time = 0;
            // Get the foxes
            GameObject[] foxes_objects = GameObject.FindGameObjectsWithTag("Fox");
            // Get the rabbits
            GameObject[] rabbits_objects = GameObject.FindGameObjectsWithTag("Rabbit");

            // Get the average speed of foxes
            float fox_current_speed = 0;
            foreach (GameObject fox in foxes_objects)
            {
                fox_current_speed += fox.GetComponent<Fox>().speed;
            }
            fox_current_speed /= foxes_objects.Length;

            // Get the average speed of rabbits
            float rabbit_current_speed = 0;
            foreach (GameObject rabbit in rabbits_objects)
            {
                rabbit_current_speed += rabbit.GetComponent<Rabbit>().speed;
            }
            rabbit_current_speed /= rabbits_objects.Length;

            // Add the number of foxes, rabbits, average speed of foxes and average speed of rabbits to the lists
            this.foxes.Add(foxes_objects.Length);
            this.rabbits.Add(rabbits_objects.Length);
            this.fox_speed.Add(fox_current_speed);
            this.rabbit_speed.Add(rabbit_current_speed);

            // Save the foxes, rabbits, average speed of foxes and average speed of rabbits to a file
            System.IO.File.WriteAllLines("data/foxes.txt", foxes.ConvertAll(i => i.ToString()).ToArray());
            System.IO.File.WriteAllLines("data/rabbits.txt", rabbits.ConvertAll(i => i.ToString()).ToArray());
            System.IO.File.WriteAllLines("data/fox_speed.txt", fox_speed.ConvertAll(i => i.ToString()).ToArray());
            System.IO.File.WriteAllLines("data/rabbit_speed.txt", rabbit_speed.ConvertAll(i => i.ToString()).ToArray());
        }
    }
}
