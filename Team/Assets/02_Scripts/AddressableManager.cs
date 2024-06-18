using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    public static AddressableManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    AsyncOperationHandle handle;

    public Transform bombPos;

    public AssetReference bombPrefab;
    public AssetReference wirePrefab;

    public string LableForBundleDown;

    IEnumerator Start()
    {
        Addressables.ClearDependencyCacheAsync("Bomb");
        StartCoroutine(CheckDownLoadFileSize());
        yield return CheckDownLoadFileSize();


    }

    IEnumerator CheckDownLoadFileSize()
    {
        Debug.Log("CheckDownLoadFileSize!!!");

        AsyncOperationHandle<long> getdownloadSize = Addressables.GetDownloadSizeAsync("Bomb");
        yield return getdownloadSize;

        Debug.Log(getdownloadSize.Result + "bytes");

        if (getdownloadSize.Result == 0)
        {
            Debug.Log("There is no Patch File...");

        }
        else
        {
            Debug.Log("Patch File Found : " + getdownloadSize.Result + "bytes");
            DownloadAsset();
        }
    }

    public void DownloadAsset()
    {
        Addressables.DownloadDependenciesAsync(LableForBundleDown).Completed += (AsyncOperationHandle handle) =>
        {
            Debug.Log("다운로드 완료");
            Addressables.Release(handle);

        };
    }

    public void LoadAsset(object key)
    {
        try
        {
            Addressables.LoadAssetAsync<GameObject>(key).Completed += (op) =>
            {
                if (((AsyncOperationHandle)op).Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("에셋 로드 완료");
                    return;
                }
            };
        }
        catch(Exception e) { Debug.LogError(e.Message); }
    }


    public void CreateBomb()
    {
        List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();

        AsyncOperationHandle<GameObject> handle = bombPrefab.InstantiateAsync();
        
        handles.Add(handle);

        handle.Completed += (AsyncOperationHandle<GameObject> completeHandle) =>
        {
            GameObject bomb = completeHandle.Result;
            if (bomb != null)
            {
                bomb.transform.position = bombPos.transform.position;
            }
        };
    }
}
