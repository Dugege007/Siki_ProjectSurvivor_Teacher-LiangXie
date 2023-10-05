using UnityEngine;
using UnityEditor;

namespace ProjectSurvivor
{
#if UNITY_EDITOR

    // 此脚本仅使用时取消注释，使用完成后注释掉
    // 因为此脚本会影响到 QFramework 的 UI

    //[CustomEditor(typeof(RepeatTileController))]
    //public class RepeatTileControllerEditor : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        base.OnInspectorGUI();

    //        if (GUILayout.Button("重新计算 Bounds"))
    //        {
    //            // 重新生成 Tilemap 的边界
    //            RepeatTileController controller = (RepeatTileController)target;
    //            controller.Tilemap.CompressBounds();
    //        }
    //    }
    //}

#endif
}
