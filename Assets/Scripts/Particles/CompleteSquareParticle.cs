using TMPro;
using UnityEngine;

public class CompleteSquareParticle : BaseParticle
{
    [SerializeField] private AnimationCurve scaleCurve;
    private TMP_Text textBox;

    private float textFontSize = 60f;
    private float currentLife = 0;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        currentLife += Time.deltaTime;
        textBox.fontSize = textFontSize * scaleCurve.Evaluate(currentLife);
    }
}
