using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VibrateMgr : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static VibrateMgr instance;

    // 진동의 최대 크기
    public float maxVibrationAmplitude = 1.0f;

    // 진동 기능 활성화 여부
    private bool isVibrationEnabled = true;

    // 싱글톤 인스턴스를 설정하는 Awake 메서드
    void Awake()
    {
        // 인스턴스가 아직 설정되지 않았다면
        if (instance == null)
        {
            // 현재 인스턴스를 설정하고 씬 전환 시 파괴되지 않도록 설정
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 인스턴스가 존재하면 현재 게임 오브젝트 파괴
            Destroy(gameObject);
        }
    }

    // 지정된 AudioSource의 RMS 값을 계산하는 함수
    private float CalculateRMS(AudioSource audioSource)
    {
        // 오디오 샘플 데이터를 저장할 배열
        float[] samples = new float[256];

        // 오디오 소스의 샘플 데이터를 가져옴
        audioSource.GetOutputData(samples, 0);

        // 샘플들의 제곱합 계산
        float sum = 0;
        foreach (var sample in samples)
        {
            sum += sample * sample;
        }

        // RMS 값 계산
        return Mathf.Sqrt(sum / samples.Length);
    }

    // 효과음 재생 중 진동을 연동하는 코루틴
    public IEnumerator VibrateWithSFX(AudioSource audioSource)
    {
        // 오디오 소스가 재생 중인 동안 반복
        while (audioSource.isPlaying)
        {
            // 오디오 소스의 RMS 값을 계산
            float rms = CalculateRMS(audioSource);

            // 진동 강도 계산 (2배 증폭)
            float vibrationStrength = Mathf.Clamp((rms / maxVibrationAmplitude) * 2, 0, 1);

            // 진동 기능이 활성화된 경우에만 진동 설정
            if (isVibrationEnabled)
            {
                // 왼손 컨트롤러 진동 설정
                SetVibration(vibrationStrength, XRNode.LeftHand);

                // 오른손 컨트롤러 진동 설정
                SetVibration(vibrationStrength, XRNode.RightHand);
            }

            // 0.1초 대기 후 다음 업데이트
            yield return new WaitForSeconds(0.1f);
        }

        // 효과음이 끝나면 왼손 진동 멈춤
        SetVibration(0, XRNode.LeftHand);

        // 효과음이 끝나면 오른손 진동 멈춤
        SetVibration(0, XRNode.RightHand);
    }

    // 진동을 설정하는 함수
    private void SetVibration(float strength, XRNode node)
    {
        // 지정된 노드의 장치를 가져옴
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);

        // 장치가 유효한지 확인
        if (device.isValid)
        {
            // 햅틱 기능을 지원하는지 확인
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
            {
                // 진동 신호를 보낼 채널 번호
                uint channel = 0;

                // 진동 신호를 보냄
                device.SendHapticImpulse(channel, strength, 0.1f);
            }
        }
    }

    // 진동 기능 스위치
    public void VibOnOff()
    {
        isVibrationEnabled = !isVibrationEnabled;
    }
}
