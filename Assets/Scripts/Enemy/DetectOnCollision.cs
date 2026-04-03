using System.Collections;
using UnityEngine;

public class DetectOnCollision : MonoBehaviour
{
    public int score;
    public GameController gameController;

    void Start()
    {
        GameObject controller = GameObject.FindWithTag("GameController");
        if (controller)
        {
            gameController = controller.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Can't find game controller");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // play fx and sfx
            GameObject vfx = Instantiate(VFXPooling.sharedInstance.prefabs[1]);
            GameObject player = collision.gameObject;
            vfx.GetComponent<AudioSource>().Play();

            vfx.transform.position = player.transform.position;
            vfx.GetComponent<ParticleSystem>().Play();
            vfx.SetActive(false);
            
            gameController.GameOver();
    
            player.SetActive(false);
        }
        // increase socre
        gameController.IncreaseScore(score);
        // play vfx
        GameObject explosionFx = VFXPooling.sharedInstance.SpawnFromPool(VFXPooling.sharedInstance.prefabs[0], VFXPooling.sharedInstance.poolAsteroid);
        explosionFx.transform.position = transform.position;
        // return vfx explosion to pool
        VFXPooling.sharedInstance.ReturnAsteroidVFXToPool(explosionFx);

        // reset velocity
        gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        // return assteroid to pool
        RockPooling.sharedInstance.ReturnToPool(gameObject);
    }

}
