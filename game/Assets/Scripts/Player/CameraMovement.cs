using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float moveSpeed = 10.0f;
    public float rotateSpeed = 90.0f;

    void Start()
    {
        transform.position = target.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position , moveSpeed * Time.deltaTime);
    }
}
