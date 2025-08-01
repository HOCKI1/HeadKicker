using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitToggler : MonoBehaviour
{
    public PlayerController player; // Игрок
    public float slowMotionTimeScale = 0.05f; // Сила слоумо
    public float slowMotionDuration = 0.6f; // Длительность слоумо

    public void IsNotHitting()
    {
        player.isHitting = false;
    }

    // Запуск слоумо корутины
    public void TriggerSlowMotion()
    {
        StartCoroutine(SlowMo());
    }

    private IEnumerator SlowMo()
    {
        // Запуск слоумо
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = 0.02f * slowMotionTimeScale;

        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Остановка слоумо
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
