using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

public class SnakeEnemyMovement : MonoBehaviour
{
    private Vector3 moveDirection;
    private BTRunner _btRunner = null;
    private Transform _otherSnake = null;

    [SerializeField] private float _detectOtherSnakeRange = 10f;
    [SerializeField] private float _detectFoodRange = 150f;
    [SerializeField] private float _moveSpeed = 8f;
    private bool _isRandomMoved = false;
    private bool _isPursuitOtherSnake = false;
    private Transform _detectSnake = null;
    private Transform _detectFood = null;
    private SnakeController _snakeController;
    private SnakeController _otherSnakeController;
    private Rigidbody _rb;

    private Transform _otherSnakeHead;
    private Transform _snakeHead;


    private void Awake()
    {
        _btRunner = new BTRunner(SettingBT());
    }

    private void Start()
    {
        _snakeController = GetComponent<SnakeController>();
        _snakeHead = _snakeController.GetSnakehead();
        _rb = _snakeHead.GetComponent<Rigidbody>();
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    private void Update()
    {
        _btRunner.operate();
        _rb.velocity = moveDirection.normalized * _moveSpeed;
    }

    private INode SettingBT()
    {
        return new SelectorNode
            (
                new List<INode>()
                {
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectOtherSnake),
                            new ActionNode(MoveToEatOtherSnake)
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectFood),
                            new ActionNode(MoveToEatFood)
                        }
                    ),
                    new ActionNode(MoveRandom)
                }
            );
    }


    #region Detect & Move Node
    private INode.ENodeState CheckDetectOtherSnake()
    {
        if (_otherSnakeHead != null) return INode.ENodeState.ESuccess;

        if(_isPursuitOtherSnake) return INode.ENodeState.EFailure;

        var overlapColliders = Physics.OverlapSphere(_snakeHead.position, _detectOtherSnakeRange, LayerMask.GetMask("Snake"));
        if (overlapColliders != null && overlapColliders.Length > 0)
        {
            foreach (var collider in overlapColliders)
            {
                if (collider.name != Define.PrefabName.snakeHeadPrefab) continue;
                _detectSnake = collider.transform;
                _otherSnakeController = _detectSnake.transform.root.GetComponent<SnakeController>();

                if (_otherSnakeController.GetSnakeLevel() < _snakeController.GetSnakeLevel())
                {
                    _otherSnakeHead = _otherSnakeController.GetSnakehead();
                    StartCoroutine(DetectOtherSnakeTime());
                    return INode.ENodeState.ESuccess;
                }
            }
        }

        _detectSnake = null;
        _otherSnakeController = null;
        _otherSnakeHead = null;

        return INode.ENodeState.EFailure;
    }

    private IEnumerator DetectOtherSnakeTime()
    {
        yield return new WaitForSeconds(4f);
        _otherSnakeHead = null;
        StartCoroutine(CoolDownDetectOtherSnake());
    }

    private IEnumerator CoolDownDetectOtherSnake()
    {
        _isPursuitOtherSnake = true;
        yield return new WaitForSeconds(4f);
        _isPursuitOtherSnake = false;
    }


    private INode.ENodeState MoveToEatOtherSnake()
    {
        if (_otherSnakeHead != null)
        {
            moveDirection = (_otherSnakeHead.position - _snakeHead.position).normalized;
            RotateSnake();
            if (Vector3.Distance(_otherSnakeHead.position, _snakeHead.position) < 0.5f)
            {
                _otherSnakeHead = null;
                return INode.ENodeState.ESuccess;
            }
            return INode.ENodeState.ERunning;
        }

        return INode.ENodeState.EFailure;
    }
    #endregion


    #region Food
    private INode.ENodeState CheckDetectFood()
    {
        //if(_otherSnakeHead != null) return INode.ENodeState.EFailure;
        if (_detectFood != null) return INode.ENodeState.ESuccess;

        var overlapColliders = Physics.OverlapSphere(_snakeHead.position, _detectOtherSnakeRange, LayerMask.GetMask(Define.ObjectName.food));
        if (overlapColliders != null && overlapColliders.Length > 0)
        {
            _detectFood = overlapColliders[0].transform;
            
            return INode.ENodeState.ESuccess;
        }

        _detectFood = null;

        return INode.ENodeState.EFailure;
    }

    private INode.ENodeState MoveToEatFood()
    {
        if (_detectFood != null)
        {
            moveDirection = (_detectFood.position - _snakeHead.position).normalized;
            RotateSnake();
            if (Vector3.Distance(_detectFood.position, _snakeHead.position) < 0.5f)
            {  
                _detectFood = null;
                return INode.ENodeState.ESuccess;
            }
            return INode.ENodeState.ERunning;
        }

        return INode.ENodeState.EFailure;
    }
    #endregion

    


    #region MoveRandom
    private INode.ENodeState MoveRandom()
    {
        if (!_isRandomMoved)
        {
            _isRandomMoved = true;
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            RotateSnake();
            StartCoroutine(IsRandomMoved());
            return INode.ENodeState.ESuccess;
        }

        return INode.ENodeState.EFailure;
    }

    private IEnumerator IsRandomMoved()
    {
        yield return new WaitForSeconds(2f);
        _isRandomMoved = false;
    }
    #endregion


    private void RotateSnake()
    {
        Quaternion newRotation = Quaternion.LookRotation(moveDirection);
        _rb.MoveRotation(newRotation);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_snakeHead.position, _detectOtherSnakeRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_snakeHead.position, _detectFoodRange);
    }
}
