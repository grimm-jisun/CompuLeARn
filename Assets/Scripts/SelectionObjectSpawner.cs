using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
public class ButtonSpawnHandler : MonoBehaviour 
{ 
    [System.Serializable] 
    public class ButtonPrefabPair 
    { 
        public Button button; 
        public GameObject prefab; 
    } 
    public List<ButtonPrefabPair> buttonPrefabPairs; 
    public Transform spawnPoint; 
    private ARRaycastManager raycastManager; 
    void Start() 
    { 
        raycastManager = FindObjectOfType<ARRaycastManager>(); 
        if (raycastManager == null) 
        { 
            /*Debug.LogError("ARRaycastManager not found in the scene."); */
        } 
        else 
        { 
            /*Debug.Log("ARRaycastManager initialized."); */
        } 
        foreach (var pair in buttonPrefabPairs) 
        { 
            if (pair.button != null && pair.prefab != null) 
            { 
                pair.button.onClick.AddListener(() => SpawnPrefab(pair.prefab)); 
            } 
            else 
            { 
                /*Debug.LogError("Button or Prefab reference is missing in the pair."); */
            } 
        } 
    } 
    void SpawnPrefab(GameObject prefab) 
    { 
        if (spawnPoint != null) 
        { 
            GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            /*Debug.Log("Prefab spawned: " + prefab.name + " at " + spawnPoint.position);*/
        }
        else 
        { 
           /*Debug.LogError("Spawn Point reference is missing."); */
        } 
    } 
}