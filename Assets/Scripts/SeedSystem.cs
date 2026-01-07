using UnityEngine;
using TMPro;

// 1. 종류 정의
public enum SeedType { None, Red, Blue, Yellow }

// 2. [병아리용] 인벤토리 시스템
public class SeedSystem : MonoBehaviour
{
    [Header("보유 개수")]
    public int redSeedCount = 0;
    public int blueSeedCount = 0;
    public int yellowSeedCount = 0;

    [Header("UI 텍스트 연결")]
    public TextMeshProUGUI redText;
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI yellowText;

    [Header("현재 선택된 씨앗")]
    public SeedType selectedSeed = SeedType.None;

    void Start() => UpdateUI();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSeed(SeedType.Blue);   // 1번 누르면 Blue
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSeed(SeedType.Red);    // 2번 누르면 Red
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSeed(SeedType.Yellow); // 3번 누르면 Yellow
    }

    void SelectSeed(SeedType type)
    {
        selectedSeed = type;
        Debug.Log("현재 선택된 씨앗: " + selectedSeed);
        // 나중에 여기서 선택된 씨앗에 따라 병아리 색을 바꾸는 코드를 넣으면 됩니다!
    }

    private void OnTriggerEnter(Collider other)
    {
        Seed seed = other.GetComponent<Seed>();
        if (seed != null)
        {
            AddSeed(seed.type, seed.scoreValue);
            Destroy(other.gameObject);
        }
    }

    public void AddSeed(SeedType type, int amount)
    {
        if (type == SeedType.Red) redSeedCount += amount;
        else if (type == SeedType.Blue) blueSeedCount += amount;
        else if (type == SeedType.Yellow) yellowSeedCount += amount;
        UpdateUI();
        Debug.Log(type + " 씨앗 획득!");
    }
    
    public void UpdateUI()
    {
        if (redText != null) redText.text = redSeedCount.ToString();
        if (blueText != null) blueText.text = blueSeedCount.ToString();
        if (yellowText != null) yellowText.text = yellowSeedCount.ToString();
    }
}

// 3. [씨앗용] 정보 스크립트
public class Seed : MonoBehaviour
{
    public SeedType type;
    public int scoreValue = 1;

    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }
}