using TanksEngine.LevelCreator;
using TanksEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Gameplay
{
    /// <summary>
    /// Исполняет выбранный режим игры.
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
        /// Вызывает инициализацию всех модулей
        /// </summary>
        private void Start()
        {
            //здесь будет проверка на режимы игры

            InitPlayer();
            InitEnemies();
            InitEvents();
            InitLevel();

        }
        /// <summary>
        /// Выдает награду игроку
        /// </summary>
        private void GiveReward()
        {

        }
        #region[EventsHandlers]
        /// <summary>
        /// Обрабатывает смерть врага
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
                //GameWin(); //Победа - если врагов не осталось
            }
        }
        /// <summary>
        /// Обрабатывает смерть игрока
        /// </summary>
        private void HandlerOnPlayerDie()
        {
            //GameLose(); //Поражение - если игрок умер
        } /// <summary>
          /// Обрабатывает событие победы
          /// </summary>
        private void GameWin()
        {
            Debug.Log("Win!");
            Global_EventManager.CallOnGameWin(); //Вызов события для UI
            Player.SetActive(false);

            GiveReward();
        }
        /// <summary>
        /// Обрабатывает событие поражения
        /// </summary>
        private void GameLose()
        {
            Debug.Log("Lose!");
            Global_EventManager.CallOnGameLose();//Вызов события для UI
        }
        #endregion
        #region[Initialisators]
        /// <summary>
        /// Создает игрока и камеру
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
        /// Подгружает врагов и вызвает функцию их спавна
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
        /// Вызывает события, после инициализации игрового режима
        /// </summary>
        private void InitEvents()
        {
            //Global_EventManager.CallOnCoinsUpdate();
        }
        /// <summary>
        /// Инициализирует игровую локацию
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

