using UnityEngine;
using Unity.Cinemachine; // 유니티 6 버전은 이 네임스페이스를 사용합니다

public class CameraViewSwitcher : MonoBehaviour
{
    private CinemachineCamera vcam;
    private CinemachineFollow followComponent;

    // 시점 상태 (0: 사선, 1: 탑다운, 2: 정면)
    private int viewMode = 0;

    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
        // Follow Offset을 조절하기 위해 Follow 컴포넌트를 가져옵니다
        followComponent = GetComponent<CinemachineFollow>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            viewMode = (viewMode + 1) % 3; // 0, 1, 2 순환
            SwitchView();
        }
    }

    void SwitchView()
    {
        switch (viewMode)
        {
            case 0: // 사선 시점 (현재 설정)
                SetCamera(new Vector3(0, 3, -3), new Vector3(35, 0, 0));
                break;
            case 1: // 수직 시점 (탑다운)
                SetCamera(new Vector3(0, 4, 0), new Vector3(90, 0, 0));
                break;
            case 2: // 정면 시점
                SetCamera(new Vector3(0, 0, -4), new Vector3(0, 0, 0));
                break;
        }
    }

    void SetCamera(Vector3 offset, Vector3 rotation)
    {
        if (followComponent != null)
        {
            followComponent.FollowOffset = offset;
        }
        transform.localEulerAngles = rotation;
    }
}