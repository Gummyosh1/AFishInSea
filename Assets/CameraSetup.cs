using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    void LateUpdate() {
        var pos = Camera.main.transform.position;
        pos.x = Mathf.Round(pos.x * 100f) / 100f;
        pos.y = Mathf.Round(pos.y * 100f) / 100f;
        Camera.main.transform.position = pos;
    }
}
