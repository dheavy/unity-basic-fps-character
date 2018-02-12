using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

  public float speed = 3.0f;
  public float obstacleRange = 5.0f;

  private bool _alive;

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
      if (Physics.SphereCast(ray, 0.75f, out hit)) {
        if (hit.distance < obstacleRange) {
          float angle = Random.Range(-110, 110);
          transform.Rotate(0, angle, 0);
        }
      }
    }
  }

}
