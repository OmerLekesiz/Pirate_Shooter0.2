using System.Collections;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    public float moveDistance = 2f; // Ne kadar yukarı çıkıp aşağı insin
    public float moveSpeed = 2f; // Hız
    public float waitTime = 1f; // Her hareket sonrası bekleme süresi

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingUp = true; // Başlangıçta yukarı hareket etsin

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveDistance;
        StartCoroutine(MoveUpDown());
    }

    IEnumerator MoveUpDown()
    {
        while (true)
        {
            // Hedef konuma doğru hareket et
            Vector3 destination = movingUp ? targetPos : startPos;

            while (Vector3.Distance(transform.position, destination) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Yön değiştir
            movingUp = !movingUp;

            // Bekleme süresi
            yield return new WaitForSeconds(waitTime);
        }
    }
}
