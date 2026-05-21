using UnityEngine;

public class DetectOnCollision : MonoBehaviour
{
    public string poolTag;
    public int scoreValue;
    private GameController gameController;
    public int hp = 1;
    void Start()
    {
        gameController = GameController.Instance;
    }


    void OnCollisionEnter(Collision col)
    {
        // kiểm tra va chạm với người chơi
        if (col.gameObject.CompareTag("Player"))
        {
            HandlePlayerDeath(col.gameObject);
            return; 
        }
    }

    // butlet ở dạng trigger
    void OnTriggerEnter(Collider collider)
    {
        // kiểm tra va chạm với đạn player
        if (collider.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            if (hp <= 0) HandleObjectDestruction();
        }
    }

    private void HandlePlayerDeath(GameObject player)
    {
        // hiệu ứng nổ
        // sử dụng Pool để tránh
        GameObject vfx = VFXPooling.Instance.prefabs[1]; // hiệu ứng nổ của player
        if (vfx != null)
        {
            vfx.transform.position = player.transform.position;
            vfx.SetActive(true);
            vfx.GetComponent<ParticleSystem>().Play();
        }

        // Gọi GameOver TRƯỚC khi tắt Player
        if (gameController != null)
        {
            gameController.GameOver();
        }

        // Vô hiệu hóa người chơi
        player.SetActive(false);

        // Thu hồi chính viên đá này về Pool
        ResetAndReturnToPool();
    }

    private void HandleObjectDestruction()
    {
        // Cộng điểm
        if (gameController != null)
        {
            gameController.IncreaseScore(scoreValue);
            Debug.Log(gameController.score);
        }

        // Hiệu ứng nổ cho Đá/Thiên thạch
        GameObject explosionFx = VFXPooling.Instance.SpawnFromPool("Rock");
        if (explosionFx != null)
        {
            explosionFx.transform.position = transform.position;
            explosionFx.SetActive(true);
        }
        
        // Thu hồi viên đá về Pool
        ResetAndReturnToPool();
    }

    private void ResetAndReturnToPool()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; // Reset cả vận tốc xoay
        }

        gameObject.SetActive(false);
        
        // Sử dụng ObjectPooler mới của bạn để thu hồi
        if (ObjectPooler.Instance != null && !string.IsNullOrEmpty(poolTag))
        {
            ObjectPooler.Instance.ReturnToPool(poolTag, gameObject);
        }
    }
}