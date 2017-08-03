using System;
using System.IO;
using Android.Content;
using Android.Hardware;
using Android.Views;

namespace DualCameraTest
{
    public class BackCameraPreview : SurfaceView, ISurfaceHolderCallback
    {
        public static string TAG = "BackCameraPreview";

        ISurfaceHolder mHolder;
        Camera mCamera;

        public BackCameraPreview(Context context, Camera camera) : base(context)
        {
            mCamera = camera;

            // Install a SurfaceHolder.Callback so we get notified when the
            // underlying surface is created and destroyed.
            mHolder = Holder;
            mHolder.AddCallback(this);
            mHolder.SetType(SurfaceType.PushBuffers);
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            // The Surface has been created, acquire the camera and tell it where
            // to draw.
            try
            {
                if (mCamera != null)
                {
                    mCamera.SetPreviewDisplay(holder);
                    mCamera.StartPreview();
                }
            }
            catch (Java.IO.IOException exception)
            {
                //Log.Error(TAG, "IOException caused by setPreviewDisplay()", exception);
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {

        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int w, int h)
        {
            if (Holder == null)
            {
                // preview surface does not exist
                return;
            }

            // stop preview before making changes
            try
            {
                mCamera.StopPreview();
            }
            catch (Exception e)
            {
                // ignore: tried to stop a non-existent preview
            }

            // set preview size and make any resize, rotate or
            // reformatting changes here

            // start preview with new settings
            try
            {
                mCamera.SetPreviewDisplay(mHolder);
                mCamera.StartPreview();

            }
            catch (Exception e)
            {
                //Log.d(TAG, "Error starting camera preview: " + e.getMessage());
            }
        }
    }
}
