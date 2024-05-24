using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    [SerializeField] float MovementSpeed;
    [SerializeField] float RotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetLook =  new Vector3 (target.position.x, this.transform.position.y, target.position.z);
        Vector3 direction = targetLook - transform.position;

        this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * RotSpeed);

        if(Vector3.Distance(targetLook, transform.position) > 3)
        {
            transform.Translate(direction.normalized * MovementSpeed * Time.deltaTime, Space.World);
        }
    }
}
