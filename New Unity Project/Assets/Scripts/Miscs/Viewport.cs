using UnityEngine;

public class Viewport : Singleton<Viewport>//方便使用的单例模式
{
    float minX;
    float minY;
    float maxX;
    float maxY;

    void Start() {
        Camera mainCamera = Camera.main;
        Vector2 BL = mainCamera.ViewportToWorldPoint(new Vector3(0, 0));//视口左下角
        Vector2 TR = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));//视口右上角
        minX = BL.x;
        minY = BL.y;
        maxX = TR.x;
        maxY = TR.y;
    }
    public Vector3 PlayerMoveablePosition(Vector3 playerPosition, float paddingX, float paddingY)//限制玩家移动位置
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);//将浮点数的值限制在某一位置内
        position.y = Mathf.Clamp(playerPosition.y, minY + paddingY, maxY - paddingY);//将浮点数的值限制在某一位置内


        return position;
    }


}
