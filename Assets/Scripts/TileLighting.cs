using System.Collections.Generic;
using UnityEngine;

public class TileLighting : MonoBehaviour
{
    public float radius = 20.0f;
    public Color color1 = new Color(77.0f / 255.0f, 77.0f / 255.0f, 77.0f / 255.0f, 1.0f);
    public Color color2 = new Color(198.0f / 255.0f, 198.0f / 255.0f, 198.0f / 255.0f, 1.0f);

    [SerializeField]
    private GameObject[] excludedGameObjects;

    [SerializeField]
    private List<Transform> excludedParentTransforms;

    private HashSet<GameObject> excludedObjects = new HashSet<GameObject>();

    private void Start()
    {
        // Add all excluded game objects and their children to the hash set
        foreach (GameObject go in excludedGameObjects)
        {
            AddExcludedGameObject(go);
        }

        foreach (Transform t in excludedParentTransforms)
        {
            AddExcludedChildren(t);
        }
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Renderer renderer = collider.GetComponent<Renderer>();
            if (renderer == null) continue;

            if (excludedObjects.Contains(collider.gameObject)) continue;

            float distance = Vector3.Distance(transform.position, collider.transform.position);
            Color color = Color.Lerp(color1, color2, distance / radius);
            Material material = renderer.material;
            material.color = color;
        }
    }

    public bool IsExcluded(GameObject gameObject)
    {
        return excludedObjects.Contains(gameObject);
    }

    public void AddExcludedGameObject(GameObject gameObject)
    {
        if (excludedObjects.Contains(gameObject)) return;

        excludedObjects.Add(gameObject);

        // Add all children of the game object to the hash set
        AddExcludedChildren(gameObject.transform);
    }

    public void RemoveExcludedGameObject(GameObject gameObject)
    {
        if (!excludedObjects.Contains(gameObject)) return;

        excludedObjects.Remove(gameObject);

        // Remove all children of the game object from the hash set
        RemoveExcludedChildren(gameObject.transform);
    }

    public GameObject[] GetExcludedGameObjects()
    {
        return new List<GameObject>(excludedObjects).ToArray();
    }

    public void AddExcludedParent(Transform parent)
    {
        if (!excludedParentTransforms.Contains(parent))
        {
            excludedParentTransforms.Add(parent);

            // Add all children of the parent to the hash set
            AddExcludedChildren(parent);
        }
    }

    public void RemoveExcludedParent(Transform parent)
    {
        if (excludedParentTransforms.Contains(parent))
        {
            excludedParentTransforms.Remove(parent);

            // Remove all children of the parent from the hash set
            RemoveExcludedChildren(parent);
        }
    }

    private void AddExcludedChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            excludedObjects.Add(child.gameObject);
            AddExcludedChildren(child);
        }
    }

    private void RemoveExcludedChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            excludedObjects.Remove(child.gameObject);
            RemoveExcludedChildren(child);
        }
    }
}