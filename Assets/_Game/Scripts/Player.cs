using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] public Transform mainObject;
    [SerializeField] private float maxRaycastDistance = 10f;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private LayerMask wallMask;

    private float brickHeight;
    private Vector3 target;
    private Vector3 currentDirection;
    private bool isMoving = false;

    public List<GameObject> bricks = new List<GameObject>();
    private CapsuleCollider playerCollider;

    private float capsuleHeight;
    private Vector3 capsuleCenter;

    private HashSet<GameObject> visitedDropZones = new HashSet<GameObject>();

    private void Start()
    {
        if (mainObject == null) mainObject = transform;

        target = mainObject.position;
        playerCollider = GetComponent<CapsuleCollider>();
        if (playerCollider != null)
        {
            capsuleHeight = playerCollider.height;
            capsuleCenter = playerCollider.center;
        }
        if (brickPrefab != null)
        {
            Renderer renderer = brickPrefab.GetComponent<Renderer>();
            if (renderer != null) brickHeight = renderer.bounds.size.y;
            else brickHeight = 0.25f;
        }
    }
    private void Update()
    {
        HandleInput();
        Move();
    }
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
            {
                Vector3 hitPoint = hit.point;
                Vector3 direction = hitPoint - mainObject.position;
                direction.y = 0f;

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
                    currentDirection = new Vector3(Mathf.Sign(direction.x), 0, 0);
                else
                    currentDirection = new Vector3(0, 0, Mathf.Sign(direction.z));
                Vector3 rayOrigin = mainObject.position + Vector3.up * 0.5f;
                RaycastHit wallHit;
                if (Physics.Raycast(rayOrigin, currentDirection, out wallHit, maxRaycastDistance, wallMask))
                {
                    target = wallHit.point - currentDirection * 0.01f;
                    target.y = mainObject.position.y;
                }
                isMoving = true;
            }
        }
    }
    void OnDrawGizmos()
    {
        if (mainObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(mainObject.position + Vector3.up * 0.5f, currentDirection * maxRaycastDistance);
        }
    }
    private void Move()
    {
        if (!isMoving) return;
        mainObject.position = Vector3.MoveTowards(mainObject.position, target, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(mainObject.position, target) < 0.01f)
        {
            mainObject.position = target;
            isMoving = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            BrickCollection(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DropZone"))
        {
            if (!visitedDropZones.Contains(collision.gameObject))
            {
                visitedDropZones.Add(collision.gameObject);
                DropBrick(collision.gameObject);
                if (bricks.Count <= 0)
                {
                    GameOver();
                }
            }
        }    
    }
    private void GameOver()
    {
        Debug.Log("Game Over");
    }
    public void BrickCollection(GameObject brickObject)
    {
        brickObject.transform.SetParent(mainObject, true);
        bricks.Add(brickObject);
        Brick brick = brickObject.GetComponent<Brick>();
        brick.gameObject.SetActive(true);
        Collider collider = brickObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
            collider.isTrigger = false;
        }
        brickObject.transform.localPosition = new Vector3(0, -(bricks.Count - 1) * brickHeight, 0);
        if (playerCollider != null)
        {
            playerCollider.height = capsuleHeight + bricks.Count * brickHeight;
            playerCollider.center = capsuleCenter - new Vector3(0f, (bricks.Count * brickHeight) / 2f, 0f);
        }
    }
    private void DropBrick(GameObject brickObject)
    {
        if (bricks.Count > 0)
        {
            GameObject lastBrickObject = bricks[bricks.Count - 1];
            bricks.RemoveAt(bricks.Count - 1);
            lastBrickObject.transform.SetParent(null);
            Destroy(lastBrickObject);
            if (playerCollider != null)
            {
                playerCollider.height = capsuleHeight + bricks.Count * brickHeight;
                playerCollider.center = capsuleCenter - new Vector3(0f, (bricks.Count * brickHeight) / 2f, 0f);
            }
        }
    }
}