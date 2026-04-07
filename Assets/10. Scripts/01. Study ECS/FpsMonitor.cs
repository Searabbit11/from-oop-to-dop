using System;
using UnityEngine;

public class FpsMonitor : MonoBehaviour
{
    [Header("Settings")]
    public float UpdateInterval = 0.5f;
    public int FontSize = 24;
    public Vector2 Position = new Vector2(10, 10);
    public int BufferSize = 500;

    // Private fields (camelCase, No underscore prefix)
    private float timeAccumulator;
    private int frameCount;
    private float currentFps;
    private float averageFps;
    private float low1PercentFps;

    private float[] frameTimes;
    private float[] sortedFrameTimes;
    private int bufferIndex;
    private bool isBufferFull;

    private GUIStyle guiStyle = new GUIStyle();

    private void Start()
    {
        frameTimes = new float[BufferSize];
        sortedFrameTimes = new float[BufferSize];
        guiStyle.fontStyle = FontStyle.Bold;
    }

    private void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        timeAccumulator += deltaTime;
        frameCount++;
        
        frameTimes[bufferIndex] = deltaTime;
        bufferIndex = (bufferIndex + 1) % BufferSize;
        if (bufferIndex == 0) isBufferFull = true;
        
        if (timeAccumulator >= UpdateInterval)
        {
            currentFps = frameCount / timeAccumulator;
            CalculateAverageAndLowFps();
            
            timeAccumulator = 0f;
            frameCount = 0;
        }
    }

    private void CalculateAverageAndLowFps()
    {
        int count = isBufferFull ? BufferSize : bufferIndex;
        if (count == 0) return;
        
        Array.Copy(frameTimes, sortedFrameTimes, count);
        Array.Sort(sortedFrameTimes, 0, count);

        float totalTime = 0f;
        for (int i = 0; i < count; i++)
        {
            totalTime += sortedFrameTimes[i];
        }
        averageFps = count / totalTime;

        // 1% Low 프레임 계산 (가장 오래 걸린 프레임들 중 하위 1% 경계값)
        int index1Percent = Mathf.FloorToInt(count * 0.99f);
        if (index1Percent >= count) index1Percent = count - 1;

        float worstFrameTime = sortedFrameTimes[index1Percent];
        low1PercentFps = worstFrameTime > 0 ? 1f / worstFrameTime : 0f;
    }

    private void OnGUI()
    {
        guiStyle.fontSize = FontSize;

        float lineSpacing = FontSize + 5f;
        
        DrawFpsText($"Current FPS: {Mathf.RoundToInt(currentFps)}", Position, currentFps);
        DrawFpsText($"Average FPS: {Mathf.RoundToInt(averageFps)}", Position + new Vector2(0, lineSpacing), averageFps);
        DrawFpsText($"1% Low FPS: {Mathf.RoundToInt(low1PercentFps)}", Position + new Vector2(0, lineSpacing * 2), low1PercentFps);
    }

    private void DrawFpsText(string text, Vector2 position, float fps)
    {
        guiStyle.normal.textColor = GetFpsColor(fps);
        GUI.color = Color.black;
        GUI.Label(new Rect(position.x + 2, position.y + 2, 300, 50), text, guiStyle);
        
        GUI.color = Color.white;
        GUI.Label(new Rect(position.x, position.y, 300, 50), text, guiStyle);
    }

    private Color GetFpsColor(float fps)
    {
        return fps >= 60f ? Color.green : (fps >= 30f ? Color.yellow : Color.red);
    }
}