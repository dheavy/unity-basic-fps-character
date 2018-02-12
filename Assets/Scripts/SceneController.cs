using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

  // Serialized variable for linking to the prefab object.
  [SerializeField] private GameObject enemyPrefab;

  private GameObject _enemy;

  void Update() {
    if (_enemy == null) {
      float angle = Random.Range(0, 360);
      _enemy = Instantiate(enemyPrefab) as GameObject;
      _enemy.transform.position = new Vector3(0, 1, 0);
      _enemy.transform.Rotate(0, angle, 0);
    }
  }

}
