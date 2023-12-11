using System;
using UnityEngine;

[Serializable]
public class Phenomenon
{

    [SerializeField] private ParticleSystem particleSystem;

    [Range(0, 1)]
    [SerializeField] private float probability;

    [SerializeField] private Color color;

    public float GetProbability() { return probability; }
    public ParticleSystem GetParticleSystem() { return particleSystem; }
    public Color GetColor() { return color; }

    public Phenomenon(ParticleSystem phenomenonParticleSystem, float phenomenonProbability, Color phenomenonColor)
    {
        this.particleSystem = phenomenonParticleSystem;
        this.probability = phenomenonProbability;
        this.color = phenomenonColor;
    }
}
