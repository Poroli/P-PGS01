using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private Rigidbody2D rb2D;
    private static Vector2 targetPosition;
    private static GameObject playerGO;

    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    private Vector2 currentPlayerVelocity;

    private void Move()
    {
        rb2D.MovePosition(Vector2.SmoothDamp(gameObject.transform.position, targetPosition, ref currentPlayerVelocity, smoothTime, maxSpeed, Time.deltaTime));
    }

    public void SetTargetPosition(Vector2 startVec, bool NeedToCalculate)
    {
        if (!NeedToCalculate)
        {
            targetPosition = startVec;
            return;
        }

        RaycastHit2D hit;
        // Find ground Position
        for (float i = 0;; i += 0.5f)
        {
            hit = Physics2D.CircleCast(startVec, i, Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider == null)
            {
                continue;
            }
            targetPosition = hit.point;
            break;
        }
    }

    public static bool DialoguePartnerReached()
    {
        if (Vector2.Distance(playerGO.transform.position, targetPosition) < 0.1f)
        {
            return true;
        }
        return false;
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerGO = gameObject;
    }

    private void Update()
    {
        Move();
    }
}
