using UnityEditor;
using UnityEngine;

namespace PlayStudios.EditorTools.SearchObjects.Editor
{
	public class SearchObjects : EditorWindow
	{
		public static SearchObjects window;

		public static void OpenWindow()
		{
			window = (SearchObjects)EditorWindow.GetWindow(typeof(SearchObjects)); //create a window
			window.titleContent.text = "Find object with component"; //set a window title
		}

		void OnGUI()
		{
			if (window == null)
				OpenWindow();

		
			if (GUILayout.Button("Find in current Scene"))
			{
				FindObjectsWithComponent.SearchInScene();
			}

			if (GUILayout.Button("Select Next"))
			{
				FindObjectsWithComponent.SelectNext();
			}

			if (GUILayout.Button("Set as Victim"))
			{
				FindObjectsWithComponent.SetAsVictim();
			}

			if (GUILayout.Button("Replace Victim With Selected"))
			{
				FindObjectsWithComponent.ReplaceVictimWithSelected();
			}
		}
	}
}