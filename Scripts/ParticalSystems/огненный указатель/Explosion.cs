using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionPrefab;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        ParticleSystem timelate = Instantiate(_explosionPrefab, transform);
        Destroy(timelate.gameObject, timelate.main.duration);
        Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    ParticleSystem timelate = Instantiate(_explosionPrefab, transform);
    //    Destroy(timelate.gameObject, timelate.main.duration);
    //    Destroy(gameObject);
    //}
}