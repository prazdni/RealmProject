using System;
using System.Collections.Generic;
using UnityEngine;

namespace Realm
{
    public class InputController : IExecute
    {
        #region Fields
        
        public Action<List<InteractableObject>> OnNodeChange = (u) => { };
        
        private readonly List<InteractableObject> _puzzleNodes;
        private List<Vector3> _possiblePoints;
        
        private Transform _interactiveObject;
        private readonly Camera _mainCamera;
        private PuzzleNode _currentNode;
        
        private bool _isNewPositionProcess;
        private bool _isNodeChanged;

        #endregion


        #region ClassLifeCycles

        public InputController(List<InteractableObject> nodes)
        {
            _puzzleNodes = nodes;
            _isNewPositionProcess = false;
            _isNodeChanged = false;
            _mainCamera = Camera.main;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_isNodeChanged)
            {
                OnNodeChange.Invoke(_puzzleNodes);
                _isNodeChanged = false;
            }
            
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (!_isNewPositionProcess)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 10.0f))
                {
                    if (Input.GetMouseButtonUp(0) && hit.collider.gameObject.CompareTag("Cube"))
                    {
                        _interactiveObject = hit.collider.gameObject.transform;
                        _currentNode = _interactiveObject.GetComponent<PuzzleNode>();
                        _possiblePoints = _currentNode.GetPossiblePoints();
                        
                        _isNewPositionProcess = true;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _currentNode.SetShake(false);
                    
                    var mouseClickPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    var currentPosition = new Vector3(mouseClickPosition.x, mouseClickPosition.z, mouseClickPosition.y);
                    
                    float tmpMagnitude = Mathf.Infinity;
                    for (int i = 0; i < _possiblePoints.Count; i++)
                    {
                        var point = _possiblePoints[i];
                        if ((point - currentPosition).sqrMagnitude < tmpMagnitude)
                        {
                            tmpMagnitude = (point - currentPosition).sqrMagnitude;
                            _interactiveObject.transform.localPosition = point;
                        }
                    }
                    
                    _isNodeChanged = true;
                    _isNewPositionProcess = false;
                }
            }
            
            Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        }

        #endregion
    }
}