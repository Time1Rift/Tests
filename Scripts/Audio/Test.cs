using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Moverent>() == false)
            return;

        _audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Moverent>() == false)
            return;

        _audioSource.Pause();
    }
}