using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    
    [Header("Camera options")]
    [Range(0, 20)]
    public float cameraSpeed;

    GameObject player;
 
    void Start () {
        player = GameObject.Find("Player");
	}
	
	void Update () {
        if (player != null)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, player.transform.position.z), cameraSpeed* Time.deltaTime);
	}
}
