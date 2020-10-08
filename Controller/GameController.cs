using System.Collections.Generic;
using UnityEngine;

namespace Realm
{
    public class GameController : MonoBehaviour
    {
        private ListExecuteObjects _executeObjects;
        private PuzzleNodesCreator _puzzleNodes;
        
        private void Start()
        {
            _puzzleNodes = new PuzzleNodesCreator();
            _executeObjects = new ListExecuteObjects(_puzzleNodes.GetPuzzleNodes());

            var cameraController = new InputController(_puzzleNodes.GetPuzzleNodes());
            _executeObjects.AddExecuteObject(cameraController);
            cameraController.OnNodeChange += GameEnding;
            
            var buttonController = new ButtonController(_puzzleNodes);
        }

        private void Update()
        {
            for (int i = 0; i < _executeObjects.Count; i++)
            {
                _executeObjects[i].Execute();
            }
        }

        private void GameEnding(List<InteractableObject> currentNodes)
        {
            var listPuzzleNodes = currentNodes;

            var listOfNodes = listPuzzleNodes.ToGroupedDictionary();

            if (ShouldCheckFurther(listOfNodes))
            {
                if (ShouldGameOver(listOfNodes))
                {
                    _executeObjects.AddExecuteObject(FindObjectOfType<WinImage>());
                    FindObjectOfType<RestartButton>().ActivateButton(true);
                    //Time.timeScale = 0.0f;
                    print("asd");
                }
            }
        }

        private bool ShouldCheckFurther(Dictionary<float, List<InteractableObject>> listOfNodes)
        {
            var shouldCheckFurther = true;
            
            foreach (var val in listOfNodes)
            {
                if (val.Value.Count < Constants.MAX_ELEMENTS_IN_ROW)
                {
                    shouldCheckFurther = false;
                }
            }

            return shouldCheckFurther;
        }

        private bool ShouldGameOver(Dictionary<float, List<InteractableObject>> listOfNodes)
        {
            var shouldOGameOver = true;
                
            foreach (var node in listOfNodes)
            {
                for (int i = 0; i < node.Value.Count - 1; i++)
                {
                    if (node.Value[i].NodeType != node.Value[i+1].NodeType)
                    {
                        shouldOGameOver = false;
                    }
                }
            }

            return shouldOGameOver;
        }
    }
}