using UnityEngine;

public class QuarkMultiverseTravel : MonoBehaviour
{
    public GameObject portal;
    public string targetMultiverse;

    public AudioSource audioSource;
    public AudioClip PortalSound;
    public AudioClip QUARKActivationSound;

    private AudioClip originalClip;

    public void Awake()
    {
        if (portal != null)
        {
            portal.SetActive(false);
        }
    }

    public void ActivatePortal()
    {
        if (portal != null)
        {
            portal.SetActive(true);

            if (audioSource != null)
            {
                originalClip = audioSource.clip;
                
                audioSource?.Stop();
                audioSource?.PlayOneShot(QUARKActivationSound);

                audioSource.clip = PortalSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void DeactivatePortal()
    {
        if (portal != null)
        {
            portal.SetActive(false);

            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.loop = true;
                audioSource.clip = originalClip;
                audioSource.Play();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (portal != null && portal.activeSelf && other.CompareTag("Player"))
        {
            TravelToTargetMultiverse();
        }
    }

    public void TravelToTargetMultiverse()
    {
        if (targetMultiverse != null)
        {
            SceneTransition.Instance.LoadScene(targetMultiverse);
        }
    }
}
