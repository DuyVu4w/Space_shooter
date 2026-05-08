using UnityEngine;

public class DetectOnCollision : MonoBehaviour
{
    public string poolTag;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        gameController = GameController.Instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 1. Kiểm tra va chạm với Người chơi
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerDeath(collision.gameObject);
            return; 
        }

        // 2. Kiểm tra va chạm với Đạn (hoặc các vật thể làm nổ đá)
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            HandleObjectDestruction();
        }
    }

    private void HandlePlayerDeath(GameObject player)
    {
        // Hiệu ứng nổ cho Player
        // Sử dụng Pool để tránh Instantiate (khuyên dùng)s
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