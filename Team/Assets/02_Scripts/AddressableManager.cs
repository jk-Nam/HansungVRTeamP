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

    AsyncOperationHandle<long> bombHandle;
    AsyncOperationHandle<long> MatHandle;
    AsyncOperationHandle<long> wireHandle;
    AsyncOperationHandle<long> brokenWireHandle;
    AsyncOperationHandle<long> shaderHandle;

    public Transform bombPos;

    public AssetReference bombPrefab;
    public AssetReference wirePrefab;

    public string LableForBundleDown;

    private List<string> matKeys = new List<string>
    {
        "Assets/08_Materials/Red.mat",
        "Assets/08_Materials/Blue.mat",
        "Assets/08_Materials/Gray.mat",
        "Assets/08_Materials/White.mat",
        "Assets/08_Materials/Yellow.mat",
        "Assets/08_Materials/Black.mat"
    };

    void Start()
    {
        Addressables.ClearDependencyCacheAsync("Bomb");
        Addressables.ClearDependencyCacheAsync("Mat");
        Addressables.ClearDependencyCacheAsync("Wire");
        Addressables.ClearDependencyCacheAsync("Shader");

        StartCoroutine(CheckDownLoadFileSize());

        //CreateBomb();
    }

    private void OnClearCacheCompleted(AsyncOperationHandle<IList<object>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Cache cleared successfully for label: " + handle.Result);
        }
        else
        {
            Debug.LogError("Failed to clear cache for label: " + handle.Result);
        }

        // 핸들 해제
        Addressables.Release(handle);
    }

    IEnumerator CheckDownLoadFileSize()
    {
        Debug.Log("CheckDownLoadFileSize!!!");

        bombHandle = Addressables.GetDownloadSizeAsync("Bomb");
        yield return bombHandle;
        MatHandle = Addressables.GetDownloadSizeAsync("Mat");
        yield return MatHandle;
        wireHandle = Addressables.GetDownloadSizeAsync("Wire");
        yield return wireHandle;
        brokenWireHandle = Addressables.GetDownloadSizeAsync("BrokenWire");
        yield return brokenWireHandle;
        shaderHandle = Addressables.GetDownloadSizeAsync("Shader");
        yield return shaderHandle;

        Debug.Log(MatHandle.Result + " bytes");

        if (bombHandle.Result == 0 && MatHandle.Result == 0 && wireHandle.Result == 0 & shaderHandle.Result == 0)
        {
            Debug.Log("There is no Patch File...");
            //패치가 없다면 에셋 로드
            LoadAsset("Bomb");
            foreach (var key in matKeys)
            {
                LoadAsset(key);
            }
            LoadAsset("Wire");
            LoadAsset("BrokenWire");
            LoadAsset("Assets/SimpleToon/Shader/ToonLightBase.shader");
        }
        else
        {
            Debug.Log("Patch File Found: " + MatHandle.Result + " bytes");
            DownloadAsset();
        }
    }

    public void DownloadAsset()
    {
        Addressables.DownloadDependenciesAsync(LableForBundleDown).Completed += (AsyncOperationHandle handle) =>
        {
            Debug.Log("다운로드 완료");
            Addressables.Release(handle);
            // 다운로드 완료 후 에셋 로드
            LoadAsset("Shader");
            LoadAsset("Bomb");
            foreach (var key in matKeys)
            {
                LoadAsset(key);
            }
            LoadAsset("Wire");
            LoadAsset("BrokenWire");
        };
    }

    public void LoadAsset(object key)
    {
        try
        {
            if (key.ToString().EndsWith(".shader"))
            {
                Addressables.LoadAssetAsync<Shader>(key).Completed += (op) =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        Shader shader = op.Result;
                        if (shader != null)
                        {
                            Debug.Log(key + ": 에셋 로드 완료, Shader: " + shader.name);
                            //Renderer renderer = GetComponent<Renderer>();
                            //if (renderer != null)
                            //{
                            //    renderer.material.shader = shader;
                            //}
                        }
                        else
                        {
                            Debug.LogError(key + ": 로드된 Shader null입니다.");
                        }
                    }
                    else
                    {
                        Debug.LogError(key + ": 에셋 로드 실패");
                    }
                };
            }
            else if (key.ToString().EndsWith(".mat")) //메테리얼 에셋 처리
            {
                Addressables.LoadAssetAsync<Material>(key).Completed += (op) =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        Material material = op.Result;
                        if (material != null)
                        {
                            Debug.Log(key + ": 에셋 로드 완료, Material: " + material.name);
                        }
                        else
                        {
                            Debug.LogError(key + ": 로드된 Material이 null입니다.");
                        }
                    }
                    else
                    {
                        Debug.LogError(key + ": 에셋 로드 실패");
                    }
                };
            }
            else // 메테리얼 이외의 에셋 처리
            {
                Addressables.LoadAssetAsync<GameObject>(key).Completed += (op) =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        GameObject loadedAsset = op.Result;
                        if (loadedAsset != null)
                        {
                            Debug.Log(key + ": 에셋 로드 완료, GameObject: " + loadedAsset.name);
                        }
                        else
                        {
                            Debug.LogError(key + ": 로드된 GameObject가 null입니다.");
                        }
                    }
                };
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }


    //폭탄 생성 *게임 매니저로 옮길 가능성 있음
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
                Debug.Log("폭탄 생성 완료: " + bomb.name);
            }
            else
            {
                Debug.LogError("폭탄 생성 실패: 로드된 GameObject가 null입니다.");
            }
        };
    }
}
