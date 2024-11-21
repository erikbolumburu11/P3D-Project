using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToObject : MonoBehaviour
{

    [SerializeField] Transform anchorPoint;

    void Update()
    {
        transform.position = anchorPoint.position;
    }
}
