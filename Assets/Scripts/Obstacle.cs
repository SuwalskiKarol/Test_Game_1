using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public int obstacleSpeed;

	void Update () {
        transform.Translate(-Vector3.forward * obstacleSpeed * Time.deltaTime);
        if (transform.position.z <= -70)
            Destroy(this.gameObject);
	}
}