using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

[ExecuteInEditMode]
public class Retronator : MonoBehaviour {

    // Create options
    public float pixeliciousness = 0;
    public Vector2 fixedRes = new Vector2(640,360);
    public int fixedWidth = 640;
    public int fixedHeight = 360;
    public int fixedMax = 640;
    public bool blurred = false;
    public ResolutionMode mode = ResolutionMode.relativeResolution;
    public Color tintColor = Color.white;
    public bool relativeFade = false;
    public bool pixelizeUI = false;
    public static bool setTempNative = false;
    public int colorDepthIndex = 0; // see RetronatorEditor -> string[] colorDepthOptions
    public bool greyscale = false;
    public Camera[] additionalGameCameras;

    // Create variables for current option values
    float currentPixeliciousness;
    Vector2 currentFixedRes;
    int currentFixedWidth;
    int currentFixedHeight;
    int currentFixedMax;
    bool currentBlurred;
    ResolutionMode currentMode;
    Color currentTintColor;
    public bool isFading = false;
    public Vector2 currentNonFadeResolution;
    bool currentPixelizeUI;
    public bool tempFadingBlur = false;
    public int currentColorDepthIndex;
    public bool currentGreyscale;

    // Create variables for needed objects for overlay and rendering
    Canvas canv;
    RenderTexture renderTexture;
    Camera[] cams;
    GameObject uiCam;
    GameObject canvas;
    GameObject panel;
    RawImage rawImage;
    Material mat;
    public static Retronator instance;
    public bool savingScene = false;

    public class RenderTextureOptions
    {
        public readonly bool Fade;
        public readonly int SizeX;
        public readonly int SizeY;
        public RenderTextureOptions(bool fade, int width, int height)
        {
            Fade = fade;
            SizeX = width;
            SizeY = height;
        }
    }

    public static List<Vector2> validResolutions = new List<Vector2>();

    #region public mehtods
    // Public Methods to manipulate Rendering at Runtime

    /// <summary>
    /// Set the Pixeliciousness for relative Resolution Mode and optionally switch to Relative Resolution Mode
    /// </summary>
    /// <param name="rate">Pixeliciousness: 0f ... 100f</param>
    /// <param name="setMode">Do you want to switch to Relative Resolution Mode after setting the Pixeliciousness?</param>
    public static void SetPixeliciousness(float rate, bool setMode = true)
    {
        if(instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        rate = Mathf.Clamp(rate, 0f, 100f);
        instance.pixeliciousness = rate;
        if (setMode)
            Retronator.SetMode(ResolutionMode.relativeResolution);
    }

    /// <summary>
    /// Get the Pixeliciousness for relative Resolution Mode
    /// </summary>
    public static float GetPixeliciousness()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentPixeliciousness;
    }

