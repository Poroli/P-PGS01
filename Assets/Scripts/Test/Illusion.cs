using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Illusion : MonoBehaviour
{
    private GameObject leftSide;
    private GameObject rightSide;

    private Camera cam;

    private void RotateSides()
    {
        float angle = 0;
        float xDistance = Mathf.Abs(cam.transform.position.x) - Mathf.Abs(transform.position.x);
        float zDistance = Mathf.Abs(cam.transform.position.z) - Mathf.Abs(transform.position.z);
        angle = Mathf.Asin(xDistance / Mathf.Sqrt((xDistance*xDistance)+(zDistance*zDistance)));
        angle = Mathf.Rad2Deg * angle;

        print(angle);

        /*  
            Es würde weiter gehen mit dem umwandeln des Winkels in die Rotation der Seiten.
            Die eingestellte POV ist das gesamte Sichtfeld also die Hälfte des POV ist der max. Sichtwinkel in eine Richtung
            Die SeitenRotation = 0 wenn der Winkel die Hälfte des POV ist. Alles darunter wird der Winkel Verhältnismäßig zur Rotation bis 90 Grad wenn die Kamera exakt vor dem GO ist.
        */

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
