using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    static public ArmorManager inInstance { get; private set; }

    [SerializeField] private float flatArmor = 0f;
    [SerializeField] private float percentArmorReduction = 0f;
    [SerializeField] private float minDamageCap = 1f;
    [SerializeField] private float blockChance = 0;
    [SerializeField] private float comeBackDamagePercent = 0f;

    private void Awake()
    {
        if (inInstance == null)
        {
            inInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public float CalculateIncomingDamage(float rawDamage, GameObject attacker = null)
    {
        if (blockChance > 0 && Random.value < blockChance)
        {
            return 0f;
        }

        float reducedDamage = rawDamage - flatArmor;
        reducedDamage *= (1f - percentArmorReduction);

        reducedDamage = Mathf.Max(reducedDamage, minDamageCap);

        if (comeBackDamagePercent > 0 && attacker != null)
        {
            ReflectDamageToAttacker(rawDamage * comeBackDamagePercent, attacker);
        }
        return reducedDamage;
    }
    void ReflectDamageToAttacker(float reflectDamage, GameObject attacker)
    {
        if (attacker.TryGetComponent<EnemyHealthManager>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(reflectDamage);
        }
    }
    public void AddFlatArmor(float amount)
    {
        flatArmor += amount;
    }
    public void AddPercentArmor(float percent)
    {
        percentArmorReduction = Mathf.Clamp(percentArmorReduction + percent , 0f, 0.8f);
    }
    public void AddBlockChance(float amount)
    {
        blockChance = Mathf.Clamp(blockChance + amount, 0f, 0.75f);
    }
}
