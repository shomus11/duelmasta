using System.Collections;
using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    private bool attacking = false;
    private float attackTimer;
    [SerializeField] float attackDelay = .5f;
    [SerializeField] float attackDuration = .5f;
    [SerializeField] AttackType currentAttack;
    [SerializeField] float attackInterval = 2f;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] SpriteRenderer enemySpriteRenderer;

    [Header("Attack Indicator")]
    [SerializeField] Color normalAttackColor;
    [SerializeField] Color heavyAttackColor;
    [SerializeField] Color parryableAttackColor;

    public delegate void OnAttack(AttackType attackType);
    public static event OnAttack OnAttackEvent;
    private void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (GameplayManager.instance.state == GameState.playing)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                if (!attacking)
                    ExecuteAttack();
            }
        }
    }

    private void ExecuteAttack()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        attacking = true;
        currentAttack = (AttackType)Random.Range(0, 3);
        SetAttackIndicatorColor();
        yield return new WaitForSeconds(attackDelay);
        enemySpriteRenderer.color = Color.white;
        OnAttackEvent?.Invoke(currentAttack);
        enemyAnimator.Play("Attack");
        yield return new WaitForSeconds(attackDuration);
        enemyAnimator.Play("Idle");
        attackTimer = 0f;
        attacking = false;
    }
    public void SetAttackIndicatorColor()
    {
        switch (currentAttack)
        {
            case AttackType.Normal:
                enemySpriteRenderer.color = normalAttackColor;
                break;
            case AttackType.Heavy:
                enemySpriteRenderer.color = heavyAttackColor;
                break;
            case AttackType.Parryable:
                enemySpriteRenderer.color = parryableAttackColor;
                break;
        }
    }
}