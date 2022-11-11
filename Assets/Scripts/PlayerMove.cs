using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // playerMove���� ����� ���� ����
    public Rigidbody2D playerRigidbody;
    public float speed = 5f;

    float h, v;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

    }

    
    void Update()
    {
        // Ű�Է� ����
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Move
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    


}
