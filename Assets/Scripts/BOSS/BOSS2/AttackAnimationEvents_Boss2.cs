using Core.Util;
using System.Collections;
using UnityEngine;

namespace Core.Combat2
{
    public class AttackAnimationEvents_Boss2 : MonoBehaviour
    {
        public GameObject attack_Collider;
        public GameObject attack_Collider1;
        public GameObject attack_Collider2;
        public GameObject attack1_Collider;
        public GameObject attackCollider_jump;
        public GameObject attackCollider_jump1;
        public GameObject final_transform_Attack;
        public GameObject final_transform_Attack1;
        private GameObject player;
        private Rigidbody2D rb;
        //public ParticleSystem impactEffect;
        //public Transform impactTransform;
        public float cameraShakeIntensity = 5f;
        private Transform selftransform;

        private void Start()
        {
            selftransform = gameObject.GetComponent<Rigidbody2D>().transform;
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player");
        }


        private void OnAttack_StartLeft()
        {
            StartCoroutine(ChangeTransform_Left());
            //attack_Collider.SetActive(true);
            //EffectManager.Instance.PlayOneShot(impactEffect, impactTransform.position);
            //CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity,1f);
        }
        IEnumerator ChangeTransform_Left()
        {
            yield return new WaitForSeconds(0);
            if(transform.localScale.x == 1)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 5f, transform.position.y), 5f);
            else if(transform.localScale.x == -1)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 5f, transform.position.y), 5f);

        }

        private void OnAttack_StartMiddle()
        {
            attack_Collider1.SetActive(true);
            StartCoroutine(ChangeTransform_Middle());
            //EffectManager.Instance.PlayOneShot(impactEffect, impactTransform.position);
            //CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity,1f);
        }
        private void OnAttack_EndMiddle()
        {
            attack_Collider1.SetActive(false);
        }
        IEnumerator ChangeTransform_Middle()
        {
            yield return new WaitForSeconds(0);
            if (transform.localScale.x == 1)
                selftransform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 10f, transform.position.y), 10f);
            else if (transform.localScale.x == -1)
                selftransform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 10f, transform.position.y), 10f);

        }
        
        private void OnAttack_StartRight()
        {
            attack_Collider2.SetActive(true);
            StartCoroutine(ChangeTransform_Right());
            //EffectManager.Instance.PlayOneShot(impactEffect, impactTransform.position);
            //CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity,1f);
        }
        private void OnAttack_EndRight()
        {
            attack_Collider2.SetActive(false);
        }
        IEnumerator ChangeTransform_Right()
        {
            yield return new WaitForSeconds(0);
            if (transform.localScale.x == 1)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 1.5f, transform.position.y), 1.5f);
            else if (transform.localScale.x == -1)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 1.5f, transform.position.y), 1.5f);

        }


        private void OnAttack1_StartMiddle()
        {
            StartCoroutine(ChangeTransform1_Middle());
            attack1_Collider.SetActive(true);
        }
        private void OnAttack1_EndMiddle()
        {
            attack1_Collider.SetActive(false);
        }
        IEnumerator ChangeTransform1_Middle()
        {
            yield return new WaitForSeconds(0);
            if (transform.localScale.x == 1)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 10f, transform.position.y), 10f);
            else if (transform.localScale.x == -1)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 10f, transform.position.y), 10f);

        }


        private void OnAttack1_StartRight()
        {
            StartCoroutine(ChangeTransform1_Right());
            attack_Collider2.SetActive(true);
        }
        private void OnAttack1_EndRight()
        {
            attack_Collider2.SetActive(false);
        }
        IEnumerator ChangeTransform1_Right()
        {
            yield return new WaitForSeconds(0);
            if (transform.localScale.x == 1)
                selftransform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 1.5f, transform.position.y), 1.5f);
            else if (transform.localScale.x == -1)
                selftransform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 1.5f, transform.position.y), 1.5f);

        }


        private void Run_Start()
        {
            rb.velocity = transform.localScale.x* 5 * Vector2.right;
        }
        private void Run_End()
        {
            rb.velocity = Vector2.zero;
        }

        /// <summary>
        /// 以下待修改
        /// </summary>
        private void OnAttack_EndTransform()
        {

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //selftransform.position = final_transform_Attack.transform.position;
        }
        

        private void OnAttack1_EndTransform()
        {
            //selftransform.position = final_transform_Attack1.transform.position;
        }
        private void OnJumpAttackStart()
        {
            attackCollider_jump.SetActive(true);
        }
        
        private void OnJumpAttackMiddle()
        {
            attackCollider_jump.SetActive(false);
            //attackCollider_jump1.SetActive(true);
        }
/*        private void OnJumpAttackMiddle_End()
        {
            attackCollider_jump.SetActive(false);
        }*/
        private void OnJumpAttackEnd()
        {
            //attackCollider_jump1.SetActive(false);
            attackCollider_jump.SetActive(true);
            CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity, 1f);
            //selftransform.position = final_transform_Attack.transform.position;
        }
        private void OnJumpAttackEndCollider()
        {
            attackCollider_jump.SetActive(false);
        }
        private void SearchPlayer()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            float distance = final_transform_Attack.transform.position.x - player.transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(player.transform.position.x,transform.position.y),Mathf.Abs(distance));
/*            if ( distance >= 0)
            {
                selftransform.position = 
                    new Vector3(selftransform.position.x - distance,
                    selftransform.position.y,
                    selftransform.position.z);
            }
            else if( distance < 0)
            {
                selftransform.position =
                    new Vector3(selftransform.position.x + distance,
                    selftransform.position.y,
                    selftransform.position.z);
            }*/
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}