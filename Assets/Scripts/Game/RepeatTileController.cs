using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

namespace ProjectSurvivor
{
    public partial class RepeatTileController : ViewController
    {
        private Tilemap mUp;
        private Tilemap mDown;
        private Tilemap mLeft;
        private Tilemap mRight;
        private Tilemap mUpLeft;
        private Tilemap mUpRight;
        private Tilemap mDownLeft;
        private Tilemap mDownRight;
        private Tilemap mCenter;

        private int AreaX = 0;
        private int AreaY = 0;

        private void Start()
        {
            // 可以先重新生成一下 Tilemap 的边界
            Tilemap.CompressBounds();

            CreateTileMaps();
            UpdatePositions();
        }

        private void Update()
        {
            if (Player.Default && Time.frameCount % 60 == 0)
            {
                // 将角色坐标转换为 Tilemap 坐标
                Vector3Int cellPos = Tilemap.layoutGrid.WorldToCell(Player.Default.transform.position);
                AreaX = cellPos.x / Tilemap.size.x;
                AreaY = cellPos.y / Tilemap.size.y;
                UpdatePositions();
            }
        }

        private void UpdatePositions()
        {
            mUp.Position(new Vector3(AreaX * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
            mDown.Position(new Vector3(AreaX * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
            mLeft.Position(new Vector3((AreaX - 1) * Tilemap.size.x, AreaY * Tilemap.size.y));
            mRight.Position(new Vector3((AreaX + 1) * Tilemap.size.x, AreaY * Tilemap.size.y));
            mUpLeft.Position(new Vector3((AreaX - 1) * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
            mUpRight.Position(new Vector3((AreaX + 1) * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
            mDownLeft.Position(new Vector3((AreaX - 1) * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
            mDownRight.Position(new Vector3((AreaX + 1) * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
            mCenter.Position(new Vector3(AreaX * Tilemap.size.x, AreaY * Tilemap.size.y));
        }

        private void CreateTileMaps()
        {
            mUp = Tilemap.InstantiateWithParent(transform);
            mDown = Tilemap.InstantiateWithParent(transform);
            mLeft = Tilemap.InstantiateWithParent(transform);
            mRight = Tilemap.InstantiateWithParent(transform);
            mUpLeft = Tilemap.InstantiateWithParent(transform);
            mUpRight = Tilemap.InstantiateWithParent(transform);
            mDownLeft = Tilemap.InstantiateWithParent(transform);
            mDownRight = Tilemap.InstantiateWithParent(transform);
            mCenter = Tilemap;
        }
    }
}
