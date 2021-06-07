using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : NetworkBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    [SerializeField] PlayInPut input; 

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelertationTime = 3f;
    [SerializeField] float paddingX = 0.2f;
    [SerializeField] float paddingY = 0.2f;
    [SerializeField] float moveRotationAngle = 90f;

    new Rigidbody2D rigidbody;

    Coroutine moveCoroutine;
    Coroutine TurmMoveCoroutine;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }

    void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidbody.gravityScale = 0f; // ����
        input.EnableGameplayInput(); // ����GamePlay������
        if(!isLocalPlayer)
        {
            OnDisable();
        }
    }

    /*
    void Update() {}
     */
    // ����Update����Ϸÿһ֡����һ�Σ����Ծ������ã���Э�̵ķ�ʽ����Viewport.Instance����

    // Update is called once per frame
    void Move(Vector2 moveInput)
    {
        // Vector2 moveAmount = moveInput * moveSpeed; //�ƶ���
        // rigidbody.velocity = moveAmount;

        if(moveCoroutine!=null)
        {
            StopCoroutine(moveCoroutine); //ֹͣЭ��
        }
        animator.SetBool("Ismove", true);

        Quaternion moveRocation = Quaternion.AngleAxis(moveRotationAngle * moveInput.x, Vector3.forward);
        Quaternion TurnMoveRocation = Quaternion.AngleAxis(moveRotationAngle * moveInput.x*-2f, Vector3.down);

        // transform.rotation = TurnMoveRocation;
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveInput.normalized * moveSpeed, moveRocation*TurnMoveRocation));//normalized���ֱ��ͼ�������Ķ�ά��������һ��
        StartCoroutine(MovePositionLimitCoroutine()); //ֻ������ط�����Э��
    }

    void StopMove()
    {
        // rigidbody.velocity = Vector2.zero;

        animator.SetBool("Ismove", false);

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);//ֹͣЭ��

        }
        
        moveCoroutine = StartCoroutine(MoveCoroutine(decelertationTime, Vector2.zero,Quaternion.identity));

        StopCoroutine(MovePositionLimitCoroutine());//ֻ������ط�ͣ��Э��

    }

    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity,Quaternion moveRotation)//���ٻ��߼��ٵ�
    {
        float t = 0f;
        while(t<time)
        {
            t += Time.fixedDeltaTime / time;
           rigidbody.velocity= Vector2.Lerp(rigidbody.velocity, moveVelocity, t);//��0��1

            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t / time);
           // transform.rotation = Quaternion.Lerp(transform.rotation, TurnmoveRotation, t / time);


            yield return null;
        }
    }

   /* IEnumerator TurnMoveCoroutine(float time, Quaternion moveRotation)//ʵ��ת��
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.fixedDeltaTime / time;
           // rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, moveVelocity, t);//��0��1
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t/time);
            


            yield return null;
        }
    }*/

    IEnumerator MovePositionLimitCoroutine()//��λ����Э��
    {
        while (true)
        {

            // transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position,paddingX,paddingY);

            yield return null;//�� return ʱ�����浱ǰ������״̬���´ε���ʱ�����ӵ�ǰλ�ô����ڴ˴��൱�ڹ���ֱ����һ֡
        }
    }


}
