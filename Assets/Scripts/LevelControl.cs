using UnityEngine;

namespace WorldsDev
{
    public class LevelControl : MonoBehaviour
    {
        public static LevelControl Instance;
        private Vector3 _temp;
        public int _maxLength = 7;
        public int _maxLines = 5;
        private Vector3 _slotScale = new Vector3(2, 1.9f, 2);
        [HideInInspector] public int MaxLines = 5;
        [HideInInspector] public int SpawnLocation = -14;
        [HideInInspector] public int SpawnHeight = 1;
        [HideInInspector] public Vector3 SummonsRotation = new Vector3(0, 180, 0);

        protected void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            Instance = this;
            CreateSlots();
        }

        //Create the colliders to detect when the player wants to spawn something at that position
        private void CreateSlots()
        {
            for (int i = 0; i < _maxLines; i++)
            {
                UpdateTempToLine(i);

                for (int j = 0; j < _maxLength; j++)
                {
                    var cube = new GameObject("Slot " + i + "-" + j);
                    cube.AddComponent<BoxCollider>();
                    _temp.z = (-2 * j);
                    cube.transform.position = _temp;
                    cube.transform.localScale = _slotScale;
                    cube.transform.SetParent(transform, true);

                    cube.AddComponent<SummonSlot>();
                }
            }
        }

        //Gets the position for the given line
        public Vector3 GetLinePosition(int line)
        {
            //Set the line
            UpdateTempToLine(line);
            _temp.z = 0;
            return _temp;
        }

        //Set the value of the temp vector to find the x position that represents the line from the upper line to the lowest 
        private void UpdateTempToLine(int line)
        {
            switch (line)
            {
                case 0:
                    _temp.x = 4;
                    break;
                case 1:
                    _temp.x = 2;
                    break;
                case 2:
                    _temp.x = 0;
                    break;
                case 3:
                    _temp.x = -2;
                    break;
                case 4:
                    _temp.x = -4;
                    break;
            }
        }
    }
}