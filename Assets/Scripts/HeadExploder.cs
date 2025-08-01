using UnityEngine;

public class HeadExploder : MonoBehaviour
{
    public GameObject chunkedHeadPrefab; // Префаб с кусочками
    public GameObject originalHead; // Оригинальная голова
    public GameObject headSocket; // Сокет для новой головы

    public float explosionForce = 500f; // Сила взрыва
    public float explosionRadius = 2f; // Радиус взрыва
    public float fragmentLifetime = 5f; // Время жизни кусочков

    public void Explode(Vector3 explodePosition)
    {
        Debug.Log("Позиция взрыва: " + explodePosition);

        // Скрытие оригинальной головы
        if (originalHead != null)
            originalHead.SetActive(false);

        // Создание головы с кусками
        GameObject chunkedHead = Instantiate(chunkedHeadPrefab, headSocket.transform.position, headSocket.transform.rotation);

        // Применение взрывной силы ко всем кусочкам
        foreach (Rigidbody rb in chunkedHead.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 explosionPos = explodePosition;
            rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            Destroy(rb.gameObject, fragmentLifetime);
        }

        Destroy(chunkedHead, fragmentLifetime + 0.5f);
    }


}
