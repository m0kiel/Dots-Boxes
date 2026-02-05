using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CompleteSquareParticle : BaseParticle
{
    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField] AnimationCurve alphaCurve;
    TMP_Text textBox;

    float textFontSize = 60f;
    float currentLife = 0;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        currentLife += Time.deltaTime;
        textBox.fontSize = textFontSize * scaleCurve.Evaluate(currentLife);
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, alphaCurve.Evaluate(currentLife));
    }
}
