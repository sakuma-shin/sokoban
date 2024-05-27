using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    

    public GameObject playerPreFab;
    public GameObject boxPrefab;
    public GameObject clearText;
    int[,] map;

    GameObject[,] field; //�Q�[���Ǘ��p�̔z��

    GameObject obj;


    // Start is called before the first frame update

    void Start()
    {
        Screen.SetResolution(1280, 720, false);

        map = new int[,]{
        { 0, 0, 0, 0, 0 },
        { 1, 2, 2, 0, 0, },
        { 0, 0, 3, 0, 0, }
        };

        //�t�B�[���h�̃T�C�Y����
        field = new GameObject[map.GetLength(0), map.GetLength(1)];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                    playerPreFab,
                    new Vector3(x, map.GetLength(0) - 1.0f - y, 0.0f),
                    Quaternion.identity
                    );
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }

            }
        }
        string debugText = "";

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";
        }



        // PrintArray();
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
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(1, 0));

            //�����N���A���Ă�����
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(-1, 0));

            //�����N���A���Ă�����
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(0, -1));

            //�����N���A���Ă�����
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(0, 1));

            //�����N���A���Ă�����
            if (IsCleard())
            {
               clearText.SetActive(true);
            }
        }
        //1.����������L�q
        //������Ȃ������Ƃ��ׂ̈�-1�ŏ�����
        //int playerIndex = GetPlayerIndex();
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

        //MoveNumber(1, playerIndex, playerIndex + 1);
        //PrintArray();


    }




    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null)
                {
                    continue;
                }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
            return false;
        }

        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            /*�v���C���[�̈ړ��悩��A����ɐ��2(��)���ړ�������
             ���̈ړ�����,MoveNumber���\�b�h����MoveNumber���\�b�h���Ă�
            �������ċN���Ă���B�ړ��s��bool�ŋL�^*/
            bool success = MoveNumber(moveTo, moveTo + velocity);
            //���������ړ����s������A�v���C���[�̈ړ������s
            if (!success)
            {
                return false;
            }
        }
        //    map[moveTo] = number;
        //    map[moveFrom] = 0;
        //    return true;
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        //field[moveFrom.y, moveFrom.x].transform.position =
        //new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);

        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);

        field[moveFrom.y, moveFrom.x] = null;

        return true;

    }

    bool IsCleard()
    {
        //vector2Int�^�̉ϒ��z��̍쐬
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //�i�[�ꏊ���ۂ��𔻒f
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        //�v�f����goals.Count�Ŏ擾
        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                //��ł���������������������B��
                return false;
            }
        }
        return true;
    }
}
