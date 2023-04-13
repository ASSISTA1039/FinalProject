using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] ballProjectilePools;
    [SerializeField] Pool[] enemyProjectilePools;

    static Dictionary<GameObject, Pool> dictionary;

    void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();

        Initialize(ballProjectilePools);
        Initialize(enemyProjectilePools);
    }

    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            dictionary.Add(pool.Prefab, pool);
            
            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }


    //Return a specified prefab gameObject in the pool
    //根据传入的prefab参数，返回对象池中预备好的游戏对象
    //Specified gameObject prefab
    //指定的游戏对象预制体
    //Prepared gameObject in the pool
    //对象池中预备好的游戏对象

    public static GameObject Release(GameObject prefab)
    {
        return dictionary[prefab].PreparedObject();
    }

    /// <summary>
    /// <para>Release a specified prepared gameObject in the pool at specified position.</para>
    /// <para>根据传入的prefab参数，在position参数位置释放对象池中预备好的游戏对象。</para> 
    /// </summary>
    /// <param name="prefab">
    /// <para>Specified gameObject prefab.</para>
    /// <para>指定的游戏对象预制体。</para>
    /// </param>
    /// <param name="position">
    /// <para>Specified release position.</para>
    /// <para>指定释放位置。</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
    #if UNITY_EDITOR
    if (!dictionary.ContainsKey(prefab))
    {
        Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);

        return null;
    }
    #endif
        return dictionary[prefab].PreparedObject(position);
    }

    /// <summary>
    /// <para>Release a specified prepared gameObject in the pool at specified position and rotation.</para>
    /// <para>根据传入的prefab参数和rotation参数，在position参数位置释放对象池中预备好的游戏对象。</para> 
    /// </summary>
    /// <param name="prefab">
    /// <para>Specified gameObject prefab.</para>
    /// <para>指定的游戏对象预制体。</para>
    /// </param>
    /// <param name="position">
    /// <para>Specified release position.</para>
    /// <para>指定释放位置。</para>
    /// </param>
    /// <param name="rotation">
    /// <para>Specified rotation.</para>
    /// <para>指定的旋转值。</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position, rotation);
    }

    /// <summary>
    /// <para>Release a specified prepared gameObject in the pool at specified position, rotation and scale.</para>
    /// <para>根据传入的prefab参数, rotation参数和localScale参数，在position参数位置释放对象池中预备好的游戏对象。</para> 
    /// </summary>
    /// <param name="prefab">
    /// <para>Specified gameObject prefab.</para>
    /// <para>指定的游戏对象预制体。</para>
    /// </param>
    /// <param name="position">
    /// <para>Specified release position.</para>
    /// <para>指定释放位置。</para>
    /// </param>
    /// <param name="rotation">
    /// <para>Specified rotation.</para>
    /// <para>指定的旋转值。</para>
    /// </param>
    /// <param name="localScale">
    /// <para>Specified scale.</para>
    /// <para>指定的缩放值。</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position, rotation, localScale);
    }
}