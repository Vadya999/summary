using Kuhpik;
using UnityEditor;
using UnityEngine;
using UnityTools;

public class SaveViewer : EditorWindow
{
    [SerializeField] private PlayerData _game;

    private Vector2 _scrollPosition;
    private SerializedProperty _gameProperty;
    private Timer _timer = new Timer(0.5f);

    [MenuItem("Custom/Save viewer")]
    private static void Init()
    {
        var window = GetWindow<SaveViewer>(false, "Save viewer", true);
    }

    private void ClearSave()
    {
        if (GUILayout.Button("Clear save"))
        {
            PlayerPrefs.DeleteKey("saveKey");
            UpdateData();
        }
    }

    private void UpdateData()
    {
        _game = SaveExtension.Load("saveKey", new PlayerData());
        var obj = new SerializedObject(this);
        _gameProperty = obj.FindProperty("_game");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Note: showing current save on DISK", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);
        CheckData();
        ClearSave();
        EditorGUILayout.BeginHorizontal();
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        EditorGUILayout.PropertyField(_gameProperty);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }

    private void CheckData()
    {
        if (_gameProperty == null) UpdateData();

        _timer.UpdateTimer();
        if (_timer.isReady)
        {
            UpdateData();
            _timer.Reset();
        }
    }
}
