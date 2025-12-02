using System.Collections.Generic;
using UnityEngine;

public class MolConnections : MonoBehaviour {

    [SerializeField] private Transform _root;
    [SerializeField] private GameObject _linePrefab;

    struct Connection {
        public Transform from;
        public Transform to;
    }

    [ContextMenu("Create Connections")]
    void createConnections() {
        // 1. step build database of all spheres
        Dictionary<int, Transform> spheres = new Dictionary<int, Transform>();

        foreach (Transform t in _root.GetComponentsInChildren<Transform>()) {
            if (t.name == _root.name) continue; // skip root

            // parse id from name
            string name = t.name.Split("__")[1];
            int id = int.Parse(name);
            spheres[id] = t;
        }

        // 2. parse connections from names
        List<Connection> connections = new List<Connection>();
        foreach (Transform t in _root.GetComponentsInChildren<Transform>()) {
            if (t.name == _root.name) continue; // skip root

            int from = int.Parse(t.name.Split("__")[1]);

            // parse connections from name
            string[] splits = t.name.Split("__TO_");
            for (int i = 1; i < splits.Length; i++) {
                int to = int.Parse(splits[i]);
                connections.Add(new Connection{ from = spheres[from], to = spheres[to] });
            }
        }

        // 3. generate connections
        foreach (Connection connection in connections) {
            Debug.Log("Connection: " + connection.from.name + " to " + connection.to.name);
            Line line = Instantiate(_linePrefab, transform).GetComponent<Line>();
            line.transform.position = Vector3.Lerp(connection.from.position, connection.to.position, 0.5f);
            line.start = connection.from;
            line.end = connection.to;
        }
    }
}
