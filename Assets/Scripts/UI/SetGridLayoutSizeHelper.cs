using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class SetGridLayoutSizeHelper : MonoBehaviour {
    private GridLayoutGroup gridLayoutGroup;

    private void Awake() {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    public void SetCellSize(float size) {
        if (gridLayoutGroup != null) {
            gridLayoutGroup.cellSize = new Vector2(size, size);
        }
    }

    public void SetCellWidth(float width) {
        if (gridLayoutGroup != null) 
        {
            Vector2 cellSize = gridLayoutGroup.cellSize;
            cellSize.x = width;
            gridLayoutGroup.cellSize = cellSize;
        }
    }

    public void SetCellHeight(float height) {
        if (gridLayoutGroup != null) 
        {
            Vector2 cellSize = gridLayoutGroup.cellSize;
            cellSize.y = height;
            gridLayoutGroup.cellSize = cellSize;
        }
    }

    public void SetCellSpacing(float spacing) {
        if (gridLayoutGroup != null) {
            gridLayoutGroup.spacing = new Vector2(spacing, spacing);
        }
    }
}