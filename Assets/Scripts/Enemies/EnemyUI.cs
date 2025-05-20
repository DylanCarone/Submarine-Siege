using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetScore(int score)
    {
        if (scoreText)
        {
            scoreText.text = score.ToString();
        }
    }
}
