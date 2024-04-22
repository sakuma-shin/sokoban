using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject playerPreFab;
    int[,] map;

    GameObject[,] field; //ゲーム管理用の配列

    GameObject obj;
    

    // Start is called before the first frame update

    void Start()
    {
        map = new int[,]{
        { 0, 0, 1, 0, 0 },
        { 0, 0, 0, 0, 0, },
        { 0, 0, 0, 0, 0, }
        };

        //フィールドのサイズ決定
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
        //デバッグログの出力
        //Debug.Log("Hello world\n");

        //結合した文字列を出力
        Debug.Log(debugText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //1.をここから記述
            //見つからなかったときの為に-1で初期化
            //int playerIndex = GetPlayerIndex();
            ////要素数はmap.Lengthで取得
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
    }


    //    void PrintArray()
    //    {
    //        string debugText = "";
    //        for (int i = 0; i < map.Length; i++)
    //        {
    //            debugText += map[i].ToString() + ",";
    //        }
    //        Debug.Log(debugText);
    //    }

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
                if (obj.tag=="player")
                {
                    return new Vector2Int(x,y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    //    bool MoveNumber(int number, int moveFrom, int moveTo)
    //    {
    //        if (moveTo < 0 || moveTo >= map.Length)
    //        {
    //            return false;
    //        }

    //        if (map[moveTo]==2) {
    //            //どの方向へ移動するかを算出
    //            int velocity = moveTo - moveFrom;
    //            /*プレイヤーの移動先から、さらに先へ2(箱)を移動させる
    //             箱の移動処理,MoveNumberメソッド内でMoveNumberメソッドを呼び
    //            処理が再起している。移動可不可をboolで記録*/
    //            bool success = MoveNumber(2, moveTo, moveTo + velocity);
    //            //もし箱が移動失敗したら、プレイヤーの移動も失敗
    //            if(!success){
    //                return false;
    //            }
    //        }
    //        map[moveTo] = number;
    //        map[moveFrom] = 0;
    //        return true;
    //    }
    //}
}