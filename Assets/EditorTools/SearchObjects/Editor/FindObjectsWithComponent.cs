using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PlayStudios.EditorTools.SearchObjects.Editor
{
	public class FindObjectsWithComponent
	{
		public string component;
		private static  Type type;
		private static  int selectObjectIndex = 0;
		private static List<GameObject> objectsFound;

		private static GameObject victim;

		/*public void SearchInResources()
	{
		List<GameObject> objects = new List<GameObject>();
		objects = LoadPrefabsContaining<tk2dTextMesh>("");
		log(objects);
	}*/

		public static void SearchInScene()
		{
			objectsFound = getAllObjectsInScene();
			objectsFound = getAllChildsWithComponent(objectsFound);
			log(objectsFound);
		}

		public static  void SelectNext()
		{
			selectObjectIndex++;
			Selection.activeGameObject = objectsFound[selectObjectIndex];
		}

		public static void SetAsVictim()
		{
			victim = Selection.activeGameObject;
		}
		public static GameObject ReplaceVictimWithSelected()
		{
			GameObject go = GameObject.Instantiate(Selection.activeGameObject);
			go.transform.position = victim.transform.position;
			go.transform.rotation = victim.transform.rotation;
			go.name = victim.name;
			go.transform.parent = victim.transform.parent;
			victim.name = "_" + victim.name;
			return go;
		}

		public static List<GameObject> LoadPrefabsContaining<T>(string path) where T : Component
		{
			List<GameObject> result = new List<GameObject>();

			var allFiles = Resources.LoadAll<UnityEngine.Object>(path);
			foreach (var obj in allFiles)
			{
				if (obj is GameObject)
				{
					GameObject go = obj as GameObject;
					if (go.GetComponent<T>() != null)
					{
						result.Add(go);
					}
				}	
			}
			return result;
		}

		private static List<GameObject> getAllChildsWithComponent(List<GameObject> objects)
		{
			List<GameObject> result = new List<GameObject>();

			return result;
		}

		public static List<GameObject> getChildsWithComponent<T>(GameObject parent) where T : Component
		{
			List<GameObject> result = new List<GameObject>();

			foreach (Transform child in parent.transform)
			{
				if (child.gameObject.GetComponent<T>() != null)
				{
					result.Add(child.gameObject);
				}
			}

			return result;
		}

		public static List<GameObject> getAllGameObjects(GameObject parent)
		{
			List<GameObject> result = new List<GameObject>();

			foreach (Transform child in parent.transform)
			{
				result.Add(child.gameObject);
				result = result.Concat(getAllGameObjects(child.gameObject)).ToList();
			}

			return result;
		}


		private static List<GameObject> getSceneObjects()
		{
			object[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject));
			List<GameObject> objects = new List<GameObject>();

			foreach (object obj in allObjects)
			{
				GameObject go = obj as GameObject;
				objects.Add(go as GameObject);
			}
			return objects;
		}

		private static List<GameObject> getAllObjectsInScene()
		{
			List<GameObject> sceneObjects = new List<GameObject>();
			sceneObjects = getSceneObjects();

			List<GameObject> result = new List<GameObject>();

			foreach (var obj in sceneObjects)
			{
				result = result.Concat(getAllGameObjects(obj)).ToList();
			}

			return result;
		}

		private static void log(List<GameObject> objects)
		{
			int count = 0;
			foreach (var go in objects)
			{
				Debug.Log(GetGameObjectPath(go.transform));
				count++;
			}
			Debug.Log("Found:"+count);
		}

		private static string GetGameObjectPath(Transform transform)
		{
			string path = transform.name;
			while (transform.parent != null)
			{
				transform = transform.parent;
				path = transform.name + "/" + path;
			}
			return path;
		}
	}
}

