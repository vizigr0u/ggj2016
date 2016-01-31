using UnityEngine;
using System.Collections;
using UnityEditor;

public class WeaponConfigurationEditor {

  [MenuItem("ScriptableObjects/WeaponConfiguration")]

  public static void CreateAsset()
  {
    ScriptableObjectUtility.CreateAsset<WeaponConfiguration>();
  }
}
