using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_camera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _cameraTarget;

    void Start()
    {
        
    } 
    

    void Update()
    {
        Vector3 relativepos = _cameraTarget.transform.position - _camera.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativepos, _cameraTarget.transform.position);
        ///Ray CameraLine = new Ray(_camera.transform.position, rotation);
}
}
