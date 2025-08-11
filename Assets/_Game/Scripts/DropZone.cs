    using UnityEngine;

    public class DropZone : MonoBehaviour
    {
        public GameObject hiddenBrick; 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hiddenBrick.SetActive(true);
        }    
    }
}