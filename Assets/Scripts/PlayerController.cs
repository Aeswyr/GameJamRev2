using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move() {
        Vector3 pos = transform.position;
        
        if (Input.GetKey(KeyCode.D)) {
            pos += new Vector3(0.1f, 0, 0);
            sprite.flipX = false;
        }
        if (Input.GetKey(KeyCode.A)) {
            pos += new Vector3(-0.1f, 0, 0); 
            sprite.flipX = true;
        }
       
        transform.position = pos;
    }
}
