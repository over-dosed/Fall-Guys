using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    Material material;
    [SerializeField] Vector2 scrollVelocity;//2维变量、暴露给上层处理、卷动速度



    // Start is called before the first frame update
    void Awake() {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {
        material.mainTextureOffset += scrollVelocity * Time.deltaTime;
    }
}
