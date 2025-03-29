
namespace UI.Utiliy
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class UITransition : MonoBehaviour
    {
        public static UITransition instance;

        float baseAnimationDuration = 0.25f;
        [SerializeField] string soundName = "";
        public RectTransform transitionImage;
        public RectTransform loadingImage;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

            SoundManager.Instance.PlayBGM(soundName);
            transitionImage.gameObject.SetActive(true);
            loadingImage.gameObject.SetActive(true);
            FadeIn(1.5f);
        }
        void QuitGame()
        {
            FadeOut(true);
        }


        public void FadeIn(float wait = 0)
        {
            float totalAnimationDuration = 0;
            Sequence sequence = DOTween.Sequence();
            totalAnimationDuration += wait;
            sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(0, baseAnimationDuration).From(1).SetEase(Ease.OutBack));
            totalAnimationDuration += baseAnimationDuration;
            sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.zero, baseAnimationDuration).From(1).SetEase(Ease.OutBack));
        }

        public void FadeOutFadeIn()
        {
            float totalAnimationDuration = 0;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
            totalAnimationDuration += baseAnimationDuration;
            sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack));
            totalAnimationDuration += baseAnimationDuration;
            totalAnimationDuration += baseAnimationDuration;
            sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(0, baseAnimationDuration).From(1).SetEase(Ease.OutBack));
            totalAnimationDuration += baseAnimationDuration;
            sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.zero, baseAnimationDuration).From(Vector3.one).SetEase(Ease.OutBack));
        }
        public void FadeOut()
        {
            float totalAnimationDuration = 0;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
            totalAnimationDuration += baseAnimationDuration;
            sequence.Insert(totalAnimationDuration, loadingImage.DOScale(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack));
        }

        public void FadeOut(string sceneName)
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySE("Click");

            Debug.Log(sceneName);
            float totalAnimationDuration = 0;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
            totalAnimationDuration += baseAnimationDuration;
            sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack)).OnComplete(() =>
            {
                SceneManager.LoadScene(sceneName);
            });
        }
        public void FadeOut(bool quit)
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySE("Click");
            float totalAnimationDuration = 0;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(totalAnimationDuration, transitionImage.DOScale(Vector3.one, baseAnimationDuration).From(Vector3.zero).SetEase(Ease.OutBack));
            totalAnimationDuration += baseAnimationDuration;
            sequence.Insert(totalAnimationDuration, loadingImage.DOScaleY(1, baseAnimationDuration).From(0).SetEase(Ease.OutBack)).OnComplete(() =>
            {
                Application.Quit();
            });

        }

        public float GetBaseAnimationDuration()
        {
            return baseAnimationDuration;
        }
    }

}
