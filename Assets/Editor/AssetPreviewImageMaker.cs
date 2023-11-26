using UnityEngine;
using UnityEditor;
using System.IO;

namespace Worlds.Tools
{
    public class AssetPreviewImageMaker : EditorWindow
    {
        private Texture2D curTexture2D;
        private GameObject curObject;
        private const int DbsButtonsWidth = 150;
        private const int DbsButtonsHeight = 50;

        private int imagePreviewSize = 256;

        private int imageSize = 512;

        [MenuItem("Tools/Asset Preview Image Maker")]
        public static void ShowWindow()
        {
   
            GetWindow(typeof(AssetPreviewImageMaker));
        }

        private Editor goEditor;

        private GUIStyle ContainerBox()
        {
            return EditorStyles.helpBox;
        }
        private GUIStyle ButtonStyle()
        {
            return EditorStyles.toolbarButton;
        }
        public void OnGUI()
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);

            curObject = (GameObject)EditorGUILayout.ObjectField("Current Object ", curObject, typeof(GameObject), false);
            if (curObject != null)
            {
                if (GUILayout.Button("Get Preview Image", GUILayout.Height(DbsButtonsHeight)))
                {
                    curTexture2D = AssetPreview.GetAssetPreview(curObject);

                    if (curTexture2D == null)
                    {
                        Debug.Log("Didnt get preview");
                    }
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical(ButtonStyle());
            if (curObject != null)
            {
                curTexture2D = AssetPreview.GetAssetPreview(curObject);

            }
            if (curTexture2D != null)
            {
                GUILayout.Label(curTexture2D, GUILayout.Width(imagePreviewSize), GUILayout.Height(imagePreviewSize));

                imageSize = EditorGUILayout.IntField("Image Size", imageSize);
                if (GUILayout.Button("Create Icon ", GUILayout.Height(DbsButtonsHeight)))
                {
                    CreateImage();
                }
            }
            GUILayout.EndVertical();
        }

        private void CreateImage()
        {
            var savePath= Application.dataPath + "/Art/Ui";
            string filePath = EditorUtility.SaveFilePanel("Save Preview Image", savePath, "", "png");
            if (!string.IsNullOrEmpty(filePath))
            {
                curTexture2D.alphaIsTransparency = true;
                byte[] bytes = curTexture2D.EncodeToPNG();
                File.WriteAllBytes(filePath, bytes);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
            }
        }


    }
}