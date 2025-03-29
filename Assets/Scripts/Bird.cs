using UnityEngine;

public class BirdMove : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;
    public float waveHeight = 0.5f; // Dalga yüksekliği
    public float waveFrequency = 2f; // Dalga frekansı

    private float t = 0f;
    private bool isFlipped = false;
    private float initialRotationY; // Başlangıç rotasyonunu saklamak için

    void Start()
    {
        // Başlangıçta Y eksenini 180 derece ters yap
        initialRotationY = transform.eulerAngles.y + 180;  
        transform.rotation = Quaternion.Euler(0, initialRotationY, 0);
    }

    void Update()
    {
        t += Time.deltaTime * speed;
        float pingPongValue = Mathf.PingPong(t, 1f);

        // İleri geri hareket
        Vector3 newPos = Vector3.Lerp(pointA, pointB, pingPongValue);

        // Yukarı-aşağı dalga efekti (Sin fonksiyonu ile)
        newPos.y += Mathf.Sin(Time.time * waveFrequency) * waveHeight;

        transform.position = newPos;

        // Yön değiştirdiğinde döndür
        if ((pingPongValue >= 0.99f || pingPongValue <= 0.01f) && !isFlipped)
        {
            Flip();
            isFlipped = true;
        }
        else if (pingPongValue > 0.01f && pingPongValue < 0.99f)
        {
            isFlipped = false;
        }
    }

    void Flip()
    {
        // Sadece Y ekseninde 180 derece çevir
        float newYRotation = transform.eulerAngles.y + 180;
        transform.rotation = Quaternion.Euler(0, newYRotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Eğer çarpan obje "Cannon" tag'ına sahipse
        if (other.CompareTag("Cannon"))
        {
            // GameManager'ı bul ve hit sayısını artır
            FindObjectOfType<GameManager>().RegisterHit();

            // "Cannon" nesnesini yok et
            Destroy(other.gameObject);
        }
    }
}