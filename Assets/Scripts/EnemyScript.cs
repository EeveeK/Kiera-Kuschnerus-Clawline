using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour, IDamageable,IHittable
{
    [SerializeField] float maxHealth = 50f;
    [SerializeField] float currentHealth;

    bool invulnerable = false;

    public void TakeDamage(float amount)
    {
        if (invulnerable)
        {
            return;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        } else
        {
            currentHealth -= amount;
            Debug.Log("Took Damage!");
            StartCoroutine(InvincibilityRoutine());
        }
        
        
    }

    private IEnumerator InvincibilityRoutine()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1);
        invulnerable = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool canItBeHit()
    {
        return true;
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
