using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectReceiver : MonoBehaviour
{
    public List<Effect> _effects = new List<Effect>();

    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
        effect.effectReceiver = this;
        effect.Init();
        StartCoroutine(effectRemoverTimer(effect.Duration, effect));
        StartCoroutine(EverySecondDamager(effect));
    }

    private void RemoveEffect(Effect effect)
    {
        effect.End();
        _effects.Remove(effect);
    }

    private void Update()
    {
        if (_effects.Count > 0)
        {
            foreach (Effect effect in _effects)
            {
                effect.Run();
            }
        }
    }

    private IEnumerator effectRemoverTimer(float duration, Effect effect)
    {
        yield return new WaitForSeconds(duration);
        RemoveEffect(effect);
    }

    private IEnumerator EverySecondDamager(Effect effect)
    {
        for(float a = 1; a <= effect.Duration; a++)
        {
            yield return new WaitForSeconds(1f);
            effect.EverySecondRun();
        }
        
    }
}
