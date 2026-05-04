using UnityEngine;

public class AutoRotateMove : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed;
    private Rigidbody rb;
    private float zMin = -5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * rotateSpeed;
    }

    void Update()
    {
        if (transform.position.z < zMin)
        {

            RockPooling.Instance.ReturnToPool(gameObject);
        }
    }
}
