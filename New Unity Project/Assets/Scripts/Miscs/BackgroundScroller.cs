using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    Material material;
    [SerializeField] Vector2 scrollVelocity;//2ά��������¶���ϲ㴦�����ٶ�



    // Start is called before the first frame update
    void Awake() {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {
        material.mainTextureOffset += scrollVelocity * Time.deltaTime;
    }
}
