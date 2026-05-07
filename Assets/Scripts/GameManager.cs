using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float maxHealth = 100f;

    private float m_CurrentHealth;
    private int coinCount;

   
    public event Action<float> OnHealthChanged; 
    public event Action<int> OnGoldChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        m_CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount) 
    {
        if (amount == 0) return;
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth - amount, 0f, maxHealth);
        OnHealthChanged?.Invoke(m_CurrentHealth / maxHealth);
    }

    public void Heal(float amount) 
    {
        if (amount == 0) return;
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth + amount, 0f, maxHealth);
        OnHealthChanged?.Invoke(m_CurrentHealth / maxHealth);
    }

    public void AddGold(int amount) 
    {
        if (amount == 0) return;
        coinCount += amount;
        OnGoldChanged?.Invoke(coinCount);
        Debug.Log($"Gold count: {coinCount}");
    }

    [ContextMenu("Test: Take 10 Damage")]
    public void TestTakeDamage() => TakeDamage(10f);

    [ContextMenu("Test: Heal 20")]
    public void TestHeal() => Heal(20f);

    [ContextMenu("Test: Add Gold")]
    public void TestAddGold() => AddGold(1);
}