using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubestacleBehavior : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3 (Random.Range(.3f, 1.5f), Random.Range(.3f, 2), 1);
        transform.Rotate(0, 0, Random.Range(-5, 5));
        speed = GameController.obstacleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
