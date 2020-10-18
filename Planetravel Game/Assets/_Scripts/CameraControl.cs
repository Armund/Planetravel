using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform LeftBCamPiv;
    public Transform LeftHCamPiv;
    public Transform RightBCamPiv;
    public Transform RightHCamPiv;

    public Transform LeftBPlayerPiv;
    public Transform LeftHPlayerPiv;
    public Transform RightBPlayerPiv;
    public Transform RightHPlayerPiv;
    public float HorS;
    public float VerS;

    public Vector3 Hor;
    public Vector3 Ver;
    public Transform playerT;
    // Start is called before the first frame update
    void Start()
    {
        HorS = ((RightBPlayerPiv.position - LeftBPlayerPiv.position).magnitude / (RightBCamPiv.position - LeftBCamPiv.position).magnitude);
        VerS = ((LeftHPlayerPiv.position - LeftBPlayerPiv.position).magnitude / (LeftHCamPiv.position - LeftBCamPiv.position).magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        temp();
    }

    public void temp()
    {
        Vector3 t = (playerT.position - LeftBPlayerPiv.position);
        Hor = new Vector3(t.x, t.y, 0f);
        Ver = new Vector3(0f, t.y, t.z);
        Camera.main.transform.position = ((Hor/HorS) + (Ver/VerS)) + LeftBCamPiv.position;
    }
}
