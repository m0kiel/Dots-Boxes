using System.Collections;
using UnityEngine;

public class BaseParticle : MonoBehaviour
{
    protected float particleLifespan;

    public void SetParticleLifeSpan(float lifeSpan)
    {
        particleLifespan = lifeSpan;
    }

    public IEnumerator StartParticleLife()
    {
        yield return new WaitForSeconds(particleLifespan);

        Destroy(gameObject);
    }
}
