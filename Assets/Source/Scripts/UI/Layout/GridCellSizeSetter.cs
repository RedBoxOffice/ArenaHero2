using ArenaHero.Utils.UnityTool;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaHero.UI
{
    [ExecuteAlways]
    [RequireComponent(typeof(GridLayoutGroup))]
    public sealed class GridCellSizeSetter : MonoBehaviour, ILayoutGroup
    {
        [SerializeField, HideInInspector] private bool _isHorizontal;
        [SerializeField, HideInInspector] private bool _isVertical;
        [SerializeField, HideInInspector] private bool _isGrid;

        [SerializeField] private GridType _gridType;

        [SerializeField, VisibleToCondition(nameof(_isGrid))] private int _rowCount;
        [SerializeField, VisibleToCondition(nameof(_isGrid))] private int _columnCount;

        [SerializeField, VisibleToCondition(nameof(_isGrid), false)] private int _countCells;

        [SerializeField] private RectTransform _parent;

        private GridLayoutGroup _grid;

        private enum GridType
        {
            Horizontal,
            Vertical,
            Grid
        }

        private void OnValidate()
        {
            if (_grid == null)
                _grid = GetComponent<GridLayoutGroup>();

            if (_parent == null)
                _parent = GetComponent<RectTransform>();

            _isHorizontal = _gridType == GridType.Horizontal;
            _isVertical = _gridType == GridType.Vertical;
            _isGrid = _gridType == GridType.Grid;

            SetLayoutVertical();
        }

        private Vector2 CalculateSize()
        {
            Vector2 size;

            switch (_gridType)
            {
                case (GridType.Grid):
                    size.x = _parent.rect.size.x / _columnCount;
                    size.y = _parent.rect.size.y / _rowCount;
                    break;
                case (GridType.Horizontal):
                    size.x = _parent.rect.size.x / _countCells;
                    size.y = _parent.rect.size.y;
                    break;
                case (GridType.Vertical):
                    size.x = _parent.rect.size.x;
                    size.y = _parent.rect.size.y / _countCells;
                    break;

                default: 
                    size = new Vector2();
                    break;
            }

            return size;
        }

        public void SetLayoutHorizontal() { }

        public void SetLayoutVertical()
        {
            _grid.cellSize = CalculateSize();
        }
    }
}