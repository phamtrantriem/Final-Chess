using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace EditorTools.HierarchyWindowIcon.Editor
{
	[InitializeOnLoad]
	public class HierarchyWindowIcon
	{
		private static Texture2D spriteIcon;
		private static Texture2D spriteMaskIcon;
		private static Texture2D textMeshProIcon;
		private static Texture2D textMeshIcon;
		private static Texture2D uiTextIcon;
		private static Texture2D uiImageIcon;
		private static Texture2D particleSystemIcon;
		private static Texture2D animatorIcon;
		private static Texture2D sortingGroupIcon;
		private static Texture2D warnIcon;

		public static bool IsShowSortingLayerLabel = false;
		public static bool IsShowGertyTagLabel = true;

		private static bool initialized;
		static HierarchyWindowIcon()
		{
			EditorApplication.hierarchyWindowItemOnGUI += DrawIconOnWindowItem;
		}
		
		private static void DrawIconOnWindowItem(int instanceID, Rect selectionRect)
		{
			GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

			if (gameObject == null)
			{
				return;
			}

			if (!initialized)
			{
				spriteIcon = EditorGUIUtility.ObjectContent(null, typeof(SpriteRenderer)).image as Texture2D;
				spriteMaskIcon = EditorGUIUtility.ObjectContent(null, typeof(SpriteMask)).image as Texture2D;
				textMeshProIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/PlayStudios/EditorTools/HierarchyWindowIcon/Images/TMP - Text Component Icon.psd");
				textMeshIcon = EditorGUIUtility.ObjectContent(null, typeof(TextMesh)).image as Texture2D;
				uiTextIcon = EditorGUIUtility.ObjectContent(null, typeof(Text)).image as Texture2D;
				uiImageIcon = EditorGUIUtility.ObjectContent(null, typeof(Image)).image as Texture2D;
				particleSystemIcon = EditorGUIUtility.ObjectContent(null, typeof(ParticleSystem)).image as Texture2D;
				animatorIcon = EditorGUIUtility.ObjectContent(null, typeof(Animator)).image as Texture2D;
				sortingGroupIcon = EditorGUIUtility.ObjectContent(null, typeof(SortingGroup)).image as Texture2D;
				warnIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/PlayStudios/EditorTools/HierarchyWindowIcon/Images/warning_icon.png");
				initialized = true;
			}

			var iconCount = 0;

			selectionRect.height = Mathf.Clamp(selectionRect.height, 0, 20);
			var iconWidth = selectionRect.height;
			var iconFullWidth = selectionRect.width + selectionRect.x;
			selectionRect.width = iconWidth;

			var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			if (spriteRenderer != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (spriteIcon != null)
				{
					GUI.DrawTexture(selectionRect, spriteIcon);
				}

				if (spriteRenderer.sprite == null)
				{
					iconCount++;
					selectionRect.x = iconFullWidth - (iconWidth * iconCount);

					if (warnIcon != null)
					{
						GUI.DrawTexture(selectionRect, warnIcon);
					}
				}
			}
			
			var spriteMask = gameObject.GetComponent<SpriteMask>();
			if (spriteMask != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (spriteMaskIcon != null)
				{
					GUI.DrawTexture(selectionRect, spriteMaskIcon);
				}

				if (spriteMask.sprite == null)
				{
					iconCount++;
					selectionRect.x = iconFullWidth - (iconWidth * iconCount);
					
					if (warnIcon != null)
					{
						GUI.DrawTexture(selectionRect, warnIcon);
					}
				}
			}

			var textMeshPro = gameObject.GetComponent<TextMeshPro>();
			if (textMeshPro != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (textMeshProIcon != null)
				{
					GUI.DrawTexture(selectionRect, textMeshProIcon);
				}
			}
			
			var textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
			if (textMeshProUGUI != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (textMeshProIcon != null)
				{
					GUI.DrawTexture(selectionRect, textMeshProIcon);
				}
			}
			
			var textMesh = gameObject.GetComponent<TextMesh>();
			
			if (textMesh != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (textMeshIcon != null)
				{
					GUI.DrawTexture(selectionRect, textMeshIcon);
				}
			}
			
			/*var tk2DTextMesh = gameObject.GetComponent<tk2dTextMesh>();
			if (tk2DTextMesh != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (textMeshIcon != null)
				{
					GUI.DrawTexture(selectionRect, textMeshIcon);
				}
			}*/

			var uiText = gameObject.GetComponent<Text>();
			if (uiText != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);
				
				GUI.DrawTexture(selectionRect, uiTextIcon);
			}
			
			var uiImage = gameObject.GetComponent<Image>();
			if (uiImage != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (uiImageIcon != null)
				{
					GUI.DrawTexture(selectionRect, uiImageIcon);
				}

				if (uiImage.sprite == null)
				{
					iconCount++;
					selectionRect.x = iconFullWidth - (iconWidth * iconCount);
					
					if (warnIcon != null)
					{
						GUI.DrawTexture(selectionRect, warnIcon);
					}
				}
			}

			var particleSystem = gameObject.GetComponent<ParticleSystem>();
			if (particleSystem != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (particleSystemIcon != null)
				{
					GUI.DrawTexture(selectionRect, particleSystemIcon);
				}
			}
		
			var animator = gameObject.GetComponent<Animator>();
			if (animator != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (animatorIcon != null)
				{
					GUI.DrawTexture(selectionRect, animatorIcon);
				}

				if (animator.runtimeAnimatorController == null)
				{
					iconCount++;
					selectionRect.x = iconFullWidth - (iconWidth * iconCount);

					if (warnIcon != null)
					{
						GUI.DrawTexture(selectionRect, warnIcon);
					}
				}
			}

			var renderer = gameObject.GetComponent<Renderer>();

			if (renderer != null && renderer.sharedMaterial == null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (warnIcon != null)
				{
					GUI.DrawTexture(selectionRect, warnIcon);
				}
			}

			var sortingGroup = gameObject.GetComponent<SortingGroup>();
			if (sortingGroup != null)
			{
				iconCount++;
				selectionRect.x = iconFullWidth - (iconWidth * iconCount);

				if (sortingGroupIcon != null)
				{
					GUI.DrawTexture(selectionRect, sortingGroupIcon);
				}
			}

			selectionRect.x = iconFullWidth - (iconWidth * iconCount);

			if (IsShowSortingLayerLabel)
			{
				if (renderer != null)
				{
					var sortingLayerText = new GUIContent(string.Format("{0}({1})", renderer.sortingLayerName, renderer.sortingOrder));
					Vector2 sortingLayerSize = GUIStyle.none.CalcSize(sortingLayerText);

					selectionRect.x -= sortingLayerSize.x + 5;
					selectionRect.width = sortingLayerSize.x + 5;

					GUI.Label(selectionRect, sortingLayerText);

					var goLayerText = new GUIContent(string.Format("{0}({1:N1})", LayerMask.LayerToName(gameObject.layer), gameObject.transform.position.z));
					Vector2 goLayerTextSize = GUIStyle.none.CalcSize(goLayerText);

					selectionRect.x -= goLayerTextSize.x + 5;
					selectionRect.width = goLayerTextSize.x + 5;

					GUI.Label(selectionRect, goLayerText);
				}
			}

			/*if (IsShowGertyTagLabel)
			{
				var gertyTag = gameObject.GetComponent<GertyTag>();

				if (gertyTag != null)
				{
					var gertyTagText = new GUIContent(string.Format("{0}", gertyTag.Tag));
					Vector2 gertyTagSize = GUIStyle.none.CalcSize(gertyTagText);

					selectionRect.x -= gertyTagSize.x + 5;
					selectionRect.width = gertyTagSize.x + 5;

					GUIStyle style = new GUIStyle();
					style.normal.textColor = new Color(135f/255f, 206f/255f, 235f/255f);
					GUI.Label(selectionRect, gertyTagText, style);
				}
			}*/
		}


		/*[MenuItem("GameObject/Tools/Toggle Showing GertyTag")]
		private static void ToggleShowingGertyTag()
		{
			IsShowGertyTagLabel = !IsShowGertyTagLabel;
		}*/
	}
}