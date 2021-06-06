using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
     [SerializeField] float moveSpeed;
     [SerializeField] Vector2 moveDirection;
       [SerializeField] float onceMoveTime; //单次运行时间

    void OnEnable()
        {
            StartCoroutine(MoveDirectly());
        }


        IEnumerator MoveDirectly()
        {
            int count = 0;
             while (gameObject.activeSelf)
            {
            float time = Time.fixedTime - count * onceMoveTime;
                if (time > onceMoveTime)
                {
                count++;
                moveDirection = moveDirection * -1; 
                }
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
}
