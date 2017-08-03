using Android.App;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Views;
using System.Collections.Generic;
using Android.Content;
using System;
using Android.Util;

namespace DualCameraTest
{
    [Activity(Label = "DualCameraTest", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private Camera mBackCamera;
        private Camera mFrontCamera;
        private BackCameraPreview mBackCamPreview;
        private FrontCameraPreview mFrontCamPreview;

        public static String TAG = "DualCamActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Dual_Cam);

            Log.Info(TAG, "Number of cameras: " + Camera.NumberOfCameras);

            // Create an instance of Camera
            mBackCamera = getCameraInstance(0);
            // Create back camera Preview view and set it as the content of our activity.
            mBackCamPreview = new BackCameraPreview(this, mBackCamera);
            FrameLayout backPreview = (FrameLayout)FindViewById(Resource.Id.back_camera_preview);
            backPreview.AddView(mBackCamPreview);

            mFrontCamera = getCameraInstance(1);
            mFrontCamPreview = new FrontCameraPreview(this, mFrontCamera);
            FrameLayout frontPreview = (FrameLayout)FindViewById(Resource.Id.front_camera_preview);
            frontPreview.AddView(mFrontCamPreview);


        }
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.camera_menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public static Camera getCameraInstance(int cameraId)
        {
            Camera c = null;
            try
            {
                c = Camera.Open(cameraId); // attempt to get a Camera instance
            }
            catch (Exception e)
            {
                // Camera is not available (in use or does not exist)
                Log.Error(TAG, "Camera " + cameraId + " not available! " + e);
            }
            return c; // returns null if camera is unavailable
        }
    }
}

