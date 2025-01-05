using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField]
    private Transform maxNorth;

    [SerializeField]
    private Transform maxSouth;

    [SerializeField]
    private Transform maxWest;

    [SerializeField]
    private Transform maxEast;

    [SerializeField]
    private Rigidbody2D rb;

    private float width, height;

    private void Awake() {
        height = GetComponent<Camera>().orthographicSize;
        width = height * GetComponent<Camera>().aspect;
    }

    public void SetEast(Vector3 v)
    {
        maxEast.position = v;
    }
    public void SetWest(Vector3 v)
    {
        maxWest.position = v;
    }
    public void SetNorth(Vector3 v)
    {
        maxNorth.position = v;
    }
    public void SetSouth(Vector3 v)
    {
        maxSouth.position = v;
    }

    private void LateUpdate() {
        var follow2D = Vector2.MoveTowards(transform.position, rb.position, 7.5f * Time.deltaTime);
        var follow = new Vector3(follow2D.x, follow2D.y, transform.position.z);

        if (follow.x - width <= maxWest.position.x) follow.x = maxWest.position.x + width;
        if (follow.x + width >= maxEast.position.x) follow.x = maxEast.position.x - width;
        if (follow.y - height <= maxSouth.position.y) follow.y = maxSouth.position.y + height;
        if (follow.y + height >= maxNorth.position.y) follow.y = maxNorth.position.y - height;

        transform.position = follow;
    }
}
