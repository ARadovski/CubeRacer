using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public event System.Action OnPlayerDeath;
    Vector2 direction;
    Vector2 normDirection;

    [SerializeField] float speed = 1;
    
    Vector2 playerSize;
    float halfScreenWidth;
    void Start()
    {
        playerSize = transform.localScale;
        halfScreenWidth = gameController.halfScreenWidth;      
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        normDirection = direction.normalized;
        transform.Translate (normDirection * speed * Time.deltaTime);

        if(transform.position.x > (halfScreenWidth + playerSize.x/2)){
            transform.position = new Vector2((-halfScreenWidth - playerSize.x/2), transform.position.y);
        }
        if(transform.position.x < (-halfScreenWidth - playerSize.x/2)){
            transform.position = new Vector2((halfScreenWidth + playerSize.x/2), transform.position.y);
        }

        if(transform.position.y < -Camera.main.orthographicSize + playerSize.y/2){
            transform.position = new Vector2(transform.position.x, -Camera.main.orthographicSize + playerSize.y/2);
        }
        if(transform.position.y > Camera.main.orthographicSize - playerSize.y/2){
            transform.position = new Vector2(transform.position.x, Camera.main.orthographicSize - playerSize.y/2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(OnPlayerDeath != null){
            OnPlayerDeath();
        }
        
        Debug.Log("Collision!");
        Destroy(gameObject);
    }
}
