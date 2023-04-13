using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Combat;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using DG.Tweening;

namespace Core.AI
{
    public class FenJie_Bat : EnemyAction
    {
        public GameObject prefabBat;
        public Transform weaponTransform;
        public SharedInt health_Restore;
        public float spawnInterval = 0.3f;
        public override TaskStatus OnUpdate()
        {
            var sequence = DOTween.Sequence();
            for (int i = 0; i < health_Restore.Value; i++)
            {
                sequence.AppendCallback(BatProduce);
                sequence.AppendInterval(spawnInterval);
            }
            return TaskStatus.Success;
        }

        private void BatProduce()
        {
            var bat = PoolManager.Release(prefabBat, weaponTransform.position);
            float x = Random.Range(-3, 3);
            float y = Random.Range(1, 3);
            bat.GetComponent<Boss_BatController>().dir = new Vector2(x, y).normalized;
        }
    }
}