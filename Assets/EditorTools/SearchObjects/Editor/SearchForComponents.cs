using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayStudios.EditorTools.SearchObjects.Editor
{
	public class SearchForComponents : EditorWindow
	{
		[MenuItem( "Tools/Prefab Tools/Search For Components" )]
		private static void Init ()
		{
			var window = (SearchForComponents) EditorWindow.GetWindow( typeof( SearchForComponents ) );
			window.Show();
			window.position = new Rect( 20, 80, 400, 300 );
		}

		private readonly string[] _modes = new string[] { "Search for component usage", "Search for missing components" };

		private List<string> _listResult;
		private int _editorMode;
		private int _editorModeOld;
		private MonoScript _targetComponent;
		private MonoScript _lastChecked = null;
		private string _componentName = "";
		private Vector2 _scroll;

		private void OnGUI ()
		{
			GUILayout.Space( 3 );
			var oldValue = GUI.skin.window.padding.bottom;
			GUI.skin.window.padding.bottom = -20;
			var windowRect = GUILayoutUtility.GetRect( 1, 17 );
			windowRect.x += 4;
			windowRect.width -= 7;
			_editorMode = GUI.SelectionGrid( windowRect, _editorMode, _modes, 2, "Window" );
			GUI.skin.window.padding.bottom = oldValue;

			if ( _editorModeOld != _editorMode )
			{
				_editorModeOld = _editorMode;
				_listResult = new List<string>();
				_componentName = _targetComponent == null ? "" : _targetComponent.name;

				_lastChecked = null;
			}

			switch (_editorMode)
			{
				case 0:
					_targetComponent = (MonoScript) EditorGUILayout.ObjectField( _targetComponent, typeof( MonoScript ), false );
					if (GUILayout.Button("Check Prefabs"))
					{
						if (_targetComponent != _lastChecked)
						{
							_lastChecked = _targetComponent;
							_componentName = _targetComponent.name;
							AssetDatabase.SaveAssets();
							var targetPath = AssetDatabase.GetAssetPath(_targetComponent);
							var allPrefabs = GetAllPrefabs();
							_listResult = new List<string>();
							foreach (var prefab in allPrefabs)
							{
								var single = new string[] { prefab };
								var dependencies = AssetDatabase.GetDependencies(single);
								foreach (var dependedAsset in dependencies)
								{
									if (dependedAsset == targetPath)
									{
										_listResult.Add(prefab);
									}
								}
							}
						}
					}

					break;
				case 1:
					if (GUILayout.Button("Search!"))
					{
						var allPrefabs = GetAllPrefabs();
						_listResult = new List<string>();
						foreach (var prefab in allPrefabs)
						{
							var o = AssetDatabase.LoadMainAssetAtPath(prefab);
							GameObject go;
							try
							{
								go = (GameObject) o;
								var components = go.GetComponentsInChildren<Component>(true);
								foreach (var c in components)
								{
									if (c == null)
									{
										_listResult.Add(prefab);
									}
								}
							}
							catch
							{
								Debug.Log("For some reason, prefab " + prefab + " won't cast to GameObject");

							}
						}
					}

					break;
			}

			if (_listResult != null)
			{
				if (_listResult.Count == 0)
				{
					GUILayout.Label(_editorMode == 0 ? (_componentName == "" ? "Choose a component" : "No prefabs use component " + _componentName) : ("No prefabs have missing components!\nClick Search to check again"));
				}
				else
				{
					GUILayout.Label(_editorMode == 0 ? ("The following prefabs use component " + _componentName + ":") : ("The following prefabs have missing components:"));
					_scroll = GUILayout.BeginScrollView(_scroll);
					foreach (var s in _listResult)
					{
						GUILayout.BeginHorizontal();
						GUILayout.Label(s, GUILayout.Width(position.width / 2));
						if (GUILayout.Button("Select", GUILayout.Width(position.width / 2 - 10)))
						{
							Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(s);
						}

						GUILayout.EndHorizontal();
					}

					GUILayout.EndScrollView();
				}
			}

		}

		public static string[] GetAllPrefabs ()
		{
			var temp = AssetDatabase.GetAllAssetPaths();
			var result = new List<string>();
			foreach ( var s in temp )
			{
				if ( s.Contains( ".prefab" ) ) result.Add( s );
			}

			return result.ToArray();
		}
	}
}