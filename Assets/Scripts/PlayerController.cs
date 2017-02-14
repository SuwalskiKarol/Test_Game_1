using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [Header("Player options")]
    [Range(0,20)]
    public float playerRotateSpeed;
    [Range(0, 20)]
    public float playerMovementSpeed;
    [Range(0, 20)]
    public float jumpForce;

    private Rigidbody rig;
    private GameObject textObject;
    private GameObject obstacle;
    private bool stopNearWall = false;
    private List<string> textList = new List<string> { "Run!","Try not to fall", "Well Done", "You Died :(" };

	void Start () {
       
        rig = GetComponent<Rigidbody>();
        textObject = GameObject.Find("dontFall");
        obstacle = GameObject.Find("Obstacle");
        textObject.GetComponent<Text>().text = textList[0];

        StartCoroutine(PlayerFalling());
    }

    void FixedUpdate () {
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.6f);
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (!stopNearWall)
            transform.Translate(new Vector3(0f, 0f, verticalMove) * playerMovementSpeed * Time.deltaTime);

        transform.Rotate(0f, horizontalMove * playerRotateSpeed, 0f);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
       {
            rig.AddForce(new Vector3(0,1,0) * jumpForce, ForceMode.Impulse);
       }

        WallColliderIssues();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "EndCube")
        {
            textObject.GetComponent<Text>().text = textList[2];
            Time.timeScale = 0;
        }
        else if (collision.transform.name == "Obstacle")
        {
            textObject.GetComponent<Text>().text = textList[3];
            Destroy(this.gameObject);
        }      
    }

    private void WallColliderIssues()
    {
        RaycastHit hit;
        Vector3 look = transform.TransformDirection(new Vector3(0, -1, 1));
        if (Physics.Raycast(transform.position, look, out hit, 1f))
        {
            if (hit.transform.tag == "Wall")
                stopNearWall = true;
        }
        else
            stopNearWall = false;
    }

    IEnumerator PlayerFalling()
    {
        while (true)
        {
            RaycastHit hit;
            Vector3 look = transform.TransformDirection(-Vector3.up);
            if (!Physics.Raycast(transform.position, look, out hit, 300))
            {
                transform.position = new Vector3(0, 0.6f, 0);
                obstacle.transform.position = new Vector3(0f, 0f, 25.5f);
                textObject.GetComponent<Text>().text = textList[1];
            }

            yield return new WaitForSeconds(2f);
            textObject.GetComponent<Text>().text = "";
        }
    }
}
