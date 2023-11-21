using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Illusion : MonoBehaviour
{
    [SerializeField] private float sidesMaxRotation;
    [SerializeField] private GameObject leftSide;
    [SerializeField] private GameObject rightSide;

    private Camera cam;

    private void RotateSides()
    {
        float xDistance = Mathf.Abs(cam.transform.position.x - transform.position.x);
        float visibleWidth = cam.orthographicSize * cam.aspect;
        float rotation = 0;
        float tmpDistanceRatio = (Mathf.Abs(sidesMaxRotation) - -90) / visibleWidth;

        if (xDistance < visibleWidth)
        {
            rotation = xDistance * tmpDistanceRatio;
        }
        else
        {
            rotation = visibleWidth * tmpDistanceRatio;
        }

        // Rotation der Seiten basierend auf der Position des Spielers
        if (cam.transform.position.x < transform.position.x)
        {
            // Spieler ist links vom Objekt
            leftSide.transform.rotation = Quaternion.Euler(0, -90 + rotation, 0);
            rightSide.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            // Spieler ist rechts vom Objekt
            rightSide.transform.rotation = Quaternion.Euler(0, -90 + rotation, 0);
            leftSide.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    private void Update()
    {
        RotateSides();
    }

    private void Start()
    {
        cam = Camera.main;
    }
}