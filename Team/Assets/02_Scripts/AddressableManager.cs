using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    public AssetReference wirePrefab;

    public void DownloadAsset(object key)
    {
        Addressables.GetDownloadSizeAsync(key).Completed += (opSize) =>
        {
            if (opSize.Status == AsyncOperationStatus.Succeeded && opSize.Result > 0)
            {
                Addressables.DownloadDependenciesAsync(key, true).Completed += (opDownload) =>
                {
                    if (((AsyncOperationHandle)opDownload).Status != AsyncOperationStatus.Succeeded)
                    {
                        return;
                    }
                };
            }
            else
            {
                //다운로드 받을 자료 없음
                Debug.Log("다운로드할 파일이 없습니다.");
            }
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

    public void CreatePrefab()
    {
        List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();

        AsyncOperationHandle<GameObject> handle = wirePrefab.InstantiateAsync();
        handles.Add(handle);
    }

}
