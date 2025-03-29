using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDefense : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] private int parryStreak = 0;
    [SerializeField] private int hitCount = 0;
    [SerializeField] private float actionDuration = .5f;
    [SerializeField] PlayerState playerState = PlayerState.Idle;
    [SerializeField] TextMeshProUGUI defenceNotification;
    [SerializeField] Transform cameraShakeEffect;
    [SerializeField] Image damageUI;
    private void Start()
    {
        defenceNotification.text = "";
    }
    private void OnEnable()
    {
        EnemyAttack.OnAttackEvent += HandleEnemyAttack;
    }

    private void OnDisable()
    {
        EnemyAttack.OnAttackEvent -= HandleEnemyAttack;
    }

    private void HandleEnemyAttack(AttackType attackType)
    {
        string action = "";
        if (attackType == AttackType.Normal && playerState == PlayerState.Block)
        {
            damageUI.color = Color.yellow;
            damageUI.DOFade(.5f, .25f).From(0f).SetLoops(2, LoopType.Yoyo);

            SoundManager.Instance.PlaySE(PlayerState.Block.ToString());
            action = "Blocked Normal Attack!";
        }
        else if (attackType == AttackType.Heavy && playerState == PlayerState.Dodge || attackType == AttackType.Normal && playerState == PlayerState.Dodge)
        {
            damageUI.color = Color.green;
            damageUI.DOFade(.5f, .25f).From(0f).SetLoops(2, LoopType.Yoyo);
            SoundManager.Instance.PlaySE(PlayerState.Dodge.ToString());
            action = $"Dodge an {attackType} Attack";
        }
        else if (attackType == AttackType.Parryable && playerState == PlayerState.Parry) // Parry (LB)
        {
            cameraShakeEffect.transform.DOShakePosition(.25f, 1.0f, 10, 90.0f);
            SoundManager.Instance.PlaySE(PlayerState.Parry.ToString());
            action = "Parried!";
            parryStreak++;
            if (parryStreak >= 3)
            {
                GameplayManager.instance.Endgame(true);
            }
        }
        else
        {
            damageUI.color = Color.red;
            damageUI.DOFade(.5f, .25f).From(0f).SetLoops(2, LoopType.Yoyo);
            SoundManager.Instance.PlaySE(attackType.ToString());
            action = "Got Hit!";
            TakeHit();
        }
        defenceNotification.text = action;
    }

    public void ParryInput(InputAction.CallbackContext context)
    {
        if (GameplayManager.instance.state == GameState.playing)
            if (context.performed && playerState == PlayerState.Idle)
            {
                StartCoroutine(PlayerBattleAction(PlayerState.Parry));
            }
    }
    public void BlockInput(InputAction.CallbackContext context)
    {
        if (GameplayManager.instance.state == GameState.playing)
            if (context.started && playerState == PlayerState.Idle)
            {
                StartCoroutine(PlayerBattleAction(PlayerState.Block));
            }
    }
    public void DodgeInput(InputAction.CallbackContext context)
    {
        if (GameplayManager.instance.state == GameState.playing)
            if (context.performed && playerState == PlayerState.Idle)
            {
                StartCoroutine(PlayerBattleAction(PlayerState.Dodge));
            }
    }
    IEnumerator PlayerBattleAction(PlayerState stateTarget)
    {
        playerState = stateTarget;
        playerAnimator.Play(playerState.ToString());
        yield return new WaitForSeconds(actionDuration);
        playerState = PlayerState.Idle;
        playerAnimator.Play(playerState.ToString());
    }
    private void TakeHit()
    {
        hitCount++;
        if (hitCount >= 5)
        {
            GameplayManager.instance.Endgame(false);
        }
    }
}
