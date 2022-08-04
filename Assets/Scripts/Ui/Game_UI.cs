using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TanksEngine.UI
{
    public class Game_UI : MonoBehaviour
    {
        [SerializeField] private GameObject Game_Mode;
        [SerializeField] private Slider slider_HP;
        [SerializeField] private GameObject Timer_text;
        [SerializeField] private GameObject Coins_text;
        [SerializeField] private GameObject PanelLose;
        [SerializeField] private GameObject PanelWin;
        [SerializeField] private GameObject TextReward;

        public FloatingJoystick Joystick { get; private set; }

        public FloatingJoystick JoystickGO;

        private float Hp_Cur;
        private float Hp_Max;
        private float timer;

        private GameObject Player;
        private CharacterEntity Player_attributes;
        
        
        private void Init(GameObject player)
        {
            Joystick = JoystickGO;
            Player = player;
            Player_attributes = player.GetComponent<CharacterEntity>();
            Global_EventManager.CallOnGameUILoaded(this);
        }

        private void OnEnable()
        {
            Global_EventManager.eventOnCoinsUpdate += HandlerOnCoinsUpdate;
            Global_EventManager.eventOnGameWin += eventOnGameWin;
            Global_EventManager.eventOnGameLose += eventOnGameLose;
            Global_EventManager.eventOnPlayerLoaded += Init;
        }

        private void OnDestroy()
        {
            Global_EventManager.eventOnCoinsUpdate -= HandlerOnCoinsUpdate;
            Global_EventManager.eventOnGameWin -= eventOnGameWin;
            Global_EventManager.eventOnGameLose -= eventOnGameLose;
        }
        private void HandlerOnCoinsUpdate()
        {
            //Coins_text.GetComponent<Text>().text = $"Coins: {Game_Mode.GetComponent<Game_Mode>().Coins_reward}";
        }
        private void eventOnGameWin()
        {
            PanelWin.SetActive(true);
            //TextReward.GetComponent<Text>().text = $" + {Game_Mode.GetComponent<Game_Mode>().Coins_reward} coins!";
        }

        private void eventOnGameLose()
        {
            PanelLose.SetActive(true);
        }

        

        void Start()
        {
            Global_EventManager.CallOnGetGameCore(gameObject);
            if (Player != null)
            {
                Player_attributes = Player.GetComponent<CharacterEntity>();
            }
            PanelWin.SetActive(false);
            PanelLose.SetActive(false);
        }

        void Update()
        {
            Timer_text.GetComponent<Text>().text = timer.ToString();
        }
        
        public void ToMainMenu()
        {
            SceneManager.LoadScene(0);
            MainMenu.SkipMenu = true;
        }
    }

}
