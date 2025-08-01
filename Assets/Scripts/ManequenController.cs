using UnityEngine;

public class ManequenController : MonoBehaviour
{
    public float maxHP = 40f; // максимальное HP манекена 
    private float currentHP; // текущее HP манекена
    public ManequenRespawner respawner; // респавнер
    private Animator animator; // аниматор

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    // Получение урона
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (currentHP <= 0) return;

        currentHP -= amount;
        Debug.Log($"Враг получил {amount} урона. Осталось HP: {currentHP}");

        // Расчёт угла попадания
        Vector3 toHit = (hitPoint - transform.position).normalized;
        toHit.y = 0;

        float angle = Vector3.SignedAngle(transform.forward, toHit, Vector3.up);

        string direction;
        if (angle >= -45 && angle <= 45)
        {
            animator.SetFloat("GetHitX", angle);
            direction = "спереди";
        }
        else if (angle > 45 && angle < 135)
        {
            animator.SetFloat("GetHitX", angle);
            direction = "справа";
        }
        else if (angle < -45 && angle > -135)
        {
            animator.SetFloat("GetHitX", angle);
            direction = "слева";
        }
        else
        {
            animator.SetFloat("GetHitX", angle);
            direction = "сзади";
        }

        animator.SetLayerWeight(1, 1f);

        Debug.Log($"Удар пришёл {direction} (угол: {angle:F1} градусов)");

        if (currentHP <= 0)
        {
            Die(hitPoint);
        }
    }

    // Смерть манекена
    private void Die(Vector3 explodePosition)
    {
        Debug.Log("Маненкен уничтожен!");
        GetComponent<HeadExploder>()?.Explode(explodePosition);
        respawner?.RequestRespawn();
        this.enabled = false;

    }

    // Конец удара
    public void HitEnd()
    {
        animator.SetLayerWeight(1, 0f);
    }
}
