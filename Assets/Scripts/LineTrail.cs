using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Planet
{
    public class LineTrail : MonoBehaviour
    {
        [SerializeField] int MaxLineLength = 20;

        LinkedList<Vector3> LinePositions = new LinkedList<Vector3>();
        LineRenderer LineRenderer;

        void Start()
        {
            LineRenderer = GetComponent<LineRenderer>();

            for (int i = 0; i < MaxLineLength; i++)
                LinePositions.AddLast(transform.position);

            LineRenderer.positionCount = MaxLineLength;
            LineRenderer.SetPositions(LinePositions.ToArray());
        }
        void FixedUpdate()
        {
            if (LinePositions.Count >= MaxLineLength) LinePositions.RemoveLast();

            LinePositions.AddFirst(transform.position);
            LineRenderer.SetPositions(LinePositions.ToArray());
        }
    }
}