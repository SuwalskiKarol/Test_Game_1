using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Platform : MonoBehaviour {

    [Header("Platform options")]
    [Range(1, 20)]
    public float bouncingForce;

    private Animator anim;

    void Start () {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                anim.SetBool("Jump", true);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("platformMoving") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >=0.5)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * bouncingForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Jump", false);               
        }   
    }

}
