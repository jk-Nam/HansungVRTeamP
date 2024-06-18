using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SoundMgr : MonoBehaviour
{
    // 싱글톤 인스턴스를 관리
    public static SoundMgr Instance { get; private set; } 

    // 효과음 폴더 경로
    [SerializeField] private string sfxFolder = "SFX"; 
    // 배경음 폴더 경로
    [SerializeField] private string bgmFolder = "BGM"; 

    // 효과음을 담을 리스트
    private List<AudioSource> sfx = new List<AudioSource>(); 
    // 배경 음악을 담을 리스트
    private List<AudioSource> bgms = new List<AudioSource>(); 
    // 현재 재생 중인 배경 음악
    private AudioSource curbgm; 

    private void Awake()
    {
        // 싱글톤 패턴을 사용하여 SoundManager 인스턴스를 관리
        if (Instance == null)
        {
            Instance = this;
            // 씬이 바뀌어도 SoundManager가 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject); 
            // 효과음 초기화
            Initializesfx(); 
            // 배경음 초기화
            Initializebgms(); 
        }
        else
        {
            // 이미 인스턴스가 존재하면 새로 생성된 객체를 파괴
            Destroy(gameObject); 
        }
    }

    // 효과음을 초기화하는 함수
    private void Initializesfx()
    {
        // 주어진 폴더 경로에서 오디오 클립을 로드
        var sfxFiles = Resources.LoadAll<AudioClip>(sfxFolder);
        foreach (var clip in sfxFiles)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            // 리스트에 추가
            sfx.Add(audioSource); 
        }
    }

    // 배경음을 초기화하는 함수
    private void Initializebgms()
    {
        // 주어진 폴더 경로에서 오디오 클립을 로드
        var bgmFiles = Resources.LoadAll<AudioClip>(bgmFolder);
        foreach (var clip in bgmFiles)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            // 배경음은 반복 재생
            audioSource.loop = true; 
            // 리스트에 추가
            bgms.Add(audioSource); 
        }
    }

    // 효과음을 재생하는 함수 (지속시간 포함)
    public void PlaySound(int index, float duration = 0.5f)
    {
        if (index >= 0 && index < sfx.Count)
        {
            // 지속 시간이 0.5초 이하이면 0.5초로 설정
            if (duration <= 0.5f) duration = 0.5f; 

            if (duration > 0.5f)
            {
                StartCoroutine(PlaySoundRepeatedlyCoroutine(sfx[index], duration));
            }
            else
            {
                // 단일 재생
                sfx[index].Play(); 
            }
        }
        else
        {
            // 유효하지 않은 인덱스 경고
            Debug.LogWarning("효과음 인덱스가 범위를 벗어났습니다!"); 
        }
    }

    // 반복 재생을 관리하는 코루틴
    private IEnumerator PlaySoundRepeatedlyCoroutine(AudioSource audioSource, float duration)
    {
        // 반복 재생을 종료할 시간 계산
        float endTime = Time.time + duration; 
        while (Time.time < endTime)
        {
            // 효과음 재생
            audioSource.Play(); 
            // 효과음의 길이만큼 대기
            yield return new WaitForSeconds(audioSource.clip.length); 
        }
    }

    // 모든 효과음의 볼륨을 설정하는 함수
    public void SetVolume(float volume)
    {
        foreach (AudioSource audioSource in sfx)
        {
            // 모든 효과음의 볼륨을 설정
            audioSource.volume = volume; 
        }
    }

    // 배경 음악을 선택하여 재생하는 함수
    public void Playbgm(int index)
    {
        if (index >= 0 && index < bgms.Count)
        {
            if (curbgm != null && curbgm.isPlaying)
            {
                // 현재 재생 중인 배경 음악을 멈춤
                curbgm.Stop(); 
            }

            curbgm = bgms[index];
            // 선택된 배경 음악을 재생
            curbgm.Play(); 
        }
        else
        {
            // 유효하지 않은 인덱스 경고
            Debug.LogWarning("배경 음악 인덱스가 범위를 벗어났습니다!"); 
        }
    }

    // 배경 음악을 멈추는 함수
    public void Stopbgm()
    {
        if (curbgm != null)
        {
            // 현재 재생 중인 배경 음악 멈춤
            curbgm.Stop(); 
        }
    }

    // 배경 음악의 볼륨을 설정하는 함수
    public void SetbgmVolume(float volume)
    {
        if (curbgm != null)
        {
            // 현재 재생 중인 배경 음악의 볼륨을 설정
            curbgm.volume = volume; 
        }
    }
}