﻿using UnityEngine;

namespace Yuliang.UI.Dark
{
    public class TanChuangManager : MonoBehaviour
    {
        [Header("BRUSH ANIMATION")]
        public Animator brushAnimator;
        public bool enableSplash = true;
        private Animator mWindowAnimator;

        void Start()
        {
            mWindowAnimator = gameObject.GetComponent<Animator>();
        }

        public void ModalWindowIn()
        {
            mWindowAnimator.Play("Modal Window In");

            if (enableSplash == true)
                brushAnimator.Play("Transition Out");
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
}