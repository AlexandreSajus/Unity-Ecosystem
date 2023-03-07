using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{

    public float speed = 1f;
    public float energy_depletion = 1f;
    public bool chased = false;
    public GameObject predator;
    public float vision = 1f;
    public float reproduction = 1f;

    public float fov = 90f;
    public int rayCount = 20;

    public float energy = 100f;
    public string state = "idle";

    public Vector3 move = Vector3.zero;
    public Vector3 rotate = Vector3.zero;
    public Vector3 direction = Vector3.zero;

    public GameObject target;
    public float time = 0f;

    // Create list of Rays
    List<Ray> rays = new List<Ray>();

    // Start is called before the first frame update
    void Start()
    {

    }

    void Patrol()
    {
        // Move in a random direction
        if (time <= 0)
        {
            rotate = new Vector3(0, Random.Range(-5f, 5f), 0);
            time = Random.Range(1f, 3f);
        }
        else
        {
            time -= Time.deltaTime;
        }

        // Move and rotate
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.eulerAngles += rotate * Time.deltaTime * speed;
    }

    // Update is called once per frame
    void Update()
    {
        rays.Clear();
        for (int i = 0; i < rayCount; i++)
        {
            // Draw rays around the fox's z axis
            float angle = transform.eulerAngles.y - fov / 2 + fov / rayCount * i;
            Vector3 direction = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
            Ray ray = new Ray(transform.position, direction);
            rays.Add(ray);
            //Debug.DrawRay(ray.origin, ray.direction * vision, Color.red);

        }

        // If x or z is above 10 or below -10, keep it within the bounds
        if (transform.position.x > 50)
        {
            transform.position = new Vector3(50, transform.position.y, transform.position.z);
            // Turn around
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else if (transform.position.x < -50)
        {
            transform.position = new Vector3(-50, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        if (transform.position.z > 50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 50);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else if (transform.position.z < -50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }

        energy -= Time.deltaTime * energy_depletion * speed;

        if (energy <= 0)
        {
            Destroy(gameObject);
        }

        else if (chased)
        {
            chased = false;
            // Move away from predator
            // if predator not destroyed
            if (predator != null)
            {
                move = transform.position - predator.transform.position;
                move = move.normalized;
                transform.LookAt(transform.position + move);
                transform.position += move * Time.deltaTime * speed;
            }
        }


        else if (energy >= 50)
        {
            if (target == null)
                state = "find_mate";
        }

        else if (energy < 50)
        {
            if (target == null)
                state = "find_prey";
        }

        else if (energy >= 50)
        {
            state = "mate";
        }

        else if (energy < 50)
        {
            state = "prey";
        }

        else if (state == "find_mate")
        {
            Patrol();
            // Check if rays hit an object tagged Fox
            foreach (Ray ray in rays)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, vision))
                {
                    if (hit.collider.tag == "Rabbit")
                    {
                        target = hit.collider.gameObject;
                        state = "mate";
                        break;
                    }
                }
            }
        }

        else if (state == "find_prey")
        {
            Patrol();

            // Check if rays hit an object tagged Fox
            foreach (Ray ray in rays)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, vision))
                {
                    if (hit.collider.tag == "Grass")
                    {
                        target = hit.collider.gameObject;
                        state = "prey";
                        break;
                    }
                }
            }
        }

        else if (state == "mate")
        {
            if (target == null)
            {
                state = "find_mate";
            }
            else
            {
                // Move towards target
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
                // Rotate towards target
                transform.LookAt(target.transform.position);

                // If target is within 1 unit
                // Destroy target
                if (Vector3.Distance(transform.position, target.transform.position) <= 1 && target.tag == "Rabbit")
                {
                    energy -= 40f;
                    state = "find_prey";
                    // Spawn new fox with speed vision endurance and reproduction mean of parents
                    // For i in int range of reproduction
                    for (int i = 0; i < reproduction; i++)
                    {
                        GameObject newFox = Instantiate(gameObject, transform.position, transform.rotation);
                        newFox.GetComponent<Rabbit>().speed = (speed + target.GetComponent<Rabbit>().speed) / 2;
                        newFox.GetComponent<Rabbit>().energy = 30f;
                    }
                }
            }
        }

        else if (state == "prey")
        {
            // if target is not destroyed
            if (target != null)
            {
                // Move towards target
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
                // Rotate towards target
                transform.LookAt(target.transform.position);

                // If target is within 1 unit
                // Destroy target
                if (Vector3.Distance(transform.position, target.transform.position) <= 2f)
                {
                    Destroy(target);
                    energy += 20f;
                }
            }
            else
            {
                state = "find_prey";
            }
        }
    }
}

