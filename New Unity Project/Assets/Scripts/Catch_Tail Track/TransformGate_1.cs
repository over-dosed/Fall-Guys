using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformGate_1 : MonoBehaviour
{

    [SerializeField] float teleportCountDown = 2f;

    [SerializeField] TransformGate_2 destination;
    [SerializeField] TransformGate_3 startGate;
    float teleporTimer;
    public bool canCheckAnother{set; get;}

    IEnumerator TeleportCoroutine(Transform transform)
    {
        Vector3 vector = new Vector3(destination.transform.position.x, destination.transform.position.y, transform.position.z);
        transform.position = vector;
        yield return null;
    }

    void OnEnable()
    {
       GameObject obj = GameObject.Find("Particle System_2");
        obj.TryGetComponent<TransformGate_2> (out TransformGate_2 gate_1);
        destination = gate_1;

        GameObject obj1 = GameObject.Find("Particle System_3");
        obj1.TryGetComponent<TransformGate_3>(out TransformGate_3 gate_2);
        startGate = gate_2;

        canCheckAnother = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(canCheckAnother)
        {
            if(destination.canCheckAnother)
            {
                teleporTimer = 0f;
                
            }
           
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(canCheckAnother) 
        {
            if(teleporTimer >= teleportCountDown) //如果倒计时结束
            {
                StartCoroutine(TeleportCoroutine(other.transform));
                canCheckAnother = false;
            }
            else
            {
                teleporTimer += Time.deltaTime;

            }
        }
    }

    void TriggerExit2D(Collider2D other)
    {
        teleporTimer = 0f;

        startGate.canCheckAnother = true;
    }
}
