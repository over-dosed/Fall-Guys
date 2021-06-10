using UnityEngine;

public class Viewport : Singleton<Viewport>//����ʹ�õĵ���ģʽ
{
    float minX;
    float minY;
    float maxX;
    float maxY;

    void Start() {
        Camera mainCamera = Camera.main;
        Vector2 BL = mainCamera.ViewportToWorldPoint(new Vector3(0, 0));//�ӿ����½�
        Vector2 TR = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));//�ӿ����Ͻ�
        minX = BL.x;
        minY = BL.y;
        maxX = TR.x;
        maxY = TR.y;
    }
    public Vector3 PlayerMoveablePosition(Vector3 playerPosition, float paddingX, float paddingY)//��������ƶ�λ��
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);//����������ֵ������ĳһλ����
        position.y = Mathf.Clamp(playerPosition.y, minY + paddingY, maxY - paddingY);//����������ֵ������ĳһλ����


        return position;
    }


}
