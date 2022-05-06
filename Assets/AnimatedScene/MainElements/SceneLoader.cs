using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FM {
    [System.Serializable] public enum AnimationType { Slide, Fade, Circle }

    [System.Serializable] public enum SlideType { Single, Double }

    [System.Serializable] public enum SlideDirection { LeftToRight, RightToLeft, DownToUp, UpToDown }

    [System.Serializable] public enum CircleSlide { Static, LeftToRight, RightToLeft, DownToUp, UpToDown }
    public class SceneLoader : MonoBehaviour {

        static SceneLoader instance;

        [Header("Entry")]
        [SerializeField] public float durationEntry;
        [SerializeField] public AnimationType animationTypeEntry;
        [Header("Slide")]
        [SerializeField] public SlideType slideTypeEntry;
        [SerializeField] public SlideDirection slideDirectionEntry;
        [Header("Circle")]
        [SerializeField] public CircleSlide circleSlideEntry;
        [SerializeField] public float circleSizeEntry;
        [SerializeField] public Ease circleExitEase;
        [SerializeField] public float circleExitDuration;
        [Header("Common")]
        [SerializeField] public Color backPanelColorEntry;
        [SerializeField] public Color frontPanelColorEntry;
        [SerializeField] public Ease easeEntry;

        [Header("Exit")]
        [SerializeField] public float durationExit;
        [SerializeField] public AnimationType animationTypeExit;
        [Header("Slide")]
        [SerializeField] public SlideType slideTypeExit;
        [SerializeField] public SlideDirection slideDirectionExit;
        [Header("Circle")]
        [SerializeField] public CircleSlide circleSlideExit;
        [SerializeField] public float circleSizeExit;
        [SerializeField] public Ease circleEntryEase;
        [SerializeField] public float circleEntryDuration;
        [Header("Common")]
        [SerializeField] public Color backPanelColorExit;
        [SerializeField] public Color frontPanelColorExit;
        [SerializeField] public Ease easeExit;
        [SerializeField] public float holdDuration;

        private Transform canvas;
        private float screenHeight;
        private float screenWidth;
        private float circleSize;

        private AsyncOperation asyncLoad;
        private bool animDone;

        private bool loadingScene;

        System.Action OnCompleteAnim;

        void Awake() {
            instance = this;
        }

        void Start() {
            if (GameObject.FindObjectOfType<Canvas>() == null) {
                canvas = Instantiate(Resources.Load("Canvas", typeof(GameObject))as GameObject).transform;
            } else {
                canvas = GameObject.FindObjectOfType<Canvas>().transform;
            }
            screenWidth = canvas.GetComponent<RectTransform>().sizeDelta.x;
            screenHeight = canvas.GetComponent<RectTransform>().sizeDelta.y;

            circleSize = screenHeight > screenWidth ? screenHeight * 1.5f : screenWidth * 1.5f;

            GameObject go1 = new GameObject();
            GameObject go2 = new GameObject();

            DG.Tweening.TweenCallback Complete = () => {
                if (OnCompleteAnim != null) {
                    OnCompleteAnim();
                }
                Destroy(go1);
                Destroy(go2);
            };

            go1.transform.SetParent(canvas);
            go1.name = "LoadingPanel01";
            go1.AddComponent<Image>();

            RectTransform rect1 = go1.GetComponent<RectTransform>();
            rect1.pivot = new Vector2(0.5f, 0.5f);

            rect1.sizeDelta = new Vector2(screenWidth, screenHeight);
            rect1.localScale = new Vector2(1, 1);

            rect1.anchoredPosition = Vector2.zero;

            rect1.GetComponent<Image>().color = backPanelColorEntry;
            rect1.GetComponent<Image>().raycastTarget = true;

            if (animationTypeEntry == AnimationType.Slide) {
                if (slideTypeEntry == SlideType.Single) {
                    rect1.pivot = new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? 1 : slideDirectionEntry == SlideDirection.RightToLeft ? 0 : 0.5f, slideDirectionEntry == SlideDirection.UpToDown ? 0 : slideDirectionEntry == SlideDirection.DownToUp ? 1 : 0.5f);
                    rect1.anchoredPosition = new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? screenWidth / 2f : slideDirectionEntry == SlideDirection.RightToLeft ? -screenWidth / 2f : 0, slideDirectionEntry == SlideDirection.UpToDown ? -screenHeight / 2f : slideDirectionEntry == SlideDirection.DownToUp ? screenHeight / 2f : 0);
                    rect1.DOSizeDelta(new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? 0 : slideDirectionEntry == SlideDirection.RightToLeft ? 0 : screenWidth, slideDirectionEntry == SlideDirection.LeftToRight ? screenHeight : slideDirectionEntry == SlideDirection.RightToLeft ? screenHeight : 0), durationEntry).SetEase(easeEntry).OnComplete(Complete);
                } else if (slideTypeEntry == SlideType.Double) {
                    go2.transform.SetParent(canvas);
                    go2.name = "LoadingPanel02";
                    go2.AddComponent<Image>();

                    RectTransform rect2 = go2.GetComponent<RectTransform>();
                    rect2.pivot = new Vector2(0.5f, 0.5f);

                    rect2.sizeDelta = new Vector2(screenWidth, screenHeight);
                    rect2.localScale = new Vector2(1, 1);

                    rect2.anchoredPosition = Vector2.zero;

                    rect2.GetComponent<Image>().color = frontPanelColorEntry;
                    rect2.GetComponent<Image>().raycastTarget = true;

                    rect2.pivot = new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? 1 : slideDirectionEntry == SlideDirection.RightToLeft ? 0 : 0.5f, slideDirectionEntry == SlideDirection.UpToDown ? 0 : slideDirectionEntry == SlideDirection.DownToUp ? 1 : 0.5f);
                    rect2.anchoredPosition = new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? screenWidth / 2f : slideDirectionEntry == SlideDirection.RightToLeft ? -screenWidth / 2f : 0, slideDirectionEntry == SlideDirection.UpToDown ? -screenHeight / 2f : slideDirectionEntry == SlideDirection.DownToUp ? screenHeight / 2f : 0);
                    rect2.DOSizeDelta(new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? 0 : slideDirectionEntry == SlideDirection.RightToLeft ? 0 : screenWidth, slideDirectionEntry == SlideDirection.LeftToRight ? screenHeight : slideDirectionEntry == SlideDirection.RightToLeft ? screenHeight : 0), durationEntry).SetEase(easeEntry);

                    rect1.pivot = new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? 1 : slideDirectionEntry == SlideDirection.RightToLeft ? 0 : 0.5f, slideDirectionEntry == SlideDirection.UpToDown ? 0 : slideDirectionEntry == SlideDirection.DownToUp ? 1 : 0.5f);
                    rect1.anchoredPosition = new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? screenWidth / 2f : slideDirectionEntry == SlideDirection.RightToLeft ? -screenWidth / 2f : 0, slideDirectionEntry == SlideDirection.UpToDown ? -screenHeight / 2f : slideDirectionEntry == SlideDirection.DownToUp ? screenHeight / 2f : 0);
                    StartCoroutine(Delay(0.15f, () => {
                        rect1.DOSizeDelta(new Vector2(slideDirectionEntry == SlideDirection.LeftToRight ? 0 : slideDirectionEntry == SlideDirection.RightToLeft ? 0 : screenWidth, slideDirectionEntry == SlideDirection.LeftToRight ? screenHeight : slideDirectionEntry == SlideDirection.RightToLeft ? screenHeight : 0), durationEntry).SetEase(easeEntry).OnComplete(Complete);
                    }));
                }
            } else if (animationTypeEntry == AnimationType.Fade) {
                rect1.GetComponent<Image>().DOFade(1, 0);
                rect1.GetComponent<Image>().DOFade(0, durationEntry).SetEase(easeEntry).OnComplete(Complete);
            } else if (animationTypeEntry == AnimationType.Circle) {
                rect1.sizeDelta = new Vector2(circleSize, circleSize);
                rect1.anchoredPosition = Vector2.zero;

                rect1.GetComponent<Image>().sprite = Resources.Load("Circle", typeof(Sprite))as Sprite;
                rect1.GetComponent<Image>().color = backPanelColorEntry;

                rect1.DOSizeDelta(new Vector2(circleSlideEntry == CircleSlide.Static ? 0 : circleSizeEntry, circleSlideEntry == CircleSlide.Static ? 0 : circleSizeEntry), durationEntry).SetEase(easeEntry).OnComplete(() => {
                    Vector2 endPosition = circleSlideEntry == CircleSlide.Static ? Vector2.zero : (circleSlideEntry == CircleSlide.LeftToRight ? new Vector2((screenWidth / 2f) + (circleSizeEntry / 2f), 0) : (circleSlideEntry == CircleSlide.RightToLeft ? new Vector2((-screenWidth / 2f) - (circleSizeEntry / 2f), 0) : (circleSlideEntry == CircleSlide.UpToDown ? new Vector2(0, (-screenHeight / 2f) - (circleSizeEntry / 2f)) : (new Vector2(0, (screenHeight / 2f) + (circleSizeEntry / 2f))))));
                    rect1.DOAnchorPos(endPosition, circleSlideEntry == CircleSlide.Static ? 0 : circleExitDuration).SetEase(circleExitEase).OnComplete(Complete);
                });
            }
        }

        void Animate(System.Action _OnComplete) {
            GameObject go1 = new GameObject();
            GameObject go2 = new GameObject();

            DG.Tweening.TweenCallback Complete = () => {
                _OnComplete();
            };

            go1.transform.SetParent(canvas);
            go1.name = "LoadingPanel01";
            go1.AddComponent<Image>();

            RectTransform rect1 = go1.GetComponent<RectTransform>();
            rect1.pivot = new Vector2(0.5f, 0.5f);

            rect1.sizeDelta = new Vector2(screenWidth, screenHeight);
            rect1.localScale = new Vector2(1, 1);

            rect1.anchoredPosition = Vector2.zero;

            rect1.GetComponent<Image>().color = backPanelColorExit;
            rect1.GetComponent<Image>().raycastTarget = true;

            if (animationTypeExit == AnimationType.Slide) {
                if (slideTypeExit == SlideType.Single) {
                    rect1.pivot = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? 0 : slideDirectionExit == SlideDirection.RightToLeft ? 1 : 0.5f, slideDirectionExit == SlideDirection.UpToDown ? 1 : slideDirectionExit == SlideDirection.DownToUp ? 0 : 0.5f);
                    rect1.anchoredPosition = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? -screenWidth / 2f : slideDirectionExit == SlideDirection.RightToLeft ? screenWidth / 2f : 0, slideDirectionExit == SlideDirection.UpToDown ? screenHeight / 2f : slideDirectionExit == SlideDirection.DownToUp ? -screenHeight / 2f : 0);
                    rect1.sizeDelta = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? 0 : slideDirectionExit == SlideDirection.RightToLeft ? 0 : screenWidth, slideDirectionExit == SlideDirection.LeftToRight ? screenHeight : slideDirectionExit == SlideDirection.RightToLeft ? screenHeight : 0);
                    rect1.DOSizeDelta(new Vector2(screenWidth, screenHeight), durationExit).SetEase(easeExit).OnComplete(Complete);
                } else if (slideTypeExit == SlideType.Double) {
                    go2.transform.SetParent(canvas);
                    go2.name = "LoadingPanel02";
                    go2.AddComponent<Image>();

                    RectTransform rect2 = go2.GetComponent<RectTransform>();
                    rect2.pivot = new Vector2(0.5f, 0.5f);

                    rect2.sizeDelta = new Vector2(screenWidth, screenHeight);
                    rect2.localScale = new Vector2(1, 1);

                    rect2.anchoredPosition = Vector2.zero;

                    rect2.GetComponent<Image>().color = frontPanelColorExit;
                    rect2.GetComponent<Image>().raycastTarget = true;

                    rect1.pivot = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? 0 : slideDirectionExit == SlideDirection.RightToLeft ? 1 : 0.5f, slideDirectionExit == SlideDirection.UpToDown ? 1 : slideDirectionExit == SlideDirection.DownToUp ? 0 : 0.5f);
                    rect1.anchoredPosition = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? -screenWidth / 2f : slideDirectionExit == SlideDirection.RightToLeft ? screenWidth / 2f : 0, slideDirectionExit == SlideDirection.UpToDown ? screenHeight / 2f : slideDirectionExit == SlideDirection.DownToUp ? -screenHeight / 2f : 0);
                    rect1.sizeDelta = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? 0 : slideDirectionExit == SlideDirection.RightToLeft ? 0 : screenWidth, slideDirectionExit == SlideDirection.LeftToRight ? screenHeight : slideDirectionExit == SlideDirection.RightToLeft ? screenHeight : 0);
                    rect1.DOSizeDelta(new Vector2(screenWidth, screenHeight), durationExit).SetEase(easeExit).OnComplete(Complete);

                    rect2.pivot = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? 0 : slideDirectionExit == SlideDirection.RightToLeft ? 1 : 0.5f, slideDirectionExit == SlideDirection.UpToDown ? 1 : slideDirectionExit == SlideDirection.DownToUp ? 0 : 0.5f);
                    rect2.anchoredPosition = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? -screenWidth / 2f : slideDirectionExit == SlideDirection.RightToLeft ? screenWidth / 2f : 0, slideDirectionExit == SlideDirection.UpToDown ? screenHeight / 2f : slideDirectionExit == SlideDirection.DownToUp ? -screenHeight / 2f : 0);
                    rect2.sizeDelta = new Vector2(slideDirectionExit == SlideDirection.LeftToRight ? 0 : slideDirectionExit == SlideDirection.RightToLeft ? 0 : screenWidth, slideDirectionExit == SlideDirection.LeftToRight ? screenHeight : slideDirectionExit == SlideDirection.RightToLeft ? screenHeight : 0);
                    StartCoroutine(Delay(0.15f, () => {
                        rect2.DOSizeDelta(new Vector2(screenWidth, screenHeight), durationExit).SetEase(easeExit).OnComplete(Complete);
                    }));
                }
            } else if (animationTypeExit == AnimationType.Fade) {
                rect1.GetComponent<Image>().DOFade(0, 0);
                rect1.GetComponent<Image>().DOFade(1, durationExit).SetEase(easeExit).OnComplete(Complete);
            } else if (animationTypeExit == AnimationType.Circle) {
                rect1.sizeDelta = circleSlideExit == CircleSlide.Static ? Vector2.zero : new Vector2(circleSizeExit, circleSizeExit);
                rect1.anchoredPosition = circleSlideExit == CircleSlide.Static ? Vector2.zero : circleSlideExit == CircleSlide.LeftToRight ? new Vector2(-(screenWidth / 2f) - (circleSizeExit / 2f), 0) : circleSlideExit == CircleSlide.RightToLeft ? new Vector2((screenWidth / 2f) + (circleSizeExit / 2f), 0) : circleSlideExit == CircleSlide.UpToDown ? new Vector2(0, ((screenHeight / 2f) + (circleSizeExit / 2f))) : new Vector2(0, (-(screenHeight / 2f) - (circleSizeExit / 2f)));

                rect1.GetComponent<Image>().sprite = Resources.Load("Circle", typeof(Sprite))as Sprite;
                rect1.GetComponent<Image>().color = backPanelColorExit;

                rect1.DOAnchorPos(Vector2.zero, circleEntryDuration).SetEase(circleEntryEase).OnComplete(() => {
                    rect1.DOSizeDelta(new Vector2(circleSize, circleSize), durationExit).SetEase(easeExit).OnComplete(Complete);
                });
            }
        }

        [SerializeField] public static void LoadScene(int sceneIndex) {
            instance.ChangeScene("", sceneIndex);
        }

        [SerializeField] public static void LoadScene(string sceneName) {
            instance.ChangeScene(sceneName);
        }

        [SerializeField] public static AsyncOperation LoadSceneAsync(int sceneIndex) {
            return instance.ChangeSceneAsync("", sceneIndex);
        }

        [SerializeField] public static AsyncOperation LoadSceneAsync(string sceneName) {
            return instance.ChangeSceneAsync(sceneName);
        }

        public static void AddOnAnimDoneListener(System.Action _OnAnimComplete){
            instance.OnCompleteAnim += _OnAnimComplete;
        }

        void ChangeScene(string sceneName = "", int sceneIndex = -1) {
            if (loadingScene) {
                return;
            }
            loadingScene = true;
            Animate(() => {
                StartCoroutine(Delay(holdDuration, () => {
                    if (sceneName != "") {
                        SceneManager.LoadScene(sceneName);
                    } else if (sceneIndex != -1) {
                        SceneManager.LoadScene(sceneIndex);
                    }
                }));
            });
        }

        AsyncOperation ChangeSceneAsync(string sceneName = "", int sceneIndex = -1) {
            StartCoroutine(SceneAsync(sceneName, sceneIndex));
            Animate(() => {
                StartCoroutine(Delay(holdDuration, () => {
                    animDone = true;
                    if (asyncLoad != null && asyncLoad.progress >= 0.9f) {
                        asyncLoad.allowSceneActivation = true;
                    }
                }));
            });
            return asyncLoad;
        }

        IEnumerator SceneAsync(string sceneName = "", int sceneIndex = -1) {
            if (sceneName != "") {
                asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            } else if (sceneIndex != -1) {
                asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            }
            if (asyncLoad == null) {
                yield return null;
            }
            asyncLoad.allowSceneActivation = false;
            yield return asyncLoad;
        }

        void Update() {
            if (asyncLoad != null && animDone && asyncLoad.progress >= 0.9f) {
                asyncLoad.allowSceneActivation = true;
            }
        }

        [SerializeField] public static Scene GetActiveScene() {
            return SceneManager.GetActiveScene();
        }

        IEnumerator Delay(float delay, System.Action OnComplete) {
            yield return new WaitForSeconds(delay);
            if (OnComplete != null)OnComplete();
        }

    }
}