using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class RayShooter : MonoBehaviour {

  private Camera _camera;

  void Start() {
    _camera = GetComponent<Camera>();

    // Hide mouse cursor at the center of the screen.
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      // Cast ray to a point in the middle of the screen.
      Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
      Ray ray = _camera.ScreenPointToRay(point);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        // Retrieve the object the ray hit, then check for ReactiveTarget component on the object.
        GameObject hitObject = hit.transform.gameObject;
        ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
        if (target != null) {
          target.ReactToHit();
        } else {
          StartCoroutine(SphereIndicator(hit.point));
        }
      }
    }
  }

  void OnGUI() {
    int size = 12;
    float posX = _camera.pixelWidth / 2 - size / 4;
    float posY = _camera.pixelHeight / 2 - size / 2;
    GUI.Label(new Rect(posX, posY, size, size), "*");
  }

  private IEnumerator SphereIndicator(Vector3 pos) {
    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    sphere.transform.position = pos;

    yield return new WaitForSeconds(1);

    Destroy(sphere);
  }

}
