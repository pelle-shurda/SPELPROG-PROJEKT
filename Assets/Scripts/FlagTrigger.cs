using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FlagTrigger : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    [SerializeField] private string nextSceneName;
    [SerializeField] private float delay = 2f;

    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasActivated) return;

        if (other.CompareTag("Player"))
        {
            hasActivated = true;

            if (anim != null)
                anim.SetTrigger("FlagDeploy");

            if (audioSource != null)
                audioSource.Play();

            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}

