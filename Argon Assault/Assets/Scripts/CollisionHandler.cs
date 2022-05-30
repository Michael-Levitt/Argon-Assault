using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float restartDelay = 1f;
    [SerializeField] ParticleSystem playerExplosion;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StartCrashSequence());
    }

    IEnumerator StartCrashSequence()
    {
        DestroyShip();
        yield return new WaitForSeconds(restartDelay);
        ReloadLevel();
    }

    private void DestroyShip()
    {
        GetComponent<MeshRenderer>().enabled = false; // Hides ship after collision
        GetComponent<PlayerController>().enabled = false; // Disable movement
        playerExplosion.Play(); // Explosion vfx
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
