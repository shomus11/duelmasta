using TMPro;
using UnityEngine;

public class UIIndicator : MonoBehaviour
{
    public TextMeshProUGUI attackIndicator;

    private void Start()
    {
        attackIndicator.text = "";
    }
    private void OnEnable()
    {
        EnemyAttack.OnAttackEvent += UpdateAttackIndicator;
    }

    private void OnDisable()
    {
        EnemyAttack.OnAttackEvent -= UpdateAttackIndicator;
    }

    private void UpdateAttackIndicator(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Normal:
                attackIndicator.text = "Normal Attack";
                attackIndicator.color = Color.white;
                break;
            case AttackType.Heavy:
                attackIndicator.text = "Heavy Attack";
                attackIndicator.color = Color.red;
                break;
            case AttackType.Parryable:
                attackIndicator.text = "Parryable Attack";
                attackIndicator.color = Color.yellow;
                break;
        }
    }
}
