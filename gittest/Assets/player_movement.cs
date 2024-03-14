using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class playercontroller : MonoBehaviour
{

    [SerializeField] private GameObject _player;

    [SerializeField]private float speed =5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private CapsuleCollider _playerCapsuleCollider;
    [SerializeField] private LayerMask _planeLayerMask;

    private float gravity = -9.81f;
    private float height;
    private Vector3 playerbottom;
    private Vector3 playerTop;
    private Vector3 playergravity;

    void Start()
    {
        height = _playerCapsuleCollider.height;
        Vector3 playerTop = _playerCapsuleCollider.center +new Vector3(0,height/2,0);
        Vector3 playerbottom = _playerCapsuleCollider.center + new Vector3(0, -height / 2, 0);
        Vector3 playergravity= new Vector3(0,2f,0);

    }
    void Update()
    {

        Ray groundCheckRay = new Ray(_player.transform.position,Vector3.down);
        if (Physics.CapsuleCast(playerTop, playerbottom, _playerCapsuleCollider.radius,
                -_playerCapsuleCollider.transform.up,100f,_planeLayerMask) == false)
        {
            _player.transform.position -= playergravity;
        }

       







        if (Input.GetKey(KeyCode.W))
        {
            _player.transform.position += new Vector3(0, 0, 1) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _player.transform.position += new Vector3(-1, 0, 1) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _player.transform.position += new Vector3(0, 0, -1) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _player.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.transform.position += new Vector3(0, Mathf.Sqrt(jump * -2f * gravity), 0 * Time.deltaTime * jump);
        }
        

    }
}
