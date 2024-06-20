using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 네임스페이스 추가

public class SoundMgr : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static SoundMgr instance;

    // 배경음 오디오 소스
    public AudioSource bgmSource;

    // 효과음 오디오 소스 목록
    public List<AudioSource> sfxSources = new List<AudioSource>();

    // 배경음 클립 목록
    public List<AudioClip> bgmClips;

    // 효과음 클립 목록
    public List<AudioClip> sfxClips;

    // UI 슬라이더
    public Slider bgmSlider; // 배경음 볼륨 슬라이더
    public Slider sfxSlider; // 효과음 볼륨 슬라이더

    private int playLoop = 1; // 효과음 반복 인수
    private int lastBGMIndex = -1; // 마지막으로 재생된 배경음의 인덱스

    void Awake()
    {
        // 인스턴스 할당 및 중복 방지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 사운드 로드 및 초기 설정
        LoadSounds();
        if (bgmClips.Count > 0)
        {
            PlayBGM(0); // 첫 번째 배경음 재생
            Debug.Log("첫 번째 배경음이 자동 재생되었습니다.");
        }
        else
        {
            Debug.LogWarning("배경음 클립이 존재하지 않습니다.");
        }
        SetBGMVolume(0.2f);
        SetSFXVolume(1.0f);

        // 슬라이더 초기화 및 이벤트 연결
        if (bgmSlider != null)
        {
            bgmSlider.value = bgmSource.volume;
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }
        if (sfxSlider != null)
        {
            // 모든 sfxSources의 볼륨이 동일하다고 가정하고 첫 번째 소스의 볼륨을 사용
            sfxSlider.value = sfxSources.Count > 0 ? sfxSources[0].volume : 1.0f;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

    }

    // 배경음 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
        }
        else
        {
            Debug.LogWarning("bgmSource가 할당되지 않았습니다.");
        }
    }

    // 효과음 볼륨 설정
    public void SetSFXVolume(float volume)
    {
        foreach (var source in sfxSources)
        {
            source.volume = volume;
        }
    }

    // 배경음 재생
    public void PlayBGM(int bgmIndex)
    {
        if (bgmSource == null)
        {
            Debug.LogWarning("bgmSource가 할당되지 않았습니다.");
            return;
        }

        if (bgmIndex >= 0 && bgmIndex < bgmClips.Count)
        {
            AudioClip clip = bgmClips[bgmIndex];
            if (clip != null)
            {
                bgmSource.clip = clip;
                bgmSource.loop = true; // 반복 재생 설정
                bgmSource.Play();
                lastBGMIndex = bgmIndex; // 마지막 재생된 배경음 인덱스 저장
            }
        }
        else
        {
            Debug.LogWarning("BGM 인덱스가 범위를 벗어났습니다: " + bgmIndex);
        }
    }

    // 효과음 반복 횟수 설정
    public void PlayLoop(int repeatCount)
    {
        playLoop = repeatCount;
    }

    // 효과음 1번 재생
    public void PlaySFXone(int sfxIndex)
    {
        PlaySFX(sfxIndex, playLoop);
    }

    // 효과음 반복 재생
    public void PlaySFX(int sfxIndex, int repeatCount = 1, float delayTime = 0)
    {
        if (sfxIndex >= 0 && sfxIndex < sfxClips.Count)
        {
            AudioClip clip = sfxClips[sfxIndex];
            if (clip != null)
            {
                StartCoroutine(PlaySFXCoroutine(clip, repeatCount, delayTime));
            }
        }
        else
        {
            Debug.LogWarning("SFX 인덱스가 범위를 벗어났습니다: " + sfxIndex);
        }
    }

    // 효과음 재생을 위한 코루틴
    private IEnumerator PlaySFXCoroutine(AudioClip clip, int repeatCount, float delayTime)
    {
        playLoop = 1;
        for (int i = 0; i < repeatCount; i++)
        {
            AudioSource source = GetAvailableAudioSource();
            source.PlayOneShot(clip);

            StartCoroutine(VibrateMgr.instance.VibrateWithSFX(source));// 추가부분

            // 재생이 끝난 뒤 delayTime 후 재생
            yield return new WaitForSeconds(clip.length + delayTime);

        }
    }

    // 사용 가능한 오디오 소스 가져오기
    private AudioSource GetAvailableAudioSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        // 사용 가능한 소스가 없을 경우 새로운 오디오 소스 생성
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        sfxSources.Add(newSource);
        return newSource;
    }

    // 사운드 로드
    public void LoadSounds()
    {
        bgmClips.Clear();
        sfxClips.Clear();

        // Resources 폴더에서 모든 오디오 클립 로드
        AudioClip[] bgms = Resources.LoadAll<AudioClip>("BGM");
        bgmClips.AddRange(bgms);

        AudioClip[] sfxs = Resources.LoadAll<AudioClip>("SFX");
        sfxClips.AddRange(sfxs);
    }

    // 배경음 정지
    public void StopBGM()
    {
        if (bgmSource != null)
        {
            if (bgmSource.isPlaying)
            {
                bgmSource.Stop();
                Debug.Log("배경음이 정지되었습니다.");
            }
            else if (lastBGMIndex >= 0)
            {
                PlayBGM(lastBGMIndex); // 마지막 재생된 배경음을 다시 재생
                Debug.Log("정지된 배경음이 다시 재생되었습니다.");
            }
        }
        else
        {
            Debug.LogWarning("bgmSource가 할당되지 않았습니다.");
        }
    }
}