    /// <summary>
    /// Set a fixed Resolution and optionally switch to Fixed Resolution Mode
    /// </summary>
    /// <param name="width">Number of Pixels to be rendered on the screen in each row</param>
    /// <param name="height">Number of Pixels to be rendered on the screen in each column</param>
    /// <param name="setMode">Do you want to switch to Fixed Resolution Mode after setting the Pixeliciousness?</param>
    public static void SetFixedResolution(int width, int height, bool setMode = true)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        width = Mathf.Clamp(width, 2, Screen.width);
        height = Mathf.Clamp(height, 2, Screen.height);
        if(width % 2 != 0)
            width--;
        if(height % 2 != 0)
            height--;
        instance.fixedRes = new Vector2(width, height);
        if (setMode)
            Retronator.SetMode(ResolutionMode.fixedResolution);
    }

    /// <summary>
    /// Get the fixed Resolution Width and Height
    /// </summary>
    public static Vector2 GetFixedResolution()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentFixedRes;
    }

    /// <summary>
    /// Set a fixed Width and optionally switch to Fixed Width Mode
    /// </summary>
    /// <param name="width">Number of Pixels to be rendered on the screen in each row</param>
    /// <param name="setMode">Do you want to switch to Fixed Width Mode after setting the Width?</param>
    public static void SetFixedWidth(int width, bool setMode = true)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        width = Mathf.Clamp(width, 2, Screen.width);
        if (width % 2 != 0)
            width--;
        instance.fixedWidth = width;
        if (setMode)
            Retronator.SetMode(ResolutionMode.fixedWidth);
    }

    /// <summary>
    /// Get the fixed Width
    /// </summary>
    public static int GetFixedWidth()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentFixedWidth;
    }

    /// <summary>
    /// Set a fixed Height and optionally switch to Fixed Height Mode
    /// </summary>
    /// <param name="height">Number of Pixels to be rendered on the screen in each column</param>
    /// <param name="setMode">Do you want to switch to Fixed Height Mode after setting the Height?</param>
    public static void SetFixedHeight(int height, bool setMode = true)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        height = Mathf.Clamp(height, 2, Screen.height);
        if (height % 2 != 0)
            height--;
        instance.fixedHeight = height;
        if (setMode)
            Retronator.SetMode(ResolutionMode.fixedHeight);
    }

    /// <summary>
    /// Get the fixed Height
    /// </summary>
    public static int GetFixedHeight()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentFixedHeight;
    }

    /// <summary>
    /// Set a fixed maximum dimension and optionally switch to Fixed Max Mode
    /// </summary>
    /// <param name="maximum">Number of Pixels to be rendered on greater screen dimension</param>
    /// <param name="setMode">Do you want to switch to Fixed Max Mode after setting the maximum Pixels?</param>
    public static void SetFixedMax(int maximum, bool setMode = true)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        int maxRes = Mathf.Max(Screen.width, Screen.height);
        maximum = Mathf.Clamp(maximum, 2, maxRes);
        if (maximum % 2 != 0)
            maximum--;
        instance.fixedMax = maximum;
        if (setMode)
            Retronator.SetMode(ResolutionMode.fixedMax);
    }

    /// <summary>
    /// Get the Resolution for the greater Screen Dimension
    /// </summary>
    public static int GetFixedMax()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentFixedMax;
    }

    public enum ResolutionMode
    {
        relativeResolution,
        fixedResolution,
        fixedWidth,
        fixedHeight,
        fixedMax
    }

    /// <summary>
    /// Switch the Resolution Mode
    /// </summary>
    /// <param name="mode">The mode you want to Switch to: ResolutionMode.relativeResolution or ResolutionMode.fixedResolution</param>
    public static void SetMode(ResolutionMode mode)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.mode = ResolutionMode.relativeResolution;
    }

    /// <summary>
    /// Get the active Resolution Mode
    /// </summary>
    public static ResolutionMode GetMode()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentMode;
    }

    /// <summary>
    /// Change the Tint Color for rendering the Screen
    /// </summary>
    /// <param name="color">The Tint Color you want to apply</param>
    public static void SetTintColor(Color color)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.tintColor = color;
    }

    /// <summary>
    /// Get the active Tint Color
    /// </summary>
    public static Color GetTintColor()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentTintColor;
    }

    /// <summary>
    /// Switch if you want the pixels to be blurred
    /// </summary>
    /// <param name="blur">Activate or deactivate blurring?</param>
    public static void SetBlur(bool blur)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.blurred = blur;
    }

    /// <summary>
    /// Get the Status of Blur
    /// </summary>
    public static bool GetBlur()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentBlurred;
    }

    /// <summary>
    /// Gets current Pixel Resolution (even if Fade is active)
    /// </summary>
    public static Vector2 GetResolution()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return new Vector2(instance.cams[0].targetTexture.width, instance.cams[0].targetTexture.height);
    }

    /// <summary>
    /// Fade the Screen from maximum Pixeliciousness to set Picelicousness or native Resolution
    /// </summary>
    /// <param name="durationInSeconds">How much time in Seconds do you want the Fade to take?</param>
    /// <param name="toPixeliciousness">Do you want to fade until the set Pixelicousness has been reached (true) or until the native Resolution has been reached (false)?</param>
    /// <param name="blur">Do you want to temporarily activate Blurring for the Fade?</param>
    public static void FadeFromMinPixels(float durationInSeconds = 1f, bool toPixeliciousness = true, bool blur = false)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.relativeFade = toPixeliciousness;
        instance.isFading = true;
        instance.tempFadingBlur = blur;
        instance.StartCoroutine("FadeFromMinPixelsRoutine", durationInSeconds);
    }

    /// <summary>
    /// Fade the Screen from native Resolution to set Picelicousness or maximum Pixeliciousness
    /// </summary>
    /// <param name="durationInSeconds">How much time in Seconds do you want the Fade to take?</param>
    /// <param name="toPixeliciousness">Do you want to fade until the set Pixelicousness has been reached (true) or until the maximum Pixeliciousness has been reached (false)?</param>
    /// <param name="blur">Do you want to temporarily activate Blurring for the Fade?</param>
    public static void FadeFromNative(float durationInSeconds = 1f, bool toPixeliciousness = true, bool blur = false)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.relativeFade = toPixeliciousness;
        instance.isFading = true;
        instance.tempFadingBlur = blur;
        instance.StartCoroutine("FadeFromNativeRoutine", durationInSeconds);
    }

    /// <summary>
    /// Fade the Screen from set Pixeliciousness or native Resolution to maximum Pixeliciousness
    /// </summary>
    /// <param name="durationInSeconds">How much time in Seconds do you want the Fade to take?</param>
    /// <param name="fromPixeliciousness">Do you want to fade from the set Pixelicousness (true) or from the native Resolution (false)?</param>
    /// <param name="blur">Do you want to temporarily activate Blurring for the Fade?</param>
    public static void FadeToMinPixels(float durationInSeconds = 1f, bool fromPixeliciousness = true, bool blur = false)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.relativeFade = fromPixeliciousness;
        instance.isFading = true;
        instance.tempFadingBlur = blur;
        instance.StartCoroutine("FadeToMinPixelsRoutine", durationInSeconds);
    }

    /// <summary>
    /// Fade the Screen from set Pixeliciousness or maximum Pixeliciousness to native Resolution
    /// </summary>
    /// <param name="durationInSeconds">How much time in Seconds do you want the Fade to take?</param>
    /// <param name="fromPixeliciousness">Do you want to fade from the set Pixelicousness (true) or from maximum Pixeliciousness (false)?</param>
    /// <param name="blur">Do you want to temporarily activate Blurring for the Fade?</param>
    public static void FadeToNative(float durationInSeconds = 1f, bool fromPixeliciousness = true, bool blur = false)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.relativeFade = fromPixeliciousness;
        instance.isFading = true;
        instance.tempFadingBlur = blur;
        instance.StartCoroutine("FadeToNativeRoutine", durationInSeconds);
    }

    /// <summary>
    /// Set the Pixelicousness back to the configured values immediately
    /// </summary>
    public static void ResetFade()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.isFading = false;
        Vector2 size = GetMainGameViewSize();
        RenderTextureOptions options = new RenderTextureOptions(false, (int)size.x, (int)size.y);
        instance.StartCoroutine("ResetFadeRoutine", options);
    }

    /// <summary>
    /// Activate or deactivate greyscale
    /// </summary>
    /// /// <param name="active">Do you want to activate greyscale (true) or deactivate it (false)?</param>
    public static void SetGreyscale(bool active)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        instance.greyscale = active;
    }

    /// <summary>
    /// Get the current state of the greyscale option (true = enabled, false = disabled)
    /// </summary>
    public static bool GetGreyscale()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        return instance.currentGreyscale;
    }

    /// <summary>
    /// Set the color depth ("Colors" parameter)
    /// </summary>
    /// <param name="depth">Valid values are 24, 15, 12, 9, 6 and 3 (bits)</param>
    public static void SetColorDepth(int depth)
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        int index = 0;
        switch (depth)
        {
            case 24:
                index = 0;
                break;
            case 15:
                index = 1;
                break;
            case 12:
                index = 2;
                break;
            case 9:
                index = 3;
                break;
            case 6:
                index = 4;
                break;
            case 3:
                index = 5;
                break;
            default:
                Debug.LogError("Retronator: Invalid value for depth used in ToggleGreyscale." + Environment.NewLine + "Valid values are 24, 15, 12, 9, 6 and 3 (bits) only!");
                return;
        }
        instance.colorDepthIndex = index;
    }

    /// <summary>
    /// Get the current color depth ("Colors" parameter) in bits (24, 15, 12, 9, 6 or 3)
    /// </summary>
    public static int GetColorDepth()
    {
        if (instance == null || !instance.isActiveAndEnabled)
        {
            Debug.LogError("You are not allowed to access Retronator when the component is not active." + Environment.NewLine + "Enable the Retronator Component before using the class!");
            throw new Exception("No instance of retronator found!");
        }
        switch (instance.currentColorDepthIndex)
        {
            case 0:
                return 24;
            case 1:
                return 15;
            case 2:
                return 12;
            case 3:
                return 9;
            case 4:
                return 6;
            case 5:
                return 3;
            default:
                Debug.LogWarning("Retronator: Unable to get the current color depth. Returning default (24)");
                return 24;
        }
    }

    /// <summary>
    /// Disable Retronator
    /// </summary>
    public static void Disable()
    {
        if (instance.isActiveAndEnabled)
            instance.enabled = false;
    }

    /// <summary>
    /// Eenable Retronator
    /// </summary>
    /// <param name="activate">Do you want to apply the settings?</param>
    public static void Enable(bool activate = true)
    {
        if (!instance.isActiveAndEnabled)
        {
            Retronator.setTempNative = !activate;
            instance.enabled = true;
        }
    }

    /// <summary>
    /// Check if Retronator is enabled
    /// </summary>
    public static bool IsEnabled()
    {
        return instance.isActiveAndEnabled;
    }

    /// <summary>
    /// Add a game camera to the "Additional Game Cameras" list
    /// </summary>
    public static bool AddGameCamera(Camera camera)
    {
        if (camera == null)
            return false;
        List<Camera> cams = new List<Camera>(instance.additionalGameCameras);
        if (!cams.Contains(camera))
        {
            cams.Add(camera);
            instance.additionalGameCameras = cams.ToArray();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Remove a game camera from the "Additional Game Cameras" list
    /// </summary>
    public static bool RomoveGameCamera(Camera camera)
    {
        if (camera == null)
            return false;
        List<Camera> cams = new List<Camera>(instance.additionalGameCameras);
        return cams.Remove(camera);
    }

    #endregion

    #region coroutines
    public IEnumerator FadeFromMinPixelsRoutine(float duration)
    {
        List<Vector2> validResolutionsInternalReversed = new List<Vector2>(validResolutions);
        validResolutionsInternalReversed.Reverse();
        if(relativeFade)
        {
            List<Vector2> tempRes = new List<Vector2>();
            foreach(Vector2 res in validResolutionsInternalReversed)
            {
                if (res.x <= currentNonFadeResolution.x && res.y <= currentNonFadeResolution.y)
                    tempRes.Add(res);
            }
            validResolutionsInternalReversed = tempRes;
        }
        float stepDuration = duration / (float)validResolutionsInternalReversed.Count;
        foreach (Vector2 res in validResolutionsInternalReversed)
        {
            if (!isFading)
                break;
            instance.CreateRenderTexture(true, (int)res.x, (int)res.y);
            yield return new WaitForSeconds(stepDuration);
        }
        if (relativeFade)
            ResetFade();
        isFading = false;
        if (instance.tempFadingBlur)
        {
            Vector2 currentRes = GetResolution();
            instance.CreateRenderTexture(false, (int)currentRes.x, (int)currentRes.y);
        }
        instance.tempFadingBlur = false;
        yield return true;
    }

    public IEnumerator FadeFromNativeRoutine(float duration)
    {
        //if (instance.tempFadingBlur)
        //    instance.blurred = true;
        List<Vector2> validResolutionsInternal = new List<Vector2>(validResolutions);
        if (relativeFade)
        {
            List<Vector2> tempRes = new List<Vector2>();
            foreach (Vector2 res in validResolutionsInternal)
            {
                if (res.x >= currentNonFadeResolution.x && res.y >= currentNonFadeResolution.y)
                    tempRes.Add(res);
            }
            validResolutionsInternal = tempRes;
        }
        float stepDuration = duration / (float)validResolutionsInternal.Count;
        foreach (Vector2 res in validResolutionsInternal)
        {
            if (!isFading)
                break;
            instance.CreateRenderTexture(true, (int)res.x, (int)res.y);
            yield return new WaitForSeconds(stepDuration);
        }
        if (relativeFade)
            ResetFade();
        isFading = false;
        if (instance.tempFadingBlur)
        {
            Vector2 currentRes = GetResolution();
            instance.CreateRenderTexture(false, (int)currentRes.x, (int)currentRes.y);
        }
        instance.tempFadingBlur = false;
        yield return true;
    }

    public IEnumerator FadeToMinPixelsRoutine(float duration)
    {
        //if (instance.tempFadingBlur)
        //    instance.blurred = true;
        List<Vector2> validResolutionsInternal = new List<Vector2>(validResolutions);
        if (relativeFade)
        {
            List<Vector2> tempRes = new List<Vector2>();
            foreach (Vector2 res in validResolutionsInternal)
            {
                if (res.x <= currentNonFadeResolution.x && res.y <= currentNonFadeResolution.y)
                    tempRes.Add(res);
            }
            validResolutionsInternal = tempRes;
        }
        float stepDuration = duration / (float)validResolutionsInternal.Count;
        foreach (Vector2 res in validResolutionsInternal)
        {
            if (!isFading)
                break;
            CreateRenderTexture(true, (int)res.x, (int)res.y);
            yield return new WaitForSeconds(stepDuration);
        }
        isFading = false;
        if (instance.tempFadingBlur)
        {
            Vector2 currentRes = GetResolution();
            instance.CreateRenderTexture(false, (int)currentRes.x, (int)currentRes.y);
        }
        instance.tempFadingBlur = false;
        yield return true;
    }

    public IEnumerator FadeToNativeRoutine(float duration)
    {
        //if (instance.tempFadingBlur)
        //    instance.blurred = true;
        List<Vector2> validResolutionsInternalReversed = new List<Vector2>(validResolutions);
        validResolutionsInternalReversed.Reverse();
        if (relativeFade)
        {
            List<Vector2> tempRes = new List<Vector2>();
            foreach (Vector2 res in validResolutionsInternalReversed)
            {
                if (res.x >= currentNonFadeResolution.x && res.y >= currentNonFadeResolution.y)
                    tempRes.Add(res);
            }
            validResolutionsInternalReversed = tempRes;
        }
        float stepDuration = duration / (float)validResolutionsInternalReversed.Count;
        foreach (Vector2 res in validResolutionsInternalReversed)
        {
            if (!isFading)
                break;
            CreateRenderTexture(true, (int)res.x, (int)res.y);
            yield return new WaitForSeconds(stepDuration);
        }
        isFading = false;
        if (instance.tempFadingBlur)
        {
            Vector2 currentRes = GetResolution();
            instance.CreateRenderTexture(false, (int)currentRes.x, (int)currentRes.y);
        }
        instance.tempFadingBlur = false;
        yield return true;
    }

    // Routine to reset after Fading
    public IEnumerator ResetFadeRoutine(RenderTextureOptions options)
    {
        CreateRenderTexture(options.Fade, options.SizeX, options.SizeY);
        yield return true;
    }
    #endregion

    void OnDisable()
    {
        RemoveCanvasAndCameraSettings();
    }

	public void Awake()
    {
        // Remove unused objects
        while(GameObject.Find("RetroLowResCanvas") != null)
        {
            DestroyImmediate(GameObject.Find("RetroLowResCanvas"));
        }
        while (GameObject.Find("RetroLowResCam") != null)
        {
            DestroyImmediate(GameObject.Find("RetroLowResCam"));
        }
        // Create needed UI objects
        instance = gameObject.GetComponent<Retronator>();
        if (!instance.isActiveAndEnabled)
            return;
        PrepareCanvasAndCameraSettings();
        SetColorDepth();
        SetGreyscale();
        //SetDithering();
	}

    private void PrepareCanvasAndCameraSettings()
    {
        mat = Resources.Load<Material>("RetroLowResMat");
        mat.SetTexture("_MainTex", renderTexture);
        canvas = new GameObject("RetroLowResCanvas");
        canvas.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
        canvas.layer = LayerMask.NameToLayer("UI");
        canvas.AddComponent<RectTransform>();
        canv = canvas.AddComponent<Canvas>();
        canv.renderMode = RenderMode.ScreenSpaceOverlay;
        canv.overrideSorting = false;
        SetCanvasDepth(pixelizeUI);
        canvas.AddComponent<CanvasRenderer>();
        rawImage = canvas.AddComponent<RawImage>();

        // Set up the cameras
        if (additionalGameCameras == null)
            additionalGameCameras = new Camera[0];
        cams = new Camera[additionalGameCameras.Length + 1];
        cams[0] = gameObject.GetComponent<Camera>();
        cams[0].cullingMask = ~(1 << LayerMask.NameToLayer("UI"));
        for (int i = 1; i <= additionalGameCameras.Length; i++)
        {
            if (additionalGameCameras[i - 1] != null)
            {
                cams[i] = additionalGameCameras[i - 1];
                cams[i].cullingMask = ~(1 << LayerMask.NameToLayer("UI"));
            }
        }
        uiCam = new GameObject("RetroLowResCam");
        uiCam.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
        uiCam.AddComponent<Camera>();
        Camera uiCamCam = uiCam.GetComponent<Camera>();
        uiCamCam.cullingMask = (1 << LayerMask.NameToLayer("UI"));
        uiCamCam.clearFlags = CameraClearFlags.Nothing;
        uiCamCam.orthographic = true;

        // Calculate the resolution steps for transitions
        CalculateValidResolutions();
        // Create the first render texture with current settings
        if (setTempNative)
        {
            Vector2 size = Retronator.GetMainGameViewSize();
            CreateRenderTexture(true, (int)size.x, (int)size.y);
        }
        else
        {
            CreateRenderTexture();
        }
        // set a material for compatibility reasons
        rawImage.material = mat;
        //rawImage.material = new Material(Shader.Find("UI/Default"));
        //rawImage.material.mainTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);

        currentPixelizeUI = pixelizeUI;
    }

    private void RemoveCanvasAndCameraSettings()
    {
        if(canvas != null)
            DestroyImmediate(canvas);
        if (uiCam != null)
            DestroyImmediate(uiCam);
        for(int i = 0; i < cams.Length; i++)
        {
            if (cams[i] != null)
            {
                if (cams[i].targetTexture != null)
                {
                    //cam.targetTexture.Release();
                    cams[i].targetTexture = null;
                }
                cams[i].cullingMask |= (1 << LayerMask.NameToLayer("UI"));
            }
        }
        if(savingScene)
        {
            savingScene = false;
            this.enabled = true;
        }

    }

    public static void CalculateValidResolutions(float width = -1, float height = -1)
    {
        Retronator.validResolutions.Clear();
        int x = Screen.width;
        int y = Screen.height;
        if(width > 0 && height > 0)
        {
            x = (int)width;
            y = (int)height;
        }
        Retronator.validResolutions.Add(new Vector2(x, y));
        if (x >= y)
        {
            while (x > 1)
            {
                x = (int)((float)x * 0.9f);
                if (x % 2 != 0)
                    x--;
                y = (int)((float)Screen.height * ((float)x / (float)Screen.width));
                if (y % 2 != 0)
                    y--;
                if (x > 0 && y > 0)
                    Retronator.validResolutions.Add(new Vector2(x, y));
                else
                    break;
            }
        }
        else
        {
            while (y > 1)
            {
                y = (int)((float)y * 0.9f);
                if (y % 2 != 0)
                    y--;
                x = (int)((float)Screen.height * ((float)y / (float)Screen.width));
                if (x % 2 != 0)
                    x--;
                if (y > 0 && x > 0)
                    Retronator.validResolutions.Add(new Vector2(y, x));
                else
                    break;
            }
        }
    }

    void Update () {
        if (canvas == null)
            //PrepareCanvasAndCameraSettings();
            Awake();
        // If any option changed, create a new render texture
        if (mode != currentMode ||
           (mode == ResolutionMode.relativeResolution && pixeliciousness != currentPixeliciousness) ||
           (mode == ResolutionMode.fixedResolution && (fixedRes != currentFixedRes)) ||
           (mode == ResolutionMode.fixedWidth && (fixedWidth != currentFixedWidth)) ||
           (mode == ResolutionMode.fixedHeight && (fixedHeight != currentFixedHeight)) ||
           (mode == ResolutionMode.fixedMax && (fixedMax != currentFixedMax)) ||
           (blurred != currentBlurred) ||
           currentTintColor != tintColor)
            CreateRenderTexture();
        if (pixelizeUI != currentPixelizeUI)
            SetCanvasDepth(pixelizeUI);
        if (colorDepthIndex != currentColorDepthIndex)
            SetColorDepth();
        //if (dithering != currentDithering)
        //    SetDithering();
        if (greyscale != currentGreyscale)
            SetGreyscale();
	}

    private void SetColorDepth()
    {
        switch (colorDepthIndex)
        {
            case 0:
                canv.GetComponent<RawImage>().material.SetFloat("_ColorDepth", 255);
                break;
            case 1:
                canv.GetComponent<RawImage>().material.SetFloat("_ColorDepth", 31);
                break;
            case 2:
                canv.GetComponent<RawImage>().material.SetFloat("_ColorDepth", 15);
                break;
            case 3:
                canv.GetComponent<RawImage>().material.SetFloat("_ColorDepth", 7);
                break;
            case 4:
                canv.GetComponent<RawImage>().material.SetFloat("_ColorDepth", 3);
                break;
            case 5:
                canv.GetComponent<RawImage>().material.SetFloat("_ColorDepth", 1);
                break;
            default:
                canv.GetComponent<RawImage>().material.SetFloat("_ColorDepth", 255);
                break;
        }
        currentColorDepthIndex = colorDepthIndex; 
    }

    //private void SetDithering()
    //{
    //    Debug.Log(dithering);
    //    if(dithering)
    //        canv.GetComponent<RawImage>().material.SetInt("_ColorDither", 1);
    //    else
    //        canv.GetComponent<RawImage>().material.SetInt("_ColorDither", 0);
    //    currentDithering = dithering;
    //}

    private void SetGreyscale()
    {
        if(greyscale)
            canv.GetComponent<RawImage>().material.SetInt("_Greyscale", 1);
        else
            canv.GetComponent<RawImage>().material.SetInt("_Greyscale", 0);
        currentGreyscale = greyscale;
    }

    private void SetCanvasDepth(bool pixelizeUI)
    {
        if(pixelizeUI)
            canv.sortingOrder = 32767;
        else
            canv.sortingOrder = -32768;
        currentPixelizeUI = pixelizeUI;
    }

    private void CreateRenderTexture(bool fade = false, int newWidth = -1, int newHeight = -1)
    {
        if (!Application.isPlaying)
        {
            CalculateValidResolutions();
        }
        // Set new width and height depending on screen size and options
        if (fade && newWidth < 0 || newHeight < 0)
        {
            newWidth = Screen.width;
            newHeight = Screen.height;
        }
        if (!fade && newWidth == Screen.width && newHeight == Screen.height)
        {
            if (instance.mode == ResolutionMode.relativeResolution)
            {
                int validResolution = Mathf.Min(Mathf.RoundToInt((((float)validResolutions.Count / 100f) * pixeliciousness))-1,validResolutions.Count);
                if (pixeliciousness > 0 && validResolution >= 0)
                {
                    newWidth = (int)(validResolutions[validResolution].x);
                    newHeight = (int)(validResolutions[validResolution].y);
                }
                else if(pixeliciousness > 0)
                {
                    newWidth = (int)(validResolutions[0].x);
                    newHeight = (int)(validResolutions[0].y);
                }
                currentPixeliciousness = pixeliciousness;
            }
            if (instance.mode == ResolutionMode.fixedResolution)
            {
                newWidth = (int)fixedRes.x;
                newHeight = (int)fixedRes.y;
                currentFixedRes = fixedRes;
            }
            if (instance.mode == ResolutionMode.fixedWidth || (instance.mode == ResolutionMode.fixedMax && Screen.width >= Screen.height))
            {
                if (instance.mode == ResolutionMode.fixedWidth)
                    newWidth = fixedWidth;
                if (instance.mode == ResolutionMode.fixedMax)
                    newWidth = fixedMax;
                float factor = (float)Screen.width / (float)Screen.height;
                newHeight = Mathf.RoundToInt(newWidth / factor);
                if(newHeight % 2 != 0)
                    newHeight = Mathf.Max(newHeight - 1, 2);
                if (instance.mode == ResolutionMode.fixedWidth)
                    currentFixedWidth = fixedWidth;
                if (instance.mode == ResolutionMode.fixedMax)
                    currentFixedMax = fixedMax;
            }
            if (instance.mode == ResolutionMode.fixedHeight || (instance.mode == ResolutionMode.fixedMax && Screen.width < Screen.height))
            {
                if (instance.mode == ResolutionMode.fixedHeight)
                    newHeight = fixedHeight;
                if (instance.mode == ResolutionMode.fixedMax)
                    newHeight = fixedMax;
                float factor = (float)Screen.width / (float)Screen.height;
                newWidth = Mathf.RoundToInt(newHeight * factor);
                if (newWidth % 2 != 0)
                    newWidth = Mathf.Max(newWidth - 1, 2);
                if (instance.mode == ResolutionMode.fixedHeight)
                    currentFixedHeight = fixedHeight;
                if (instance.mode == ResolutionMode.fixedMax)
                    currentFixedMax = fixedMax;
            }
            currentNonFadeResolution.Set(newWidth, newHeight);
        }
        // Create the new render texture
        //if(renderTexture != null)
        //    renderTexture.Release();
        renderTexture = new RenderTexture(newWidth, newHeight, 24, RenderTextureFormat.Default);
        // Apply blurriness, if enabled
        if(blurred || (fade && tempFadingBlur))
            renderTexture.filterMode = FilterMode.Bilinear;
        else
            renderTexture.filterMode = FilterMode.Point;
        // Set the render texture as the cameras target texture
        //if (cam.targetTexture != null)
        //    cam.targetTexture.Release();
        // Save current option values
        currentBlurred = blurred;
        instance.currentMode = instance.mode;
        currentTintColor = tintColor;
        // Apply the render texture to the canvas' raw image
        rawImage.texture = renderTexture;
        // Apply tint color
        rawImage.material.SetColor("_ColorTint", tintColor);
        // Set targetTexture of Camera
        for (int i = 0; i < cams.Length; i++)
        {
            if(cams[i] != null)
                cams[i].targetTexture = renderTexture;
        }
    }

    public static Vector2 GetMainGameViewSize()
    {
        #if UNITY_EDITOR
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
            return (Vector2)Res;
        #else
            return new Vector2(Screen.width, Screen.height);
        #endif
    }
}

//public class MyAssetModificationProcessor : UnityEditor.AssetModificationProcessor
//{
//    public static void OnWillSaveAssets(string[] paths)
//    {
//        Retronator.instance.savingScene = true;
//        Retronator.instance.enabled = false;
//    }
//}