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
        float angle = 0;
        float xDistance = Mathf.Abs(cam.transform.position.x) - Mathf.Abs(transform.position.x);
        float zDistance = Mathf.Abs(cam.transform.position.z) - Mathf.Abs(transform.position.z);
        angle = Mathf.Asin(xDistance / Mathf.Sqrt((xDistance * xDistance) + (zDistance * zDistance)));
        angle = Mathf.Rad2Deg * angle;

        print(angle);

        // Umwandeln des Winkels in die Rotation der Seiten
        float pov = cam.fieldOfView;
        float rotation = 0;

        if (angle < pov)
        {
            float tmpAngleRatio = (Mathf.Abs(sidesMaxRotation) - -90) / pov;
            rotation = angle * tmpAngleRatio;
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