using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProjectSurvivor.AbilityConfig))]
public class AbilityConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ����Ĭ�ϵ�Inspector
        DrawDefaultInspector();

        ProjectSurvivor.AbilityConfig config = (ProjectSurvivor.AbilityConfig)target;

        // ���һ����ť��������µ�AbilityPower
        if (GUILayout.Button("��� AbilityPower"))
        {
            ProjectSurvivor.AbilityPower newPower = new ProjectSurvivor.AbilityPower();
            newPower.Lv = (config.Powers.Count + 1).ToString();
            config.Powers.Add(newPower);
        }

        // ���һ����ť������������AbilityPowers
        if (GUILayout.Button("���±��"))
        {
            for (int i = 0; i < config.Powers.Count; i++)
            {
                config.Powers[i].Lv = (i + 1).ToString();
            }
        }

        EditorGUILayout.LabelField("��ֵ����", EditorStyles.boldLabel);

        // ����Powers�б�
        for (int i = 0; i < config.Powers.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            // ����һ����ʾ��ǰ�ȼ��ı��⣬�Ӵ�
            EditorGUILayout.LabelField("�ȼ� " + config.Powers[i].Lv, EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            for (int j = 0; j < config.Powers[i].PowerDatas.Length; j++)
            {
                EditorGUILayout.BeginHorizontal();
                // ���ñ�ǩ���
                EditorGUILayout.LabelField("PowerType " + (j + 1) + ":", GUILayout.Width(80));
                // ����PowerType�����˵��Ͷ�Ӧ����ֵ�������ͬһ��
                config.Powers[i].PowerDatas[j].Type = (ProjectSurvivor.AbilityPower.PowerType)EditorGUILayout.EnumPopup(config.Powers[i].PowerDatas[j].Type, GUILayout.MinWidth(120));
                config.Powers[i].PowerDatas[j].Value = EditorGUILayout.FloatField(config.Powers[i].PowerDatas[j].Value, GUILayout.MinWidth(60));
                EditorGUILayout.EndHorizontal();
            }
        }

        // ����������κθ��ģ��ǵñ�Ƕ���Ϊ"dirty"���Ա���ı�����
        if (GUI.changed)
        {
            EditorUtility.SetDirty(config);
        }
    }
}
