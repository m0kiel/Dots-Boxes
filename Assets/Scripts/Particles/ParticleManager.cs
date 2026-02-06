using System;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType { CompleteSquare }
public class ParticleManager : Singleton<ParticleManager>
{
    [SerializeField] List<ParticleInfo> particleInfoList;
    Dictionary<ParticleType, ParticleInfo> particles = new();

    [SerializeField] private GameObject worldSpaceCanvas;

    private void Awake()
    {
        SetParticles();
    }

    private void SetParticles()
    {
        for (int i = 0; i < particleInfoList.Count; i++)
        {
            particles.Add(particleInfoList[i].particleType, particleInfoList[i]);
        }
    }

    public void SpawnParticle(ParticleType particleType, Vector3 position)
    {
        GameObject particle = Instantiate(particles[particleType].particlePrefab, position, Quaternion.identity);
        BaseParticle baseParticle = particle.GetComponent<BaseParticle>();

        baseParticle.SetParticleLifeSpan(particles[particleType].particleLifeSpan);
        StartCoroutine(baseParticle.StartParticleLife());
    }

    public void SpawnWorldSpaceParticle(ParticleType particleType, Vector3 position)
    {
        GameObject particle = Instantiate(particles[particleType].particlePrefab, position, Quaternion.identity, worldSpaceCanvas.transform);
        BaseParticle baseParticle = particle.GetComponent<BaseParticle>();

        baseParticle.SetParticleLifeSpan(particles[particleType].particleLifeSpan);
        StartCoroutine(baseParticle.StartParticleLife());
    }
}

[Serializable]
public struct ParticleInfo
{
    public ParticleType particleType;
    public GameObject particlePrefab;
    public float particleLifeSpan;
}
