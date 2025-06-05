using UnityEngine;

public class PositionWatcher : MonoBehaviour
{
    private Vector3 lastPosition;
    private int lastFrameChanged = -1;

    void Awake()
    {
        // 在 Awake 时记录初始位置，这样 Start 或 OnEnable 的修改也能被捕获
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        // 使用一个阈值来避免浮点数精度问题
        if (Vector3.SqrMagnitude(transform.position - lastPosition) > 0.0001f)
        {
            // 只在位置真正改变的帧打印日志，避免刷屏
            if (Time.frameCount != lastFrameChanged)
            {
                Debug.LogWarning($"【位置监视器】: 在第 {Time.frameCount} 帧，'{gameObject.name}' 的位置被改变了！" +
                                 $"\n前一帧位置: {lastPosition.ToString("F4")}" +
                                 $"\n当前位置: {transform.position.ToString("F4")}", this);

                // 打印堆栈跟踪，这可能会告诉你调用来源！
                Debug.LogWarning(System.Environment.StackTrace);

                lastFrameChanged = Time.frameCount;
            }

            lastPosition = transform.position;
        }
    }
}