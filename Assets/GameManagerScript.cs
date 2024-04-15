using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    int[] map = { 0, 0, 1, 0, 2, 0, 2,0 };
    
    // Start is called before the first frame update

    void Start()
    {
        string debugText = "";

        PrintArray();
        //�f�o�b�O���O�̏o��
        //Debug.Log("Hello world\n");

        //����������������o��
        Debug.Log(debugText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //1.����������L�q
            //������Ȃ������Ƃ��ׂ̈�-1�ŏ�����
            int playerIndex = GetPlayerIndex();
            ////�v�f����map.Length�Ŏ擾
            //for(int i = 0;i < map.Length;i++) {
            //    if (map[i]==1){
            //        playerIndex = i;
            //        break;
            //    }
            //}
            //for(int i = 0;i<map.Length ; i++) {
            //    if (map[i] == 2) {
            //        playerIndex = i;
            //        break;  
            //    }
            //}

            //for (int i = 0;i<map.Length;i++){
            //    if (map[i] == 3) { 
            //    playerIndex=i;
            //        break;
            //    }
            //}

            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();

        }
    }

    void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }

        if (map[moveTo]==2) {
            //�ǂ̕����ֈړ����邩���Z�o
            int velocity = moveTo - moveFrom;
            /*�v���C���[�̈ړ��悩��A����ɐ��2(��)���ړ�������
             ���̈ړ�����,MoveNumber���\�b�h����MoveNumber���\�b�h���Ă�
            �������ċN���Ă���B�ړ��s��bool�ŋL�^*/
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //���������ړ����s������A�v���C���[�̈ړ������s
            if(!success){
                return false;
            }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
}
