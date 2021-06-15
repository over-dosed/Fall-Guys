using System.Collections;
using UnityEngine;

public class TransformGate_3 : MonoBehaviour {
    [SerializeField] float teleportCountDown = 5f;

    [SerializeField] TransformGate_1 destination;
    [SerializeField] TransformGate_2 startGate;
    float teleporTimer;
    public bool canCheckAnother { set; get; }

    IEnumerator TeleportCoroutine(Transform transform) {
        int random = (int)Random.Range(0, 2);
        Vector3 vector = new Vector3(0, 0, 0);
        switch (random) {
            case 0: vector = new Vector3(destination.transform.position.x, destination.transform.position.y, transform.position.z); break;
            case 1: vector = new Vector3(startGate.transform.position.x, startGate.transform.position.y, transform.position.z); break;
        }
        //Vector3 vector = new Vector3(destination.transform.position.x, destination.transform.position.y, transform.position.z);
        transform.position = vector;
        yield return null;
    }

    void OnEnable() {
        GameObject obj = GameObject.Find("Particle System_1");
        obj.TryGetComponent<TransformGate_1>(out TransformGate_1 gate_1);
        destination = gate_1;

        GameObject obj1 = GameObject.Find("Particle System_2");
        obj1.TryGetComponent<TransformGate_2>(out TransformGate_2 gate_2);
        startGate = gate_2;

        canCheckAnother = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (canCheckAnother) {
            if (destination.canCheckAnother) {
                teleporTimer = 0f;

            }

        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (canCheckAnother) {
            if (teleporTimer >= teleportCountDown) //如果倒计时结束
            {
                StartCoroutine(TeleportCoroutine(other.transform));
                //canCheckAnother = false;
                teleporTimer = 0f;
                startGate.canCheckAnother = true;
            } else {
                teleporTimer += Time.deltaTime;

            }
        }
    }

    void TriggerExit2D(Collider2D other) {
        teleporTimer = 0f;

        startGate.canCheckAnother = true;
    }
}
