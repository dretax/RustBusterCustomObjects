﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Fougerite;

namespace CustomObjectsServer
{
    public class LoadingHandler : MonoBehaviour
    {
        public static AssetBundle bundle;

        public IEnumerator LoadAsset()
        {
            WWW www = WWW.LoadFromCacheOrDownload(CustomObjectsServer.AssetPath, 1);
            yield return www;
            if (www.error != null)
            {
                Logger.LogError("Error loading asset. " + www.error);
                //return;
            }
            bundle = www.assetBundle;
            www.Dispose();
            
            GameObject ourobject = new GameObject();
            LoadObjectFromBundle handler = ourobject.AddComponent<LoadObjectFromBundle>();
            handler.Create("personal_transport_helicopter", new Vector3(6592f, 380f, -2653f), new Quaternion(0, 0, 0, 0), new Vector3(1, 1, 1));
            UnityEngine.Object.DontDestroyOnLoad(ourobject);
            
            ourobject = new GameObject();
            handler = ourobject.AddComponent<LoadObjectFromBundle>();
            handler.Create("militarycar", new Vector3(6592f, 380f, -2653f), new Quaternion(0, 0, 0, 0), new Vector3(1, 1, 1));
            UnityEngine.Object.DontDestroyOnLoad(ourobject);
            
            bundle.Unload(false);
            Logger.Log("Objects loaded.");
        }
        
        public class LoadObjectFromBundle : MonoBehaviour
        {
            private GameObject _ObjectInstantiate;
            private string _name;
            private Vector3 _pos;
            private Quaternion _rot;
            private Vector3 _siz;

            public void Create(string name, Vector3 pos, Quaternion rot, Vector3 siz)
            {
                _name = name;
                _pos = pos;
                _rot = rot;
                _siz = siz;
                try
                {
                    _ObjectInstantiate = bundle.Load(_name, typeof(GameObject)) as GameObject;
                    _ObjectInstantiate.transform.localScale = new Vector3(_siz.x, _siz.y, _siz.z);
                    _ObjectInstantiate = (GameObject) Instantiate(_ObjectInstantiate, _pos, _rot);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error spawning asset: " + ex);
                }
            }
            
            public GameObject ObjectInstantiate
            {
                get { return _ObjectInstantiate; }
            }

            public string Name
            {
                get { return _name; }
            }

            public Vector3 Position
            {
                get { return _pos; }
            }

            public Quaternion Rotation
            {
                get { return _rot; }
            }

            public Vector3 Size
            {
                get { return _siz; }
            }
        }
    }
}