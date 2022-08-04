using TanksEngine.LevelCreator;
using TanksEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Gameplay
{
    /// <summary>
    /// ��������� ��������� ����� ����.
    /// </summary>
    public class Game_Mode : MonoBehaviour
    {
        #region[Variables]
        [Header("Player")]
        public GameObject Player;
        public GameObject Camera;
        public int Coins_reward { get; private set; }
        public GameObject PlayerSpawnPoint;
        [Header("Level")]
        public bool LoadLevelOnStart;
        public GameLevelManager levelManager;
        [Header("Enemies")]
        public bool SpawnEnemy;
        public GameObject[] EnemySpawnPoints;
        [SerializeField] private GameObject[] Enemies;
        [SerializeField] private int Num_Enemy;
        #endregion
        #region[EventsSetup]
        private void OnEnable()
        {
            Global_EventManager.eventOnEnemyDie += HandlerOnEnemyDie;
            Global_EventManager.eventOnPlayerDie += HandlerOnPlayerDie;
        }

        private void OnDestroy()
        {
            Global_EventManager.eventOnEnemyDie -= HandlerOnEnemyDie;
            Global_EventManager.eventOnPlayerDie -= HandlerOnPlayerDie;
        }
        #endregion

        /// <summary>
        /// �������� ������������� ���� �������
        /// </summary>
        private void Start()
        {
            //����� ����� �������� �� ������ ����

            InitPlayer();
            InitEnemies();
            InitEvents();
            InitLevel();

        }
        /// <summary>
        /// ������ ������� ������
        /// </summary>
        private void GiveReward()
        {

        }
        #region[EventsHandlers]
        /// <summary>
        /// ������������ ������ �����
        /// </summary>
        /// <param name="enemy"></param>
        private void HandlerOnEnemyDie(GameObject enemy)
        {
            if (enemy.GetComponent<Bot_Info>())
            {
                //Player.GetComponent<TankDetailsManager>().coins += enemy.GetComponent<Bot_Info>().reward;
                Coins_reward += enemy.GetComponent<Bot_Info>().reward;
                Global_EventManager.CallOnCoinsUpdate();
            }
            Num_Enemy--;
            if (Num_Enemy <= 0)
            {
                //GameWin(); //������ - ���� ������ �� ��������
            }
        }
        /// <summary>
        /// ������������ ������ ������
        /// </summary>
        private void HandlerOnPlayerDie()
        {
            //GameLose(); //��������� - ���� ����� ����
        } /// <summary>
          /// ������������ ������� ������
          /// </summary>
        private void GameWin()
        {
            Debug.Log("Win!");
            Global_EventManager.CallOnGameWin(); //����� ������� ��� UI
            Player.SetActive(false);

            GiveReward();
        }
        /// <summary>
        /// ������������ ������� ���������
        /// </summary>
        private void GameLose()
        {
            Debug.Log("Lose!");
            Global_EventManager.CallOnGameLose();//����� ������� ��� UI
        }
        #endregion
        #region[Initialisators]
        /// <summary>
        /// ������� ������ � ������
        /// </summary>
        private void InitPlayer()
        {
            GameResourcesData _data = Resources.Load<GameResourcesData>("GameResource");

            if (Player == null)
            {
                Player = GameObject.Instantiate(_data.Player, PlayerSpawnPoint.transform.position, PlayerSpawnPoint.transform.rotation);
                //Player.GetComponent<PlayerController>()._joystick = GameUI.GetComponent<Game_UI>().Joystick;
            }
            if (Camera == null)
            {
                Camera = GameObject.Instantiate(_data.MainCamera);
                Camera.GetComponent<CameraController>().Player = Player.transform;
            }

            Global_EventManager.CallOnPlayerLoaded(Player);
        }
        /// <summary>
        /// ���������� ������ � ������� ������� �� ������
        /// </summary>
        private void InitEnemies()
        {
            if (SpawnEnemy)
            {
                GameResourcesData _data = Resources.Load<GameResourcesData>("GameResource");

                Enemies = _data.Enemies;
                Num_Enemy = Enemies.Length;

                BotsSpawner.SpawnBots(_data.Enemies, EnemySpawnPoints);
            }
        }
        /// <summary>
        /// �������� �������, ����� ������������� �������� ������
        /// </summary>
        private void InitEvents()
        {
            //Global_EventManager.CallOnCoinsUpdate();
        }
        /// <summary>
        /// �������������� ������� �������
        /// </summary>
        private void InitLevel()
        {
            if (LoadLevelOnStart)
            {
                levelManager.LoadLevelRandom();
            }

        }
        #endregion
       
       

    }
}

