using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : NetworkBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Quaternion initialQuaternion = new Quaternion(Quaternion.identity.x, Quaternion.identity.y + 5.4f, Quaternion.identity.z, Quaternion.identity.w);
    [SerializeField] PlayInPut input;
    [SerializeField] float a;
    [SerializeField] Vector2 b;
    [SerializeField] bool c;

    bool grounded = true;
   // [SerializeField] float jumpSpeed = 1000f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelertationTime = 3f;
    [SerializeField] float paddingX = 0.2f;
    [SerializeField] float paddingY = 0.1f;
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
        rigidbody.gravityScale = 5f; // 重力
        input.EnableGameplayInput(); // 激活GamePlay动作表
        if(!isLocalPlayer)
        {
            OnDisable();
        }
    }

    /*
    void Update() {}
     */
    // 由于Update是游戏每一帧调用一次，所以尽量少用，用协程的方式调用Viewport.Instance单例

    // Update is called once per frame
    void Move(Vector2 moveInput)
    {
        // Vector2 moveAmount = moveInput * moveSpeed; //移动量
        // rigidbody.velocity = moveAmount;
        if (moveInput.y >= 10)
        {
            Jump();
        }
        if (moveCoroutine!=null)
        {
            StopCoroutine(moveCoroutine); //停止协程
        }
        animator.SetBool("Ismove", true);

        Quaternion moveRocation = Quaternion.AngleAxis(moveRotationAngle * moveInput.x, Vector3.forward);
        Quaternion TurnMoveRocation = Quaternion.AngleAxis(moveRotationAngle * moveInput.x*-2f, Vector3.down);

        // transform.rotation = TurnMoveRocation;
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveInput.normalized * moveSpeed, moveRocation*TurnMoveRocation));//normalized让手柄和键盘输入的二维向量保持一致
        StartCoroutine(MovePositionLimitCoroutine()); //只在这个地方启用协程
    }

    void StopMove()
    {
        // rigidbody.velocity = Vector2.zero;

        animator.SetBool("Ismove", false);

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);//停止协程

        }
        
        moveCoroutine = StartCoroutine(MoveCoroutine(decelertationTime, Vector2.zero, initialQuaternion));

        StopCoroutine(MovePositionLimitCoroutine());//只在这个地方停用协程

    }

    void Jump()
    {
        if (grounded == true)
        {
            var component = transform.GetComponent<Rigidbody2D>();
            component.AddForce(new Vector2(0, 100), ForceMode2D.Impulse);
            grounded = false;
            animator.SetBool("IsFalling", true);

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, Vector3.left);
    }


    void Update()
    {
        c = grounded;
        Vector2 origin = transform.position;
        Vector2 offsetted = new Vector2(origin.x, origin.y - paddingY);
        RaycastHit2D onGround2 = Physics2D.Raycast(offsetted, -Vector2.up);
        //print("b" + transform.position + ",c=" + colliderOffset + ",d=" + onGround2.distance);
                    b = onGround2.point;
            a=onGround2.distance;
        if (onGround2.distance <= 0.3)
        {

            grounded = true;
            animator.SetBool("IsFalling", false);
        }
    }


    IEnumerator OnGround()
    {
       // this.
        yield return null;
    }
    void  OnCollisionEnter()
    {
        grounded = true;
       // Debug.Log("I'm colliding with something!");
    }


    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity, Quaternion moveRotation)//加速或者减速的
    {
        Vector2 xvector = new Vector2(moveVelocity.x, 0);
        Vector2 yvector = new Vector2(0, moveVelocity.y);
        if(moveVelocity.y>=10)
        {
            Jump();
        }
        float t = 0f;
        while (t < time)
        {
            /* if(findFloor.onfloor==true)
             {

                 var component = transform.GetComponent<Rigidbody2D>();
                 component.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
             }*/
            t += Time.fixedDeltaTime / time;
            rigidbody.velocity = new Vector2(Vector2.Lerp(rigidbody.velocity, xvector, t).x, rigidbody.velocity.y);
            //rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, xvector, t);//从0到1

            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t / time);
            // transform.rotation = Quaternion.Lerp(transform.rotation, TurnmoveRotation, t / time);


            yield return null;
        }
    }

    /* IEnumerator TurnMoveCoroutine(float time, Quaternion moveRotation)//实现转向
     {
         float t = 0f;
         while (t < time)
         {
             t += Time.fixedDeltaTime / time;
            // rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, moveVelocity, t);//从0到1
             transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t/time);



             yield return null;
         }
     }*/

    IEnumerator MovePositionLimitCoroutine()//移位限制协程
    {
        while (true)
        {

            // transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position,paddingX,paddingY);

            yield return null;//在 return 时，保存当前函数的状态，下次调用时继续从当前位置处理。在此处相当于挂起直到下一帧
        }
    }


}
