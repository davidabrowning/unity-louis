using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class GridHighlighterScript : MonoBehaviourSingletonBase<GridHighlighterScript>
    {
        public GameObject HighlightPrefab;
        private List<GameObject> _currentHighlights = new();

        private void Start()
        {
        }

        public void Highlight(List<Vector2Int> tiles)
        {
            Hide();
            foreach (Vector2Int tile in tiles)
            {
                GameObject currentHighlight = Instantiate(HighlightPrefab);
                currentHighlight.SetActive(true);
                currentHighlight.transform.position = new Vector3Int(tile.x, tile.y, 1);
                _currentHighlights.Add(currentHighlight);
            }
        }

        public void Hide()
        {
            foreach (GameObject currentHighlight in _currentHighlights)
            {
                currentHighlight.SetActive(false);
                Destroy(currentHighlight);
            }
            _currentHighlights.Clear();
        }
    }
}
