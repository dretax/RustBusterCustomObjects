using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using RustBuster2016.API;
using UnityEngine;

namespace CustomObjects
{
    public class CustomObjects : RustBusterPlugin
    {
        public static string AssetPath = "file://";
        public GameObject MainHolder;
        public LoadingHandler Handler;
        
        public override string Name
        {
            get { return "CustomObjects"; }
        }

        public override string Author
        {
            get { return "DreTaX"; }
        }

        public override Version Version
        {
            get { return new Version("1.1"); }
        }

        public override void Initialize()
        {
            try
            {
                AssetPath = AssetPath + @RustBuster2016.API.Hooks.GameDirectory + "\\RB_Data\\CustomObjects\\myasset.unity3d";
                MainHolder = new GameObject();
                Handler = MainHolder.AddComponent<LoadingHandler>();
                RustBuster2016.API.Hooks.LogData("CustomObjects", "Added component, we are loading object.");
                UnityEngine.Object.DontDestroyOnLoad(Handler);
                try
                {
                    Handler.StartCoroutine(Handler.LoadAsset());
                }
                catch (Exception ex)
                {
                    RustBuster2016.API.Hooks.LogData("CustomObjects", "Loaded. Calling Loadasset.ex " + ex);
                }

                RustBuster2016.API.Hooks.LogData("CustomObjects", "Loaded. Calling Loadasset.");
            }
            catch (Exception ex)
            {
                RustBuster2016.API.Hooks.LogData("CustomObjects", "Exception1 : " + ex);
            }
        }

        public override void DeInitialize()
        {
            Caching.CleanCache();
        }
    }
}