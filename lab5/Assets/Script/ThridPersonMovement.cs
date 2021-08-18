using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    public Transform cam;


     void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f)* Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Powerups")
        {
            speed = 100f;
            StartCoroutine(ResetPower());
            
        }
    }
    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        speed = 20f;



    }
}
