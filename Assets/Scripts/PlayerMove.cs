using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(h, 0, v);
        
        // Transform 대신 Rigidbody를 사용하는 것이 충돌에 더 정확하지만, 
        // 입문 단계에서는 이동을 확인하기 위해 간단히 작성합니다.
        transform.Translate(moveDir * speed * Time.deltaTime);
    }
}