using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticle;

    private ParticleManager ParticleManager { get => particleManager ?? core.GetCoreComponent(ref particleManager); }
    private ParticleManager particleManager;

    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;

    public override void Init(Core core)
    {
        base.Init(core);

        //Subscribing to event in the Stats, when the Health reaches Zero, the Die function will be called.
        Stats.HealthZero += Die;
    }

    //Function which will disable the object the Death Core Component attached to.
    public void Die()
    {
        //instantiate some particles when object dies
        foreach (var particle in deathParticle)
        {
            ParticleManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }

    // In order to avoid errors, we should make sure that we subscribe to the event only when Death script is Enabled
    private void OnEnable()
    {
        Stats.HealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.HealthZero -= Die;
    }
}
