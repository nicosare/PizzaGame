using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NoSeason
{
    [SerializeField] private float morningHour;
    [SerializeField] private float eveningHour;
    [SerializeField] private float nightHour;
    [SerializeField] private List<Phenomenon> atmosphericPhenomena;

    public float GetMorningHour() { return morningHour; }
    public float GetEveningHour() { return eveningHour; }
    public float GetNightHour() { return nightHour; }
    public List<Phenomenon> GetPhenomenons() { return atmosphericPhenomena; }
    public List<float> GetProbabilities()
    {

        List<float> probabilities = new List<float>();

        foreach (Phenomenon phenomenon in atmosphericPhenomena)
        {
            probabilities.Add(phenomenon.GetProbability());
        }

        return probabilities;
    }
    public List<ParticleSystem> GetParticleSystems()
    {
        List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        foreach (Phenomenon phenomenon in atmosphericPhenomena)
        {
            particleSystems.Add(phenomenon.GetParticleSystem());
        }

        return particleSystems;
    }

    public NoSeason(float morningHour, float eveningHour, float nightHour)
    {
        this.morningHour = morningHour;
        this.eveningHour = eveningHour;
        this.nightHour = nightHour;
    }
}