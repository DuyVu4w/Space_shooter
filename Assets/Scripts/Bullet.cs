using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float zBound; 
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // reset vận tốc, vận tốc góc
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // add Fore để bắn đạn
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);      
    }

    void Update()
    {
        if (transform.position.z > zBound)
        {
            Deactive();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Deactive();
    }


    private void Deactive()
    {
        // reset vận tốc, vận tốc góc
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        BulletPooling.Instance.ReturnToPool(gameObject);
    }
}
