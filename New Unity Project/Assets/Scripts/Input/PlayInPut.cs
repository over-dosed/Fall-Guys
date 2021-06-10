using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Player Input")]
public class PlayInPut : ScriptableObject, MyInputActions.IGamePlayActions {
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };
    //public event System.Action
    MyInputActions myInputActions;//之前写的类 
    void OnEnable() {
        myInputActions = new MyInputActions();
        myInputActions.GamePlay.SetCallbacks(this);

    }

    public void DisableAllInputs()//禁用所有输入类
    {
        myInputActions.GamePlay.Disable();
    }

    void OnDisable() {
        DisableAllInputs();
    }


    public void EnableGameplayInput() {
        myInputActions.GamePlay.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void OnMove(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)//希望玩家按住按键持续移动
        {
            //if(onMove!=null)
            onMove.Invoke(context.ReadValue<Vector2>());
        }

        if (context.phase == InputActionPhase.Canceled)//希望玩家按住按键持续移动
        {
            //if(onStopMove!=null)
            onStopMove.Invoke();
        }



        //throw new System.NotImplementedException();
    }
}
