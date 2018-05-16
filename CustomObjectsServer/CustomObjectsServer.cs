using System;
using Fougerite;
using RustBuster2016Server;
using UnityEngine;

namespace CustomObjectsServer
{
    public class CustomObjectsServer : Fougerite.Module
    {
        public static string AssetPath = "file:///";
        public GameObject MainHolder;
        public LoadingHandler Handler;
        
        public override string Name
        {
            get { return "CustomObjectsServer"; }
        }

        public override string Author
        {
            get { return "DreTaX"; }
        }

        public override string Description
        {
            get { return "CustomObjectsServer"; }
        }

        public override Version Version
        {
            get { return new Version("1.0"); }
        }

        public override void Initialize()
        {
            AssetPath = AssetPath + @Util.GetRootFolder() + "\\Save\\CustomObjectsServer\\myasset.unity3d";
            RustBuster2016Server.API.AddFileToDownload(new RBDownloadable("CustomObjects\\", Util.GetRootFolder() + "\\Save\\CustomObjectsServer\\myasset.unity3d"));
            
            MainHolder = new GameObject();
            Handler = MainHolder.AddComponent<LoadingHandler>();
            UnityEngine.Object.DontDestroyOnLoad(Handler);
            try
            {
                Handler.StartCoroutine(Handler.LoadAsset());
            }
            catch (Exception ex)
            {
                Logger.LogError("Couroutine failed. " + ex);
            }
        }

        public override void DeInitialize()
        {
            Caching.CleanCache();
        }
    }
}