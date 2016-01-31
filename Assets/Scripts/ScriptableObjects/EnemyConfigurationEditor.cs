using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemyConfigurationEditor {

  [MenuItem("ScriptableObjects/EnemyConfiguration")]

  public static void CreateAsset()
  {
    ScriptableObjectUtility.CreateAsset<EnemyConfiguration>();
  }
}
