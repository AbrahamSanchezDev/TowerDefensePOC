using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    public class PlayerControl : MonoBehaviour
    {
        private int _curMoney;
        private float _moneyPerLevel = 0.5f;


        private SummonObj _curSelectedSummon;

        private Dictionary<Vector3, GameObject> _spawnedPos = new Dictionary<Vector3, GameObject>();

        public static PlayerControl Instance;


        protected void Awake()
        {
            Instance = this;
            Setup();
        }

        protected void Setup()
        {
            _curMoney = CurStartingMoney();
            AddMoney(_curMoney);

            var curLevel = GameControl.Level;
            var db = PrefabsRef.Prefabs.SummonDB.SummonObjs;
            var canvas = GameCanvasUi.Instance;
            for (var i = 0; i < db.Count; i++)
            {
                var cur = db[i];
                if (!cur.Data.SpawnAtThisLevel(curLevel)) continue;
                if (_curSelectedSummon == null) _curSelectedSummon = cur;

                var price = db[i].Price;

                canvas.AddSummon(cur.Icon, price, () => { _curSelectedSummon = cur; }, i);
            }
        }

        //Add Passive money
        private IEnumerator Start()
        {
            var delay = new WaitForSeconds(0.5f);
            var addPerTick = 2 + (int) (_moneyPerLevel * GameControl.Level);
            while (GameControl.CurGameState != GameState.Lost)
            {
                if (GameControl.CurGameState == GameState.StartedPlaying) AddMoney(addPerTick);

                yield return delay;
            }
        }

        //Calculate whats the starting money
        private int CurStartingMoney()
        {
            return 20 + (int) (_moneyPerLevel * GameControl.Level);
        }

        //Add Money to the player
        public void AddMoney(int amount)
        {
            if (GameControl.CurGameState != GameState.StartedPlaying) return;

            _curMoney += amount;
            if (GameCanvasUi.Instance)
                GameCanvasUi.Instance.UpdateMoney(_curMoney);
        }

        private bool CanBuyCurrent()
        {
            if (_curSelectedSummon == null) return false;
            return _curMoney >= _curSelectedSummon.Price;
        }

        public void SpawnCurrentAt(Vector3 pos)
        {
            if (GameControl.CurGameState != GameState.StartedPlaying) return;
            if (!CanBuyCurrent()) return;
            if (_spawnedPos.ContainsKey(pos))
            {
                //Check if there is an existing obj if so don't spawn a new obj
                if (_spawnedPos[pos] != null) return;
            }
            else
            {
                //No previews spawned obj
                _spawnedPos.Add(pos, null);
            }

            //Spawn summon

            //Remove Money
            AddMoney(-_curSelectedSummon.Price);
            var go = Instantiate(_curSelectedSummon.Prefab, pos, Quaternion.identity);
            go.transform.eulerAngles = LevelControl.Instance.SummonsRotation;
            _spawnedPos[pos] = go;
            go.SetActive(false);

            var ai = go.AddComponent<SummonAI>();
            ai.SecondSetup(_curSelectedSummon.Data);
            //Do extra setups on game object
            _curSelectedSummon.DoSetup(go, _curSelectedSummon.Data);

            go.SetActive(true);
        }
    }
}