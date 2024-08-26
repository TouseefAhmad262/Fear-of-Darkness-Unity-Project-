using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    public PathRenderMode RenderMode;
    public bool DestroyInGame = true;

    void Awake()
    {
        if (DestroyInGame)
        {
            Destroy(this);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (RenderMode != PathRenderMode.OnSelect)
            return;

        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform currentChild = transform.GetChild(i);
                Vector3 currentChildPosition = currentChild.position;

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(currentChildPosition, 0.5f);

                if (i < transform.childCount - 1)
                {
                    Vector3 nextChildPosition = transform.GetChild(i + 1).position;

                    Vector3 direction = nextChildPosition - currentChildPosition;
                    float distance = direction.magnitude;

                    bool isObstacle = Physics.Raycast(currentChildPosition, direction.normalized, distance);

                    Gizmos.color = isObstacle ? Color.red : Color.green;

                    Gizmos.DrawLine(currentChildPosition, nextChildPosition);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (RenderMode != PathRenderMode.Always)
            return;

        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform currentChild = transform.GetChild(i);
                Vector3 currentChildPosition = currentChild.position;

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(currentChildPosition, 0.5f);

                if (i < transform.childCount - 1)
                {
                    Vector3 nextChildPosition = transform.GetChild(i + 1).position;

                    Vector3 direction = nextChildPosition - currentChildPosition;
                    float distance = direction.magnitude;

                    bool isObstacle = Physics.Raycast(currentChildPosition, direction.normalized, distance);

                    Gizmos.color = isObstacle ? Color.red : Color.green;

                    Gizmos.DrawLine(currentChildPosition, nextChildPosition);
                }
            }
        }
    }

    public enum PathRenderMode
    {
        Always,
        OnSelect
    }
}
