using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonoBehaviours
{
    public class UnitHoldingZone : GameZone
    {
        [SerializeField] private int _maxSlots = 9;

        [Header("Slot generation")] 
        [SerializeField]
        private int _candidateGridResolution = 9;
        [SerializeField] 
        private float _edgePadding = 0.05f;

        private readonly List<Unit> _units = new();

        public bool IsFull => _units.Count >= _maxSlots;
        public int Count => _units.Count;

        public bool TryAddUnit(Unit unit)
        {
            if (unit == null || _units.Contains(unit))
            {
                return false;
            }

            if (IsFull)
            {
                return false;
            }

            _units.Add(unit);
            ReassignTargets();
            return true;
        }

        public bool RemoveUnit(Unit unit)
        {
            if (!_units.Remove(unit))
            {
                return false;
            }

            ReassignTargets();
            return true;
        }

        private void ReassignTargets()
        {
            List<Vector2> localSlots = GenerateBalancedSlots(_units.Count);

            for (int i = 0; i < _units.Count; i++)
            {
                Vector3 local = transform.position + new Vector3(localSlots[i].x, localSlots[i].y, -1);
                //Vector3 world = transform.TransformPoint(local);
                _units[i].SetTargetPosition(local);
            }
        }

        private List<Vector2> GenerateBalancedSlots(int count)
        {
            List<Vector2> result = new();

            if (count <= 0)
            {
                return result;
            }

            if (count == 1)
            {
                result.Add(Vector2.zero);
                return result;
            }

            bool hasCentreUnit = count % 2 == 1;
            int pairCount = count / 2;

            List<Vector2> pairAnchors = ChooseMirroredPairAnchors(pairCount);

            foreach (Vector2 anchor in pairAnchors)
            {
                result.Add(anchor);
                result.Add(-anchor);
            }

            if (hasCentreUnit)
            {
                result.Insert(0, Vector2.zero);
            }

            return result;
        }

        private List<Vector2> ChooseMirroredPairAnchors(int pairCount)
        {
            List<Vector2> candidates = GenerateCandidateAnchors();
            List<Vector2> chosen = new();

            // Choose NW / SE as a starting point
            Vector2 first = new(-HalfSize.x, HalfSize.y);
            chosen.Add(first);

            while (chosen.Count < pairCount)
            {
                Vector2 bestCandidate = Vector2.zero;
                float bestScore = float.NegativeInfinity;

                foreach (Vector2 candidate in candidates)
                {
                    if (IsAlreadyChosen(candidate, chosen))
                    {
                        continue;
                    }

                    float score = ScoreCandidate(candidate, chosen);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestCandidate = candidate;
                    }
                }

                chosen.Add(bestCandidate);
            }

            return chosen;
        }

        private float ScoreCandidate(Vector2 candidate, List<Vector2> chosen)
        {
            List<Vector2> existingPoints = new();

            foreach (Vector2 anchor in chosen)
            {
                existingPoints.Add(anchor);
                existingPoints.Add(-anchor);
            }

            Vector2 a = candidate;
            Vector2 b = -candidate;

            float minDistance = float.PositiveInfinity;

            foreach (Vector2 point in existingPoints)
            {
                minDistance = Mathf.Min(minDistance, Vector2.Distance(a, point));
                minDistance = Mathf.Min(minDistance, Vector2.Distance(b, point));
            }

            // Prefer points farther from the centre when distances tie.
            float centreBias = candidate.sqrMagnitude * 0.001f;

            return minDistance + centreBias;
        }

        private List<Vector2> GenerateCandidateAnchors()
        {
            List<Vector2> candidates = new();

            Vector2 min = -HalfSize;
            Vector2 max = HalfSize;
            int resolution = Mathf.Max(2, _candidateGridResolution);

            for (int x = 0; x < resolution; x++)
            {
                for (int y = 0; y < resolution; y++)
                {
                    float px = Mathf.Lerp(min.x, max.x, x / (float)(resolution - 1));
                    float py = Mathf.Lerp(min.y, max.y, y / (float)(resolution - 1));

                    Vector2 p = new(px, py);

                    if (p == Vector2.zero)
                    {
                        continue;
                    }

                    // Keep only one side of each mirrored pair.
                    if (p.x > 0f)
                    {
                        continue;
                    }

                    if (Mathf.Approximately(p.x, 0f) && p.y < 0f)
                    {
                        continue;
                    }

                    candidates.Add(p);
                }
            }

            return candidates;
        }

        private bool IsAlreadyChosen(Vector2 candidate, List<Vector2> chosen)
        {
            return chosen.Any(c =>
                Vector2.Distance(c, candidate) < 0.01f ||
                Vector2.Distance(c, -candidate) < 0.01f);
        }

        private Vector2 _size => new Vector2(_halfWidth*2f, _halfLength*2f);
        private Vector2 HalfSize => _size * (0.5f - _edgePadding);

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_size.x, _size.y, 0.05f));
        }
#endif
    }
}