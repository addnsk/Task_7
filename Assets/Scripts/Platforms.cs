using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private List<GameObject> _platforms;
    [SerializeField] private int _rowsAndColumns;
    [SerializeField] private int _coordinateStep;
    [SerializeField] private int _probability;

    private GameObject[,] _map;

    private Vector3 _position;
    private int _emptyCount = 1;


    private void Awake()
    {
        System.Random random = new System.Random();

        _map = new GameObject[_rowsAndColumns, _rowsAndColumns];

        for (float i = _rowsAndColumns * - _coordinateStep; i < _map.GetLength(0) * _coordinateStep; i += _coordinateStep)
        {
            for (float j = _rowsAndColumns * - _coordinateStep; j < _map.GetLength(1) * _coordinateStep; j += _coordinateStep)
            {
                if (random.Next(1, 100) <= _probability && _emptyCount <= 0)
                {
                    _emptyCount = 2;
                    _position = new Vector3(j, i, transform.position.z);

                    var platform = Instantiate(_platforms[random.Next(0, _platforms.Count)], _position, Quaternion.identity);
                    platform.transform.SetParent(gameObject.transform);
                }
                else
                {
                    _emptyCount -= 1;
                }
            }
        }
    }
}