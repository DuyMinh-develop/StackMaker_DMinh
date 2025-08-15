using System.Net;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject closeChest;
    [SerializeField] private GameObject openChest;
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem pSystemOne;
    [SerializeField] private ParticleSystem pSystemTwo;

    private void Update()
    {
        pSystemOne.Play();
        pSystemTwo.Play();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            closeChest.SetActive(false);
            openChest.SetActive(true);
            player.isMoving = false;
        }    
    }
}
