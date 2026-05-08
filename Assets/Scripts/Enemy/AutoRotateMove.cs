using UnityEngine;

public class AutoRotateMove : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed;
    private Rigidbody rb;
    private float zMin = -5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if (rb != null)
        {
            rb.angularVelocity = Random.insideUnitSphere * rotateSpeed;
        }
    }

    void Update()
    {
        if (transform.position.z < zMin)
        {

            ObjectPooler.Instance.ReturnToPool("Rock", gameObject);
        }
    }
}
