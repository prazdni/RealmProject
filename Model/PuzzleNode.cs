using System.Collections.Generic;
using UnityEngine;

namespace Realm
{
    public class PuzzleNode : InteractableObject, IShake
    {
        #region Fields

        private Vector3[] _mainVectors;
        
        private Quaternion _defaultRotation;
        private Vector3 _defaultPosition;

        private bool _isShaking;

        #endregion

        
        #region UnityMethods

        private void Start()
        {
            _defaultPosition = transform.localPosition;
            _defaultRotation = transform.rotation;
            
            _mainVectors = new[]
            {
                Vector3.up,
                Vector3.down,
                Vector3.left,
                Vector3.right
            };
        }

        #endregion


        #region IExecute

        public override void Execute()
        {
            if (_isShaking)
            {
                Shake();
            }
        }

        #endregion

        
        #region IShake

        public void Shake()
        {
            float coordinateRotation =Mathf.PingPong(Time.time, 1.0f) * 2 - 1;
            transform.Rotate(0.0f, coordinateRotation/Mathf.Abs(coordinateRotation), 0.0f);
        }

        public void SetShake(bool isShaking)
        {
            _isShaking = isShaking;
            transform.rotation = _defaultRotation;
        }

        #endregion

        
        #region Methods

        public List<Vector3> GetPossiblePoints()
        {
            _isShaking = true;
            
            var availableObjects = new List<Vector3>();

            RaycastHit hit;
            
            for (int i = 0; i < _mainVectors.Length; i++)
            {
                if (RaycastHitObject(_mainVectors[i] * 2, out hit))
                {
                    CompareHitAndAdd(hit, availableObjects);
                }
            }
            
            availableObjects.Add(gameObject.transform.localPosition);

            return availableObjects;
        }
        
        public override void GameRestart()
        {
            transform.localPosition = _defaultPosition;
        }
        
        private bool RaycastHitObject(Vector3 dir, out RaycastHit hit)
        {
            return Physics.Raycast(transform.position, dir, out hit, 2.0f);
        }

        private void CompareHitAndAdd(RaycastHit hit, List<Vector3> availableObjects)
        {
            if (hit.collider.gameObject.CompareTag("Point"))
            {
                availableObjects.Add(hit.collider.gameObject.transform.localPosition);
            }
        }

        #endregion
    }
}