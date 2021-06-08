using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformGate : MonoBehaviour
{
    [SerializeField] float preparedTime = 2f;

    [SerializeField] GameObject targetGate;

    float waitingTime;
    public bool isEnabled { set; get; }

    IEnumerator TeleportCoroutine(GameObject player)
    {
        Vector3 vector = new Vector3(targetGate.transform.position.x, targetGate.transform.position.y, transform.position.z);
        player.transform.position = vector;
        yield return null;
    }

    void OnEnable()
    {
        isEnabled = true;
    }

    /*
    void OnEnable()
    {
        GameObject obj = GameObject.Find("Particle System_2");
        obj.TryGetComponent<TransformGate_2>(out TransformGate_2 gate_1);
        destination = gate_1;

        GameObject obj1 = GameObject.Find("Particle System_3");
        obj1.TryGetComponent<TransformGate_3>(out TransformGate_3 gate_2);
        startGate = gate_2;

        canCheckAnother = true;
    }
     */

    void OnTriggerEnter2D(GameObject other)
    {
        if (isEnabled)
        {
            waitingTime = 0f;
        }
    }

    void OnTriggerStay2D(GameObject other)
    {
        if (isEnabled)
        {
            if (waitingTime >= preparedTime) //如果倒计时结束
            {
                StartCoroutine(TeleportCoroutine(other));
            }
            else
            {
                waitingTime += Time.deltaTime;
            }
        }
    }

    void TriggerExit2D(GameObject other)
    {
        waitingTime = 0f;
    }

}
