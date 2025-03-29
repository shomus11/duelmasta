using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;

    float baseAnimationDuration = 0.25f;
    public RectTransform endGamePanel;
    public RectTransform endGamePanelBG;
    public TextMeshProUGUI tittleText;
    public RectTransform menuButtonSelection;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endGamePanel.gameObject.SetActive(false);
    }
    private void Update()
    {

    }

    public void ShowEndPanel(bool win = false)
    {
        if (SoundManager.Instance != null)
            if (win)
                SoundManager.Instance.PlaySE("Win");
            else
                SoundManager.Instance.PlaySE("Lose");

        float totalAnimationDuration = 0;
        Sequence sequence = DOTween.Sequence();

        if (win)
            tittleText.text = "Victory!";
        else
            tittleText.text = "Defeated!";

        endGamePanel.gameObject.SetActive(true);
        sequence.Insert(totalAnimationDuration, endGamePanelBG.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack))
            .OnComplete(() =>
            {
                tittleText.gameObject.SetActive(true);
            });
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, tittleText.rectTransform.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack))
           .OnComplete(() =>
           {
               menuButtonSelection.gameObject.SetActive(true);
           });
        totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, menuButtonSelection.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));

    }
}
