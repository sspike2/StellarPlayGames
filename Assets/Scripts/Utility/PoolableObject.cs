using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoolableObject
{
    public void OnGameObjectSpawn();
    public void OnGameObjectReturn();
}
