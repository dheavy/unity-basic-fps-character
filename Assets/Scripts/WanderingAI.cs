using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

  public float speed = 3.0f;
  public float obstacleRange = 5.0f;

  private bool _alive;

  [SerializeField] private GameObject fireballPrefab;
  private GameObject _fireball;

  public void SetAlive(bool alive) {
    _alive = alive;
  }

  void Start() {
    _alive = true;
  }

  void Update() {
    if (_alive) {
      transform.Translate(0, 0, speed * Time.deltaTime);

      // Raycast a sphere to "look" for obstacles in front.
      // If any, turn towards a semirandom new direction.
      Ray ray = new Ray(transform.position, transform.forward);
      RaycastHit hit;
      if (Physics.SphereCast(ray, 1.75f, out hit)) {
        GameObject hitObject = hit.transform.gameObject;
        if (hitObject.GetComponent<PlayerCharacter>()) {
          if (_fireball == null) {
            _fireball = Instantiate(fireballPrefab) as GameObject;
            // Place fireball in front of enemy and point in the same direction.
            _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            _fireball.transform.rotation = transform.rotation;
          }
        } else if (hit.distance < obstacleRange) {
          float angle = Random.Range(-110, 110);
          transform.Rotate(0, angle, 0);
        }
      }
    }
  }

}
