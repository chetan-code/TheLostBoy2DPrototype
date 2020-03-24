using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    float timeToTurn = 5f;
    [SerializeField,Range(0.1f,1.0f)]
    float walkSpeed;
    [SerializeField]
    float distance = 10f;
    [SerializeField]
    LayerMask objectToBedetectedLayer;
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject[] deactivateOnDeath;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    BoxCollider2D deathTrigger;
    Vector2 raycastOrigin;
    Vector2 raycastDirection;

    Player AI;
    float localTime;
    bool isAlive = true;
    Vector2 AIMovementVector = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<Player>();
        AIMovementVector.x =  walkSpeed;
        //Move Right
        AI.SetDirectionalInput(AIMovementVector);
        raycastDirection = transform.right;
        localTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            CastRay();
        }

        if (Time.time > localTime + timeToTurn)
            {                //change direction
                ChangeDirection();
            }

        if (AI.controller.collisions.right|| AI.controller.collisions.left) {
            Debug.Log("collision");
            ChangeDirection();
        }


    }

    private void CastRay() {
        raycastOrigin = transform.position;
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, distance, objectToBedetectedLayer);
        Debug.DrawRay(raycastOrigin, raycastDirection * distance, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player")) {
                PlayerDetected();
            }
        }
    }

    private void PlayerDetected() {
        gameManager.Death();
    }


    private void ChangeDirection() {
        localTime = Time.time;
        raycastDirection = raycastDirection * -1;
        AIMovementVector.x = AIMovementVector.x * -1;
        AI.SetDirectionalInput(AIMovementVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.TAGS.Player.ToString()) && gameManager.isAlive) {
            isAlive = false;
            deathTrigger.enabled = false;
            AI.controller.collider.enabled = false;
            AI.moveSpeed = 0;
            AI.playerFx.InstantiateFx(1, transform.position, Quaternion.identity);
            animator.SetBool("IsDead", true);
            foreach (GameObject go in deactivateOnDeath) {
                go.SetActive(false);
            }
            Destroy(gameObject, 0.3f);
        }    
    }
}
