using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FisherAnimation : MonoBehaviour
{
    public Transform rodTip; // The position where the line starts (e.g., fishing rod tip)
    public Transform bobber; // The position where the line ends (e.g., bobber)
    public int segments = 20; // Number of segments for the line
    public float lineArcHeight = 2.0f; // How high the arc should be

    public LineRenderer lineRenderer;
    int up = 0;
    int down = 0;
    float timer = 0;

    void Start()
    {
        lineRenderer.positionCount = segments + 1; // The number of points the line will have
    }

    void Update()
    {
        updateBobber();
        DrawFishingLine();
    }

    void DrawFishingLine()
    {
        Vector3 startPoint = rodTip.position;
        Vector3 endPoint = bobber.position;

        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            Vector3 pointOnLine = Vector3.Lerp(startPoint, endPoint, t);

            // Create an arc by adjusting the y position
            float parabolicT = t * 2 - 1;
            pointOnLine.y += lineArcHeight * (1 - parabolicT * parabolicT);
            pointOnLine.z += 1;

            lineRenderer.SetPosition(i, pointOnLine);
        }
    }

    public void updateBobber(){
        timer += Time.deltaTime;
        if (timer > 1){
            if (up < 10){
            bobber.position += new Vector3(0,.175f,0);
            timer = 0;
            up++;
            }
            else{
                bobber.position -= new Vector3(0,.175f,0);
                timer = 0;
                down++;
                if (down >= 10){
                    down = 0;
                    up = 0;
                }
            }
        }
    }
}
