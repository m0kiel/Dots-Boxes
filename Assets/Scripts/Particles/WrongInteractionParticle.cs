using TMPro;
using UnityEngine;

public class WrongInteractionParticle : BaseParticle
{
    [SerializeField] private AnimationCurve scaleCurve;
    private TMP_Text textBox;

    private float textFontSize = 40f;
    private float currentLife = 0;

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
