using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private GameObject _prefab;

    [SerializeField] 
    private int _count = 20; 

    [SerializeField]
    private float _step = 1;

    [SerializeField]
    private float _delayBeforeSpawn = 0.04f; 
    
    private float _recoloringDuration = 0.5f;

    [SerializeField]
    private float _delayBeforeRecoloring = 0.2f;

    private GameObject[,] _cubes;

    private Vector3 _startPosition = new(-1, 19, 0);

    private void Start()
    {
        _cubes = new GameObject[_count, _count];

        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        for (var y = 0; y < _count; y++)
        {
            for (var x = 0; x < _count; x++)
            {
                var cube = Instantiate(_prefab); 

                cube.transform.position = new Vector3(_startPosition.x+x * _step, _startPosition.y-y* _step, 0); 
            
                _cubes[x, y] = cube; 

                yield return new WaitForSeconds(_delayBeforeSpawn); 
            }
        }
    }
    
    public void ChangeColors()
    {
        StartCoroutine(ChangeCubesColor());
    }

    private IEnumerator ChangeCubesColor()
    {
        var nextColor = Random.ColorHSV(); 
        
        for (var i = 0; i < _count; i++)
        {
            for (var j = 0; j < _count; j++)
            {
                var currentCube=_cubes[j, i].GetComponent<Recoloring>();
                currentCube.SetColor(nextColor, _recoloringDuration); 
            
                yield return new WaitForSeconds(_delayBeforeRecoloring); 
            }
        }
    }
}
