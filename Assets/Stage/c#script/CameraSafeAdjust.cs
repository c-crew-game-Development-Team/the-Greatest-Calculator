using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    private Camera mainCamera;
    private float targetAspect = 2600f / 1179f; // 목표 비율 (2556x1179)
    private int lastScreenWidth;
    private int lastScreenHeight;
    private Rect lastSafeArea;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        lastSafeArea = Screen.safeArea;
        UpdateViewport();
    }

    void Update()
    {
        // 화면 크기가 변경되었는지 감지
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight || Screen.safeArea != lastSafeArea)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            lastSafeArea = Screen.safeArea;
            UpdateViewport();
        }
    }

    void UpdateViewport()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        // 현재 화면 비율이 목표 비율보다 넓을 경우 (가로가 더 긴 경우)
        if (currentAspect > targetAspect)
        {
            float scaleHeight = targetAspect / currentAspect;
            mainCamera.rect = new Rect((1 - scaleHeight) / 2, 0, scaleHeight, 1);
        }
        // 현재 화면 비율이 목표 비율보다 좁을 경우 (세로가 더 긴 경우)
        else
        {
            float scaleWidth = currentAspect / targetAspect;
            mainCamera.rect = new Rect(0, (1 - scaleWidth) / 2, 1, scaleWidth);
        }

        //ApplySafeArea();왜 안 되냐..
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        float safeX = safeArea.x / Screen.width;
        float safeY = safeArea.y / Screen.height;
        float safeWidth = safeArea.width / Screen.width;
        float safeHeight = safeArea.height / Screen.height;

        // 카메라의 Viewport Rect를 Safe Area에 맞게 조정
        mainCamera.rect = new Rect(safeX, safeY, safeWidth, safeHeight);
    }

    void OnPreCull() => GL.Clear(true, true, Color.black); // 검은색 여백 채우기
}
