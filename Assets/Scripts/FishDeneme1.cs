using UnityEngine;
using System.Collections;

public class FishMove : MonoBehaviour
{
    [Header("X-Z Hareket Ayarları")]
    public Vector3 pointA;
    public Vector3 pointB;
    public float moveSpeed = 2f;

    [Header("Y Hareket Ayarları")]
    public float yAmplitude = 1f; // Y ekseninde hareket miktarı
    public float yFrequency = 1f; // Y ekseninde hız
    public float bottomWaitTime = 1f; // Alt noktada bekleme süresi

    private float t = 0f;
    private float yStart;
    private bool isWaiting = false;
    private float yTimer = 0f;

    void Start()
    {
        yStart = transform.position.y; // Başlangıçtaki Y konumunu sakla
    }

    void Update()
    {
        // X-Z ekseninde sürekli hareket
        t += Time.deltaTime * moveSpeed;
        float pingPongValue = Mathf.PingPong(t, 1f);
        Vector3 newPos = Vector3.Lerp(pointA, pointB, pingPongValue);

        // Eğer Y ekseni bekleme durumundaysa hareket etme
        if (!isWaiting)
        {
            float yOffset = Mathf.Sin(yTimer * yFrequency) * yAmplitude;
            newPos.y = yStart + yOffset;
            yTimer += Time.deltaTime;

            // Eğer balık en alt noktaya ulaştıysa bekleme sürecini başlat
            if (yOffset <= -yAmplitude + 0.1f) // Y en alt noktaya yakınsa
            {
                StartCoroutine(WaitAtBottom());
            }
        }
        else
        {
            newPos.y = yStart - yAmplitude; // En alt noktada beklerken sabit kal
        }

        // Yeni pozisyonu uygula
        transform.position = newPos;
    }

    IEnumerator WaitAtBottom()
    {
        isWaiting = true; // Y ekseni hareketini durdur
        yield return new WaitForSeconds(bottomWaitTime);
        isWaiting = false; // Bekleme süresi bitince tekrar yukarı çık
        yTimer = 0f; // Y hareketini sıfırdan başlat
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