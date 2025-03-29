using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int score = 0;
    public TextMeshPro scoreText;
    public Cannon.ColorType colorType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannon"))
        {
            if(colorType == other.GetComponent<Cannon>().colorType)
            {
                score += other.GetComponent<Cannon>().scoreValue;
            }
            else
            {
                score -= other.GetComponent<Cannon>().scoreValue;
            }            
            scoreText.text = "Score: " + score.ToString();
            Destroy(other.gameObject);
        }            
    }
}
