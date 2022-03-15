using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject TextBox;
    public Text TalkText;
    public Text CharTalkText;

    public GameObject ScanObj;
    public ChatManager chatManager;
    public int chatIndex;
    public bool isAction;
    public bool isTexting;

    AudioSource audioSource;
    public AudioClip TextSound;

    void Start()
    {
        Screen.SetResolution(640, 480, false);

        // 오디오 설정
        this.audioSource = GetComponent<AudioSource>();
        audioSource.clip = TextSound;
    }

    public void Action(GameObject scanObject)
    {
        ScanObj = scanObject;
        ObjectData objData = ScanObj.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);
        
        TextBox.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string chatData = chatManager.GetChat(id, chatIndex);

        // chatIndex에 해당하는 데이터가 null일 경우
        if (chatData == null)
        {
            isAction = false;
            chatIndex = 0;
            return;
        }
        
        // 초상화가 있는 NPC의 경우 CharTalkText, 아닐 경우 TalkText로 텍스트 출력
        if (isNpc)
            StartCoroutine(ChatEffect(CharTalkText, chatData));
        else
            StartCoroutine(ChatEffect(TalkText, chatData));

        isAction = true;
        chatIndex++;
    }

    IEnumerator ChatEffect(Text text, string chat)
    {
        string writerChat = "";
        char space = ' ';
        char whiteSpace = ' ';

        for (int i = 0; i <= chat.Length; i++)
        {
            if (i == chat.Length)
            {
                isTexting = false;
                yield return null;
            }
            else
            {
                isTexting = true;
                writerChat += chat[i];
                text.text = writerChat;

                if (chat[i].CompareTo(space) == 0)
                    continue;

                if (chat[i].CompareTo(whiteSpace) == 0)
                {
                    yield return new WaitForSeconds(0.3f);
                    continue;
                }

                audioSource.Play();
                yield return new WaitForSeconds(0.08f);
            }
        }
    }
}
