using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 1.2f;
    public float jumpDuration = 0.4f;
    
    public LayerMask waterLayer;
    public LayerMask groundLayer;

    private bool isJumping = false;

    void Update()
    {
        if (isJumping) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            transform.forward = moveDir;

            // 1. [핵심] 훨씬 앞쪽(0.8f)을 미리 검사해서 물이 있는지 확인
            if (IsWaterAhead(moveDir))
            {
                // 2. 물을 발견했다면 즉시 멈추고(Translate 실행 안함), 점프 가능한지 체크
                if (CanJumpOver(moveDir, out Vector3 jumpTarget))
                {
                    StartCoroutine(AutoJump(jumpTarget));
                }
                else
                {
                    // 2칸 이상의 물이라면 그냥 멈춤 (아무것도 안 함)
                    Debug.Log("앞에 물이 너무 넓어서 못 가!");
                }
            }
            else
            {
                // 3. 앞에 물이 전혀 없을 때만 자유 이동
                transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
            }
        }
    }

    bool IsWaterAhead(Vector3 dir)
{
    // 1. 시작 높이를 캐릭터 발(transform.position)에서 아주 살짝만 위로(0.2f) 잡습니다.
    // 너무 높으면(0.5f) 물까지 거리가 멀어지고, 너무 낮으면 땅 큐브에 막힐 수 있습니다.
    Vector3 rayStart = transform.position + Vector3.up * 0.2f + dir * 0.8f;
    
    // 2. 레이 길이를 1.5f에서 2.5f로 넉넉하게 늘립니다.
    float rayLength = 2.5f; 
    
    // 씬 뷰에서 선이 물 큐브를 뚫고 지나가는지 확인하세요!
    Debug.DrawRay(rayStart, Vector3.down * rayLength, Color.red);
    
    // 3. Raycast 호출 시에도 똑같은 길이를 적용합니다.
    return Physics.Raycast(rayStart, Vector3.down, rayLength, waterLayer);
}

    bool CanJumpOver(Vector3 dir, out Vector3 target)
    {
        // 2.2칸 앞이 착지 지점
        target = transform.position + dir * 2.2f; 
        
        // 착지 지점 아래에 'Ground' 레이어가 있는지 확인
        Vector3 checkPos = target + Vector3.up * 1.0f;
        Debug.DrawRay(checkPos, Vector3.down * 2.0f, Color.green); // 착지 지점은 초록색 선으로 표시
        
        return Physics.Raycast(checkPos, Vector3.down, 2.0f, groundLayer);
    }

    IEnumerator AutoJump(Vector3 target)
    {
        isJumping = true;
        Vector3 startPos = transform.position;
        float timer = 0f;

        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;
            float percent = timer / jumpDuration;

            Vector3 currentPos = Vector3.Lerp(startPos, target, percent);
            // 점프 곡선
            float y = jumpHeight * (-4 * Mathf.Pow(percent, 2) + 4 * percent);
            currentPos.y = startPos.y + y;

            transform.position = currentPos;
            yield return null;
        }

        transform.position = new Vector3(target.x, startPos.y, target.z);
        isJumping = false;
    }
}