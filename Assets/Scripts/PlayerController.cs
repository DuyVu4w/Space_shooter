using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Boundary
{
    public float xMax, xMin, zMax, zMin;
}

public class PlayerController : MonoBehaviour
{
    // Input system
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction attackAction;

    // Move value
    private Vector2 moveAmt;

    // bullet spawn position
    private float zBulletPos = 1.4f;

    private Rigidbody rb;

    // movement
    public float speed = 5f;
    public float tilt = 10f;

    // VFX
    public GameObject engine_VFX;
    public Boundary bound;

    // shoot
    public float fireRate = .5f;
    private float timeRate;

    // sfx
    // weapon sound
    public AudioSource source;
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        attackAction = inputActions.FindActionMap("Player").FindAction("Attack");
    }

    void Update()
    {
        moveAmt = moveAction.ReadValue<Vector2>();
        if (attackAction.ReadValue<float>() > 0 && Time.time > timeRate)
        {
            timeRate = Time.time + fireRate;
            Shoot();
        }
    }
    void FixedUpdate()
    {
        // Hướng di chuyển (theo local axes);
        Vector3 moveDir = (Vector3.right * moveAmt.x + Vector3.forward * moveAmt.y);

        if (moveDir.normalized.sqrMagnitude > 0)
        {
            engine_VFX.SetActive(true);
        }
        else
        {
            engine_VFX.SetActive(false);
        }

        // Gán vận tốc tuyến tính
        rb.linearVelocity = moveDir * speed;

        // Clamp vị trí trong boundary
        Vector3 newPos = rb.position;
        newPos.x = Mathf.Clamp(newPos.x, bound.xMin, bound.xMax);
        newPos.z = Mathf.Clamp(newPos.z, bound.zMin, bound.zMax);

        // Giữ object trong biên
        rb.position = newPos;

        // nghiêng player
        transform.rotation = Quaternion.Euler(0, 0, moveAmt.x * -tilt);
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool("Bullet");
        if (bullet != null)
        {
            bullet.transform.position = transform.position + new Vector3(0, 0, zBulletPos);
        }
        source.Play();
    }
}

