using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TMP_Text questText;
    [SerializeField] private string prefix = "Kiwi coins:";
    //[SerializeField] private GameManager manager;

    private void Awake()
    {
        if (questText==null && TryGetComponent(out questText))
        {
            enabled = false;
        }
    }

    private void Start()
    {
        if (GameManager.Instance != null) 
        {
            GameManager.Instance.OnGoldChanged += UpdateGoldText;
        }
        UpdateGoldText(0);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGoldChanged -= UpdateGoldText;
        }
    }

    private void UpdateGoldText(int newGoldCount)
    {
        questText.text = prefix + newGoldCount;
        if(newGoldCount == 5)
        {
            questText.color = Color.green;
        }
    }
}
