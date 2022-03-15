using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    // 딕셔너리에 객체 아이디, 대사 저장
    Dictionary<int, string[]> chatData;

    private void Awake()
    {
        chatData = new Dictionary<int, string[]>();
        GenerateData();
    }
    
    void GenerateData()
    {
        chatData.Add(10, new string[] { "*  ( 폐허의 그림자가 어렴풋이\n    솟아오르니, 당신의 의지도\n    솟아오른다. )",
            "*  ( 체력이 모두 회복되었다. )" });
        chatData.Add(11, new string[] { "*  ( 낙엽을 밟을 때의\n    바스락거리는 즐거운 소리에\n    당신의 의지가 충만해진다. )",
            "*  ( 체력이 모두 회복되었다. )" });
        
        chatData.Add(100, new string[] { "*  두려움 없는 자 앞으로 나아가라\n*  용기있는 자, 어리석은 자\n*  둘 다 가운데 길로 가지 말라" });
        chatData.Add(101, new string[] { "*  앞으로 나아가시오." });
        chatData.Add(102, new string[] { "*  서쪽 방은 동쪽 방의\n    청사진이다." });

        chatData.Add(200, new string[] { "*  \"표지를 읽으려면\n    [Z] 키를 누르시오. \"" });

        chatData.Add(300, new string[] { "*  이 스위치를 눌러요.\n\n                        - 토리엘" });
        chatData.Add(301, new string[] { "*  이 스위치도 눌러요.\n\n                        - 토리엘" });

        chatData.Add(400, new string[] { "*  개굴, 개굴.\n*  ( 실례할게, 인간. )",
            "*  ( 괴물들과 싸울 때를\n    위한 조언 하나 해 줄게. )",
            "*  ( 특정 행동을 하거나\n    거의 쓰러질 때까지\n    싸우면 말이지... )",
            "*  ( 괴물들은 너랑은 더\n    싸우려 하지 않을 거야. )",
            "*  ( 괴물이 너랑 싸우고\n    싶어하지 않아하면,\n    부디... )",
            "*  ( 자비를 보여줘, 인간. ) \n*  개굴."});
    }

    // Get 함수로 텍스트 데이터 전달
    public string GetChat(int id, int chatIndex)
    {
        if (chatIndex == chatData[id].Length)
            return null;
        else
            return chatData[id][chatIndex];
    }
}
