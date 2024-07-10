using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] public Transform goal;
    [SerializeField] public float speed;
    [SerializeField] public float rotSpeed;
    [SerializeField] public float acceleration;
    [SerializeField] public float deceleration;
    [SerializeField] public float brakeAngle;
    public float minSpeed = 0;
    public float maxSpeed = 1;

    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);
        if(Vector3.Angle(goal.forward, this.transform.forward) < brakeAngle && speed > 0.5f)
        {
            speed = Mathf.Clamp(speed - (deceleration * Time.deltaTime),minSpeed,maxSpeed);
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);
        }
        this.transform.Translate(0,0,speed);
    }
}
