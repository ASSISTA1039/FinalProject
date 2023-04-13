using UnityEngine;

public class ModalWindowManager : MonoBehaviour
{
    [Header("BRUSH ANIMATION")]
    public Animator brushAnimator;
    public bool enableSplash = true;
    public int chapter;
    private Animator mWindowAnimator;

    void Start()
    {
        mWindowAnimator = gameObject.GetComponent<Animator>();
        chapter = -1;
    }

    public void ModalWindowIn(int chapter)
    {
        //判断是否选择特定章节/读档
        this.chapter = chapter;
        if (this.chapter == -1)
        {
            mWindowAnimator.Play("Modal Window In");

            if (enableSplash == true)
            {
                brushAnimator.Play("Transition Out");
            }
        }
    }

    public void ModalWindowOut()
    {
        mWindowAnimator.Play("Modal Window Out");

        if (enableSplash == true)
        {
            brushAnimator.Play("Transition In");
        }
    }
}
