using TMPro;
using UnityEngine;

public class WrongInteractionParticle : BaseParticle
{
    [SerializeField] AnimationCurve scaleCurve;
    TMP_Text textBox;

    float textFontSize = 40f;
    float currentLife = 0;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        currentLife += Time.deltaTime;
        textBox.fontSize = textFontSize * scaleCurve.Evaluate(currentLife * 1/particleLifespan); // Lifespan is 0.3
    }
}
