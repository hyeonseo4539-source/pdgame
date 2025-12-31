using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject objectToPlace; // 배치할 물건 (에디터에서 할당)

    void Update()
    {
        // 마우스 왼쪽 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 1. 클릭한 위치를 가져옴
                Vector3 clickPosition = hit.point;

                // 2. 좌표를 반올림하여 그리드(정수) 위치로 변환
                float x = Mathf.Round(clickPosition.x);
                float z = Mathf.Round(clickPosition.z);

                // 3. 해당 위치에 물체 생성 (y값은 바닥 위로 0.5 정도)
                Vector3 spawnPos = new Vector3(x, 0.5f, z);
                Instantiate(objectToPlace, spawnPos, Quaternion.identity);
                
                Debug.Log($"그리드 설치 위치: {x}, {z}");
            }
        }
    }
}