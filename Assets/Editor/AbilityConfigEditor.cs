using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProjectSurvivor.AbilityConfig))]
public class AbilityConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 绘制默认的Inspector
        DrawDefaultInspector();

        ProjectSurvivor.AbilityConfig config = (ProjectSurvivor.AbilityConfig)target;

        // 添加一个按钮用于添加新的AbilityPower
        if (GUILayout.Button("添加 AbilityPower"))
        {
            ProjectSurvivor.AbilityPower newPower = new ProjectSurvivor.AbilityPower();
            newPower.Lv = (config.Powers.Count + 1).ToString();
            config.Powers.Add(newPower);
        }

        // 添加一个按钮用于重新排序AbilityPowers
        if (GUILayout.Button("重新编号"))
        {
            for (int i = 0; i < config.Powers.Count; i++)
            {
                config.Powers[i].Lv = (i + 1).ToString();
            }
        }

        EditorGUILayout.LabelField("数值调整", EditorStyles.boldLabel);

        // 遍历Powers列表
        for (int i = 0; i < config.Powers.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            // 绘制一个表示当前等级的标题，加粗
            EditorGUILayout.LabelField("等级 " + config.Powers[i].Lv, EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            for (int j = 0; j < config.Powers[i].PowerDatas.Length; j++)
            {
                EditorGUILayout.BeginHorizontal();
                // 设置标签宽度
                EditorGUILayout.LabelField("PowerType " + (j + 1) + ":", GUILayout.Width(80));
                // 绘制PowerType下拉菜单和对应的数值输入框在同一行
                config.Powers[i].PowerDatas[j].Type = (ProjectSurvivor.AbilityPower.PowerType)EditorGUILayout.EnumPopup(config.Powers[i].PowerDatas[j].Type, GUILayout.MinWidth(120));
                config.Powers[i].PowerDatas[j].Value = EditorGUILayout.FloatField(config.Powers[i].PowerDatas[j].Value, GUILayout.MinWidth(60));
                EditorGUILayout.EndHorizontal();
            }
        }

        // 如果你做了任何更改，记得标记对象为"dirty"，以便更改被保存
        if (GUI.changed)
        {
            EditorUtility.SetDirty(config);
        }
    }
}
