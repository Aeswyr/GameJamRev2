using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() {
        MoveLeft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveLeft() {
        Vector3 pos = transform.position;
        pos += new Vector3(-0.05f, 0, 0);
        transform.position = pos;
    }
}
