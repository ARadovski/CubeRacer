using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    Vector2 direction;
    Vector2 normDirection;

    [SerializeField] float speed = 1;
    
    float playerWidth;
    float halfScreenWidth;
    void Start()
    {
        playerWidth = transform.localScale.x;
        halfScreenWidth = gameController.halfScreenWidth;
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        normDirection = direction.normalized;
        transform.Translate (normDirection * speed * Time.deltaTime);

        if(transform.position.x > (halfScreenWidth + playerWidth)){
            transform.position = new Vector2((-halfScreenWidth - playerWidth), transform.position.y);
        }
        if(transform.position.x < (-halfScreenWidth - playerWidth)){
            transform.position = new Vector2((halfScreenWidth + playerWidth), transform.position.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        Destroy(gameObject);
    }
}
