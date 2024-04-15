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
            int playerIndex = GetPlayerIndex();
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
            //どの方向へ移動するかを算出
            int velocity = moveTo - moveFrom;
            /*プレイヤーの移動先から、さらに先へ2(箱)を移動させる
             箱の移動処理,MoveNumberメソッド内でMoveNumberメソッドを呼び
            処理が再起している。移動可不可をboolで記録*/
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //もし箱が移動失敗したら、プレイヤーの移動も失敗
            if(!success){
                return false;
            }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
}
