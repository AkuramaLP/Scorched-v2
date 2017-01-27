using UnityEngine;
using System.Collections;
using UnityEditor;

// Show a custom Editor
[CustomEditor(typeof(Retronator))]
public class RetronatorEditor : Editor
{
    Retronator.ResolutionMode[] resolutionModes = new[] { Retronator.ResolutionMode.relativeResolution, Retronator.ResolutionMode.fixedResolution, Retronator.ResolutionMode.fixedWidth, Retronator.ResolutionMode.fixedHeight, Retronator.ResolutionMode.fixedMax };

    string[] colorDepthOptions = new string[] { "True Color (24 Bit)", "32k Colors (15 Bit)", "4k Colors (12 Bit)", "512 Colors (9 Bit)", "64 Colors (6 Bit)", "8 Colors (3 Bit)" };

    int mode;
    int fixedResX = -1;
    int fixedResY = -1;
    int fixedWidth = -1;
    int fixedHeight = -1;
    int fixedMax = -1;
    float fadeDuration = 1f;
    bool faded = false;
    bool relativeFade = true;
    bool blurFade = false;

    public override void OnInspectorGUI()
    {
        Retronator myScript = (Retronator)target;
        if (!myScript.isActiveAndEnabled)
        {
            EditorGUILayout.LabelField("Retronator DISABLED!", EditorStyles.boldLabel);
            return;
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Settings", "Button", EditorStyles.boldLabel);

        if (GUILayout.Button("How to access at runtime"))
        {
            string text = "There are static runtime methods to change the settings in game.\r";
            text += "Please make sure, you call all methods using the class name:\rRetronator.Methodname()\r\r";
            text += "SetPixeliciousness()\r";
            text += "SetFixedResolution()\r";
            text += "SetFixedWidth()\r";
            text += "SetFixedHeight()\r";
            text += "SetFixedMax()\r";
            text += "SetMode()\r";
            text += "SetTintColor()\r";
            text += "SetBlur()\r\r";
            text += "There are some static methods for transitions:\r\r";
            text += "FadeFromMinPixels()\r";
            text += "FadeToMinPixels()\r";
            text += "ResetFade()\r\r";
            text += "See the docs for more information.";
            EditorUtility.DisplayDialog("Runtime Methods", text, "OK");
        }
        EditorGUILayout.EndHorizontal();

        if (fixedResX < 0)
            fixedResX = (int)myScript.fixedRes.x;
        if (fixedResY < 0)
            fixedResY = (int)myScript.fixedRes.y;
        if (fixedWidth < 0)
            fixedWidth = myScript.fixedWidth;
        if (fixedHeight < 0)
            fixedHeight = myScript.fixedHeight;
        if (fixedMax < 0)
            fixedMax = myScript.fixedMax;

        int i = 0;
        foreach (Retronator.ResolutionMode m in resolutionModes)
        {
            if (myScript.mode == m)
            {
                mode = i;
                break;
            }
            i++;
        }
        mode = EditorGUILayout.Popup("Resolution Mode:", mode, System.Enum.GetNames(typeof(Retronator.ResolutionMode)));
        myScript.mode = resolutionModes[mode];
        if (Retronator.validResolutions.Count < 1)
        {
            Vector2 screenSize = Retronator.GetMainGameViewSize();
            Retronator.CalculateValidResolutions(screenSize.x, screenSize.y);
        }

        if (myScript.mode == Retronator.ResolutionMode.relativeResolution)
        {
            myScript.pixeliciousness = EditorGUILayout.Slider("Pixeliciousness:", myScript.pixeliciousness, 0, 100);
        }
        if (myScript.mode == Retronator.ResolutionMode.fixedResolution)
        {
            fixedResX = EditorGUILayout.IntField("Horizontal Pixels:", fixedResX);
            fixedResY = EditorGUILayout.IntField("Vertical Pixels:", fixedResY);
            fixedResX = Mathf.Max(fixedResX, 2);
            fixedResY = Mathf.Max(fixedResY, 2);
            if (fixedResX % 2 != 0 || fixedResY % 2 != 0)
            {
                EditorGUILayout.LabelField("The resolution values must be divisible by two!", EditorStyles.boldLabel);
                if (GUILayout.Button("Set to valid values"))
                {
                    if (fixedResX % 2 != 0)
                        fixedResX = Mathf.Max(fixedResX - 1, 2);
                    if (fixedResY % 2 != 0)
                        fixedResY = Mathf.Max(fixedResY - 1, 2);
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
            else if (myScript.fixedRes.x != fixedResX || myScript.fixedRes.y != fixedResY)
            {
                if (GUILayout.Button("Apply"))
                {
                    myScript.fixedRes = new Vector2(fixedResX, fixedResY);
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
        }
        if (myScript.mode == Retronator.ResolutionMode.fixedWidth)
        {
            fixedWidth = EditorGUILayout.IntField("Horizontal Pixels:", fixedWidth);
            fixedWidth = Mathf.Max(fixedWidth, 2);
            if (fixedWidth % 2 != 0)
            {
                EditorGUILayout.LabelField("The resolution value must be divisible by two!", EditorStyles.boldLabel);
                if (GUILayout.Button("Set to valid value"))
                {
                    fixedWidth = Mathf.Max(fixedWidth - 1, 2);
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
            else if (myScript.fixedWidth != fixedWidth)
            {
                if (GUILayout.Button("Apply"))
                {
                    myScript.fixedWidth = fixedWidth;
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
        }
        if (myScript.mode == Retronator.ResolutionMode.fixedHeight)
        {
            fixedHeight = EditorGUILayout.IntField("Vertical Pixels:", fixedHeight);
            fixedHeight = Mathf.Max(fixedHeight, 2);
            if (fixedHeight % 2 != 0)
            {
                EditorGUILayout.LabelField("The resolution value must be divisible by two!", EditorStyles.boldLabel);
                if (GUILayout.Button("Set to valid value"))
                {
                    fixedHeight = Mathf.Max(fixedHeight - 1, 2);
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
            else if (myScript.fixedHeight != fixedHeight)
            {
                if (GUILayout.Button("Apply"))
                {
                    myScript.fixedHeight = fixedHeight;
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
        }
        if (myScript.mode == Retronator.ResolutionMode.fixedMax)
        {
            fixedMax = EditorGUILayout.IntField("Maximum Pixels:", fixedMax);
            fixedMax = Mathf.Max(fixedMax, 2);
            if (fixedMax % 2 != 0)
            {
                EditorGUILayout.LabelField("The resolution value must be divisible by two!", EditorStyles.boldLabel);
                if (GUILayout.Button("Set to valid value"))
                {
                    fixedMax = Mathf.Max(fixedMax - 1, 2);
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
            else if (myScript.fixedMax != fixedMax)
            {
                if (GUILayout.Button("Apply"))
                {
                    myScript.fixedMax = fixedMax;
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                }
            }
        }

        myScript.blurred = EditorGUILayout.Toggle("Blur the Image", myScript.blurred);
        myScript.tintColor = EditorGUILayout.ColorField("Screen Tint Color:", myScript.tintColor);

        //myScript.pixelizeUI = EditorGUILayout.Toggle("Pixelize User Interface?", myScript.pixelizeUI);

        //myScript.colorDepth = EditorGUILayout.Slider("Color Depth per Channel:", myScript.colorDepth, 1, 256);
        myScript.colorDepthIndex = EditorGUILayout.Popup("Colors", myScript.colorDepthIndex, colorDepthOptions);
        myScript.greyscale = EditorGUILayout.Toggle("Monochrome", myScript.greyscale);

        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        SerializedProperty tps = serializedObject.FindProperty("additionalGameCameras");
        EditorGUILayout.PropertyField(tps, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Add Camera"))
        {
            Camera[] tempArray = myScript.additionalGameCameras;
            myScript.additionalGameCameras = new Camera[myScript.additionalGameCameras.Length + 1];
            if (tempArray.Length > 0)
            {
                for (int j = 0; j < tempArray.Length; j++)
                {
                    myScript.additionalGameCameras[j] = tempArray[j];
                }
            }
        }





        //if(myScript.colorDepthIndex != 0)
        //    myScript.dithering = EditorGUILayout.Toggle("Boost Details?", myScript.dithering);

        EditorGUILayout.LabelField("Fading Test", EditorStyles.boldLabel);
        if (Application.isPlaying)
        {
            fadeDuration = EditorGUILayout.Slider("Fade Duration:", fadeDuration, 0.01f, 5f);
            relativeFade = EditorGUILayout.Toggle("Fade relative?", relativeFade);
            blurFade = EditorGUILayout.Toggle("Blur the Fading?", blurFade);
            if (GUILayout.Button("FadeFromMinPixels (" + fadeDuration + ", " + relativeFade + ", " + blurFade + ")"))
            {
                Retronator.FadeFromMinPixels(fadeDuration, relativeFade, blurFade);
                faded = true;
            }
            if (GUILayout.Button("FadeToMinPixels (" + fadeDuration + ", " + relativeFade + ", " + blurFade + ")"))
            {
                Retronator.FadeToMinPixels(fadeDuration, relativeFade, blurFade);
                faded = true;
            }
            if (GUILayout.Button("FadeFromNative (" + fadeDuration + ", " + relativeFade + ", " + blurFade + ")"))
            {
                Retronator.FadeFromNative(fadeDuration, relativeFade, blurFade);
                faded = true;
            }
            if (GUILayout.Button("FadeToNative (" + fadeDuration + ", " + relativeFade + ", " + blurFade + ")"))
            {
                Retronator.FadeToNative(fadeDuration, relativeFade, blurFade);
                faded = true;
            }
            if (faded)
            {
                if (GUILayout.Button("ResetFade()"))
                {
                    Retronator.ResetFade();
                    faded = false;
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Fading tests can only be executed in play mode!", EditorStyles.boldLabel);
        }
        EditorUtility.SetDirty(myScript);
    }

}
