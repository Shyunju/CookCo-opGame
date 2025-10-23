using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CookCo_opGame
{
    public class SelectFile : MonoBehaviour
    {
        [SerializeField] GameObject _creat;	// 플레이어 닉네임 입력UI
        [SerializeField] TMP_Text[] _slotText;		// 슬롯버튼 아래에 존재하는 Text들
        [SerializeField] TMP_Text _newPlayerName;	// 새로 입력된 플레이어의 닉네임

        bool[] _savefile = new bool[3];	// 세이브파일 존재유무 저장

        void Start()
        {
            // 슬롯별로 저장된 데이터가 존재하는지 판단.
            for (int i = 0; i < 3; i++)
            {
                if (File.Exists(DataManager.Instance.Path + $"{i}"))
                {
                    _savefile[i] = true;			// 해당 슬롯 번호의 bool배열 true로 변환
                    DataManager.Instance.NowSlot = i;	// 선택한 슬롯 번호 저장
                    DataManager.Instance.LoadData();	// 해당 슬롯 데이터 불러옴
                    _slotText[i].text = $"{DataManager.Instance.NowPlayer.Name} \n{DataManager.Instance.NowPlayer.Wallet} Gold \n저장 시각 : {DataManager.Instance.NowPlayer.year} / {DataManager.Instance.NowPlayer.month} / {DataManager.Instance.NowPlayer.day}";	// 버튼에 닉네임 표시
                }
                else
                {
                    _slotText[i].text = "비어있음";
                }
            }
            // 불러온 데이터를 초기화시킴.(버튼에 닉네임을 표현하기위함이었기 때문)
            DataManager.Instance.DataClear();
        }

        public void Slot(int number)	// 슬롯의 기능 구현
        {
            PlayButtonSound();
            DataManager.Instance.NowSlot = number;	// 슬롯의 번호를 슬롯번호로 입력함.

            if (_savefile[number])	// bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
            {
                DataManager.Instance.LoadData();	// 데이터를 로드하고
                GoGame();	// 게임씬으로 이동
            }
            else	// bool 배열에서 현재 슬롯번호가 false라면 데이터가 없다는 뜻
            {
                Creat();	// 플레이어 닉네임 입력 UI 활성화
            }
        }

        public void Creat()	// 플레이어 닉네임 입력 UI를 활성화하는 메소드
        {
            _creat.gameObject.SetActive(true);
        }

        public void GoGame()	// 게임씬으로 이동
        {
            if (!_savefile[DataManager.Instance.NowSlot])	// 현재 슬롯번호의 데이터가 없다면
            {
                DataManager.Instance.NowPlayer.Name = _newPlayerName.text; // 입력한 이름을 복사해옴
                DataManager.Instance.SaveData(); // 현재 정보를 저장함.
            }
            SceneManager.LoadScene("LobbyScene"); // 게임씬으로 이동

        }
        public void ExitGame()
        {
            DataManager.Instance.ExitGame();
        }
        public void PlayButtonSound()
        {
            SoundManager.Instance.PlayPressedButtonSound();
        }
    }
}
