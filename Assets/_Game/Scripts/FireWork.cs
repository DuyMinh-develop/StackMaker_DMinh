using UnityEngine;

public class FireWork : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystemOne;
    [SerializeField] private ParticleSystem _particleSystemTwo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _particleSystemOne.Play();
            _particleSystemTwo.Play();
        }    
    }
}
