using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BitingBug : MonoBehaviour
{
    public Vector3 MoveToRelativeX = new Vector3(3f,0,0);
    public Vector3 MoveSpeed = new Vector3(2f, 0, 0);
    private Vector3 startPos;
    private Vector3 endPos;
    private bool positionReached;
    
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + MoveToRelativeX;
    }

    void Update()
    {
        switch (positionReached)
        {
            case false when Vector3.Distance(transform.position, endPos) > 0.1f:
                transform.position += (MoveSpeed * Time.deltaTime);
                break;
            case false when Vector3.Distance(transform.position, endPos) < 0.1f:
                positionReached = true;
                break;
            case true when Vector3.Distance(transform.position, startPos) > 0.1f:
                transform.position -= (MoveSpeed * Time.deltaTime);
                break;
            case true when Vector3.Distance(transform.position, startPos) < 0.1f:
                positionReached = false;
                break;
        }
    }
}
