using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    public float speed = 1f;
    public float vision = 1f;
    public float endurance = 1f;
    public float reproduction = 1f;

    public float fov = 90f;
    public int rayCount = 20;

    public float energy = 100f;
    public string state = "idle";

    public Vector3 move = Vector3.zero;
    public Vector3 rotate = Vector3.zero;

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
        // Move forward for 3 seconds
        if (time < 3)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            time += Time.deltaTime;
        }
        // Wait for 1 second
        else if (time < 4)
        {
            time += Time.deltaTime;
        }
        // Rotate for 3 seconds
        else if (time < 7)
        {
            rotate = new Vector3(0, 5f, 0);
            transform.eulerAngles += rotate * Time.deltaTime * speed;
            time += Time.deltaTime;
        }
        // Reset time
        else
        {
            time = 0;
        }
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
            Debug.DrawRay(ray.origin, ray.direction * vision, Color.red);

        }

        // If x or z is above 10 or below -10, keep it within the bounds
        if (transform.position.x > 50)
        {
            transform.position = new Vector3(50, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -50)
        {
            transform.position = new Vector3(-50, transform.position.y, transform.position.z);
        }
        if (transform.position.z > 50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 50);
        }
        else if (transform.position.z < -50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }

        energy -= Time.deltaTime * 1/endurance;

        if (energy <= 0)
        {
            Destroy(gameObject);
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

        if (state == "find_mate")
        {
            Patrol();
            // Check if rays hit an object tagged Fox
            foreach (Ray ray in rays)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, vision))
                {
                    if (hit.collider.tag == "Fox")
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
                    if (hit.collider.tag == "Rabbit")
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
            // Move towards target
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            // Rotate towards target
            transform.LookAt(target.transform.position);

            // If target is within 1 unit
            // Destroy target
            if (Vector3.Distance(transform.position, target.transform.position) <= 1 && target.tag == "Fox")
            {
                energy -= 40f;
                state = "find_prey";
                // Spawn new fox with speed vision endurance and reproduction mean of parents
                GameObject newFox = Instantiate(gameObject, transform.position, transform.rotation);
                newFox.GetComponent<Fox>().speed = (speed + target.GetComponent<Fox>().speed) / 2;
                newFox.GetComponent<Fox>().vision = (vision + target.GetComponent<Fox>().vision) / 2;
                newFox.GetComponent<Fox>().endurance = (endurance + target.GetComponent<Fox>().endurance) / 2;
                newFox.GetComponent<Fox>().reproduction = (reproduction + target.GetComponent<Fox>().reproduction) / 2;
            }
        }

        else if (state == "prey")
        {
            // Move towards target
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            // Rotate towards target
            transform.LookAt(target.transform.position);

            // If target is within 1 unit
            // Destroy target
            if (Vector3.Distance(transform.position, target.transform.position) <= 1)
            {
                Destroy(target);
                energy += 20f;
            }
        }
    }
}

