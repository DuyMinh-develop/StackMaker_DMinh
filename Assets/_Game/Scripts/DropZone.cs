    using UnityEngine;

    public class DropZone : MonoBehaviour
    {
        public GameObject hiddenBrick; 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hiddenBrick.SetActive(true);
            Collider collider = hiddenBrick.gameObject.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}