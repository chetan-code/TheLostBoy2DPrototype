using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class RopeSystem : MonoBehaviour
{

    Vector3 attachedPoint;
    LineRenderer lineRenderer;
    [SerializeField]
    Transform attachedBody;

    // Start is called before the first frame update
    void Start()
    {
        attachedPoint = attachedBody.position;
        lineRenderer = GetComponent<LineRenderer>();
    }


    private void LateUpdate()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, attachedPoint);
    }
}
