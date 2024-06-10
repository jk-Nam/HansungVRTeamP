
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeModule : BombModule
{
    public GameObject player; // �÷��̾� ������Ʈ
    public GameObject Goal;

    public float moveAmount = 0.1f; // �̵���

    // UI ��ư�� ������ ����
    public UnityEngine.UI.Button upButton;
    public UnityEngine.UI.Button downButton;
    public UnityEngine.UI.Button leftButton;
    public UnityEngine.UI.Button rightButton;

    private Vector3 initialPosition;

    void Start()
    {
        // �÷��̾��� �ʱ� ��ġ ����
        initialPosition = player.transform.position;

        // UI ��ư Ŭ�� �̺�Ʈ ������ �߰�
        upButton.onClick.AddListener(MoveUp);
        downButton.onClick.AddListener(MoveDown);
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);

        InitiallizeModule();
    }

    public override void InitiallizeModule()
    {
        // ��� �ʱ�ȭ �ڵ� (�ʿ信 ���� �߰�)
        moduleType = BombMoudleType.Maze;
    }

    public override void DefuseModule()
    {
        isDefused = true;
        // ��� ���� �� �ʿ��� �߰� ����
    }

    //public override void Fail()
    //{
    //   // incorrectCnt++;
    //    // ���� �� �ʿ��� �߰� ����
    //}

    // ��ư �Է¿� ���� �̵� �Լ�
    void MoveUp()
    {
        player.transform.Translate(Vector3.up * moveAmount);
    }

    void MoveDown()
    {
        player.transform.Translate(Vector3.down * moveAmount);
    }

    void MoveLeft()
    {
        player.transform.Translate(Vector3.left * moveAmount);
    }

    void MoveRight()
    {
        player.transform.Translate(Vector3.right * moveAmount);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MazeWall"))
        {
            // �÷��̾��� ��ġ�� �ʱ� ��ġ�� ����
            Debug.Log("����");
            player.transform.position = initialPosition;
         //   Fail();


        }
        else if (other.gameObject.CompareTag("MazeGoal"))
        {
            Debug.Log("Ŭ����!");
            DefuseModule();
        }



    }
}