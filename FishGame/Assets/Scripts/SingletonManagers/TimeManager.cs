using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public bool IsPaused { get; private set; } = false;
    public bool IsSlowed { get; private set; } = false;
    private float slowdownFactor = 0.05f;
    private float slowdownLength = 2f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if (IsSlowed)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = Time.timeScale * .02f;
            if (Time.timeScale == 1f)
            {
                IsSlowed = false;
            }
        }
    }

    public void DoSlowmotion(float factor, float duration)
    {
        IsSlowed = true;
        slowdownFactor = factor;
        slowdownLength = duration;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void Pause(bool pause)
    {
        IsPaused = pause;
        if (pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}