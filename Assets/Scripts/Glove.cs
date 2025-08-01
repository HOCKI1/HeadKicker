using UnityEngine;

public class Glove : MonoBehaviour
{
    public string targetTag = "Head"; // Тег головы врага
    public PlayerController playerController; // Контроллер игрока

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (playerController.isHitting)
            {
                ManequenController enemy = other.GetComponentInParent<ManequenController>();
                enemy.TakeDamage(10f, playerController.transform.position); // Нанесение урона
            }
        }
    }
}
