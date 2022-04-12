using UnityEngine;
public class SunshineNativeCameraHandler : MonoBehaviour
{

    private const string PACKAGE_NAME = "com.SmileSoft.unityplugin";



    private const string _CALLBACK_IMAGE_CAPTURE = "OnTakeIngPictureCallback";
    private const string _CALLBACK_RECORD_VIDEO = "OnTakingVideoCallback";




    private const string File_FRAGMENT_CLASS_NAME = ".MainActivity";
    private const string FOLDER_CREATE_METHOD_NAME = "CreateFolder";


    private const string FileProviderName = "com.sungkyul.libroom";


    public delegate void OnTakePictureCallbackHandler(bool success, string path);
    private OnTakePictureCallbackHandler _callBackCamera_Image;

    public delegate void OnTakeVideoCallbackHandler(bool success, string path);
    private OnTakeVideoCallbackHandler _callBackCamera_Video;


    private AndroidJavaObject _nativeCamera;
	private AndroidJavaObject _nativeFile;


	private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        _nativeCamera = new AndroidJavaObject("com.SmileSoft.unityplugin.Camera.CameraFragment");
		_nativeFile = new AndroidJavaObject("com.SmileSoft.unityplugin.FileUtils");
		//SetUp            (String providerName, String gameObject, String imageCapturecallback,String videoCapturecallback)  : Jave Function
		_nativeCamera.Call("SetUp", FileProviderName, gameObject.name, _CALLBACK_IMAGE_CAPTURE, _CALLBACK_RECORD_VIDEO);
    }



    public void TakePicture(string filename, OnTakePictureCallbackHandler callback)
    {
        _callBackCamera_Image = callback;
        _nativeCamera.Call("CapturePictureFromCamera", filename);
    }

    public void TakeVideo(string filename, OnTakeVideoCallbackHandler callback)
    {
        _callBackCamera_Video = callback;
        _nativeCamera.Call("RecordVideoFromCamera", filename);
    }



    public void OnTakeIngPictureCallback(string result)
    {
#if UNITY_ANDROID
        if (_callBackCamera_Image != null)
        {
            _callBackCamera_Image.Invoke(!string.IsNullOrEmpty(result), result);
            _callBackCamera_Image = null;
        }
#endif
    }


    public void OnTakingVideoCallback(string result)
    {
#if UNITY_ANDROID
        if (_callBackCamera_Video != null)
        {
            _callBackCamera_Video.Invoke(!string.IsNullOrEmpty(result), result);
            _callBackCamera_Video = null;
        }
#endif
    }

    public string CreateFolderInAndroid(string foldername)
    {
		return _nativeFile.Call<string>("CreateFolder", foldername);
    }

}