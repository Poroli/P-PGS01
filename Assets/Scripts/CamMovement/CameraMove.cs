using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform[] BorderPoints;
    [SerializeField] private Transform camFollowTarget;
    [SerializeField] private float viewportFollowDistance = 0.2f; // 20% entfernung eh Cam Target folgt

    public void SetBorderPoints(Transform[] NewBorderPoints)
    {
        BorderPoints = NewBorderPoints;
    }
    private void LateUpdate()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(camFollowTarget.position);
        Vector3 pos = transform.position;

        // Zentrierung in X-Richtung
        if (viewportPosition.x < viewportFollowDistance || viewportPosition.x > 1 - viewportFollowDistance)
        {
            Vector3 TargetSetCentralVec = camFollowTarget.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, viewportPosition.z));
            TargetSetCentralVec.y = 0; // Ignoriere die Y-Richtung
            TargetSetCentralVec.x -= Mathf.Clamp(viewportPosition.x, viewportFollowDistance, 1 - viewportFollowDistance) - viewportPosition.x;
            Vector3 newPos = pos + TargetSetCentralVec;

            // Überprüfe, ob eine Grenze überschritten wird
            for (int i = 0; i < BorderPoints.Length; i++)
            {
                Vector2 nextPoint = new Vector2(BorderPoints[(i + 1) % BorderPoints.Length].position.x, BorderPoints[(i + 1) % BorderPoints.Length].position.y);
                Vector2 intersection;
                if (LineIntersection(pos, newPos, BorderPoints[i].position, nextPoint, out intersection))
                {
                    // Berechne die Richtung der Bewegung
                    Vector3 direction = (newPos - pos).normalized;

                    // Verschiebe den Schnittpunkt minimal vor die Grenze
                    newPos = new Vector3(intersection.x, intersection.y, pos.z) - direction * 0.01f;
                    break;
                }
            }

            pos = newPos;
        }

        // Zentrierung in Y-Richtung
        if (viewportPosition.y < viewportFollowDistance || viewportPosition.y > 1 - viewportFollowDistance)
        {
            Vector3 TargetSetCentralVec = camFollowTarget.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, viewportPosition.z));
            TargetSetCentralVec.x = 0; // Ignoriere die X-Richtung
            TargetSetCentralVec.y -= Mathf.Clamp(viewportPosition.y, viewportFollowDistance, 1 - viewportFollowDistance) - viewportPosition.y;
            Vector3 newPos = pos + TargetSetCentralVec;

            // Überprüfe, ob eine Grenze überschritten wird
            for (int i = 0; i < BorderPoints.Length; i++)
            {
                Vector2 nextPoint = new Vector2(BorderPoints[(i + 1) % BorderPoints.Length].position.x, BorderPoints[(i + 1) % BorderPoints.Length].position.y);
                Vector2 intersection;
                if (LineIntersection(pos, newPos, BorderPoints[i].position, nextPoint, out intersection))
                {
                    // Berechne die Richtung der Bewegung
                    Vector3 direction = (newPos - pos).normalized;

                    // Verschiebe den Schnittpunkt minimal vor die Grenze
                    newPos = new Vector3(intersection.x, intersection.y, pos.z) - direction * 0.01f;
                    break;
                }
            }

            pos = newPos;
        }

        transform.position = pos;
    }

    bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out Vector2 intersection)
    {
        intersection = Vector2.zero;

        float s1_x, s1_y, s2_x, s2_y;
        s1_x = p2.x - p1.x;     s1_y = p2.y - p1.y;
        s2_x = p4.x - p3.x;     s2_y = p4.y - p3.y;

        float s, t;
        s = (-s1_y * (p1.x - p3.x) + s1_x * (p1.y - p3.y)) / (-s2_x * s1_y + s1_x * s2_y);
        t = (s2_x * (p1.y - p3.y) - s2_y * (p1.x - p3.x)) / (-s2_x * s1_y + s1_x * s2_y);

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            // Kollision erkannt
            intersection = new Vector2(p1.x + (t * s1_x), p1.y + (t * s1_y));

            // Überprüfe die Bewegungsrichtung
            if ((p2.x - p1.x) * (intersection.x - p1.x) >= 0 &&
                (p2.y - p1.y) * (intersection.y - p1.y) >= 0)
            {
                return true;
            }
        }

        return false; // Keine Kollision
    }
}
