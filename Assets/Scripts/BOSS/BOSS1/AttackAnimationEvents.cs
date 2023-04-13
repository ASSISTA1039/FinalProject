using Core.Util;
using UnityEngine;

namespace Core.Combat
{
    public class AttackAnimationEvents : MonoBehaviour
    {
        public GameObject attackCollider;
        public GameObject attackCollider_jump;
        public ParticleSystem impactEffect;
        public Transform impactTransform;
        public float cameraShakeIntensity = 0.2f;
        
        private void OnAttackStart()
        {
            attackCollider.SetActive(true);
            //EffectManager.Instance.PlayOneShot(impactEffect, impactTransform.position);
            CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity,1f);
        }

        private void OnAttackEnd()
        {
            attackCollider.SetActive(false);
        }

        private void OnJumpAttackStart()
        {
            attackCollider_jump.SetActive(true);
        }

        private void onJumpAttackEnd()
        {
            attackCollider_jump.SetActive(false);
        }
    }
}