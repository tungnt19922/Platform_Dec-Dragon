using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;

    //Room camera
    private float currentPosX;
    private Vector3 velocity =Vector3.zero;

    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX,transform.position.y, transform.position.z), ref velocity, speed);

        //Follow Player
        transform.position = new Vector3 (player.position.x + lookAhead, transform.position.y, transform.position.z );
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), cameraSpeed * Time.deltaTime);
    }

    public void MoveToNextRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }

}
