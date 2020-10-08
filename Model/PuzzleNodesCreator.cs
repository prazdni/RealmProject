using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Realm
{
    public class PuzzleNodesCreator
    {
        #region Fields

        private List<InteractableObject> _puzzleNodes;

        private PuzzleNodeType[] _nodeTypes;

        #endregion


        #region ClassLifeCycles

        public PuzzleNodesCreator()
        {
            _nodeTypes = new[]
            {
                PuzzleNodeType.Star,
                PuzzleNodeType.Diamond,
                PuzzleNodeType.Medal
            };
            
            _puzzleNodes = new List<InteractableObject>();
            
            _puzzleNodes = Object.FindObjectsOfType<InteractableObject>().ToList();

            CreateRegularLevel();
        }

        #endregion


        #region Methods

        public void CreateRegularLevel()
        {
            var quantityOfTypes = new Dictionary<PuzzleNodeType, int>()
            {
                [PuzzleNodeType.Star]    = 0,
                [PuzzleNodeType.Diamond] = 0,
                [PuzzleNodeType.Medal]   = 0,
            };
            
            for (int i = 0; i < _puzzleNodes.Count; i++)
            {
                var rand = Random.Range(0, Constants.QUANTITY_OF_ELEMENTS);
                
                if (quantityOfTypes[_nodeTypes[rand]] < Constants.MAX_ELEMENTS_IN_ROW)
                {
                    AddTypeAndMaterial(_puzzleNodes[i], _nodeTypes[rand], quantityOfTypes);
                }
                else
                {
                    AddDifferentTypeAndMaterial(_puzzleNodes[i], _nodeTypes[rand], quantityOfTypes);
                }
            }
        }
        
        public void CreateEasyLevel()
        {
            var groupedNodes = _puzzleNodes.ToGroupedDictionary();

            var randList = CreateRandList(Random.Range(0, Constants.QUANTITY_OF_ELEMENTS));

            var randIterator = 0;
            foreach (var node in groupedNodes)
            {
                if (randIterator == Constants.QUANTITY_OF_ELEMENTS - 2)
                {
                    SetDifferentTypeAndMaterialToPuzzleList(node.Value, _nodeTypes[randList[randIterator]], 
                        _nodeTypes[randList[randIterator + 1]]);
                }
                else
                {
                    if (randIterator == Constants.QUANTITY_OF_ELEMENTS - 1)
                    {
                        SetDifferentTypeAndMaterialToPuzzleList(node.Value, _nodeTypes[randList[randIterator]], 
                            _nodeTypes[randList[randIterator - 1]]);
                    }
                    else
                    {
                        SetDefaultTypeAndMaterialToPuzzleList(node.Value, _nodeTypes[randList[randIterator]]);
                    }
                }

                randIterator++;
            }
        }
        
        private void AddDifferentTypeAndMaterial(InteractableObject nodeToChange, PuzzleNodeType nodeType, 
            Dictionary<PuzzleNodeType, int> currentQuantityOfTypes)
        {
            switch (nodeType)
            {
                case PuzzleNodeType.Medal:
                    if (currentQuantityOfTypes[PuzzleNodeType.Diamond] < Constants.MAX_ELEMENTS_IN_ROW)
                    {
                        AddTypeAndMaterial(nodeToChange, PuzzleNodeType.Diamond, currentQuantityOfTypes);
                    }
                    else
                    {
                        AddTypeAndMaterial(nodeToChange, PuzzleNodeType.Star, currentQuantityOfTypes);
                    }
                    break;
                case PuzzleNodeType.Diamond:
                    if (currentQuantityOfTypes[PuzzleNodeType.Medal] < Constants.MAX_ELEMENTS_IN_ROW)
                    {
                        AddTypeAndMaterial(nodeToChange, PuzzleNodeType.Medal, currentQuantityOfTypes);
                    }
                    else
                    {
                        AddTypeAndMaterial(nodeToChange, PuzzleNodeType.Star, currentQuantityOfTypes);
                    }
                    break;
                case PuzzleNodeType.Star:
                    if (currentQuantityOfTypes[PuzzleNodeType.Medal] < Constants.MAX_ELEMENTS_IN_ROW)
                    {
                        AddTypeAndMaterial(nodeToChange, PuzzleNodeType.Medal, currentQuantityOfTypes);
                    }
                    else
                    {
                        AddTypeAndMaterial(nodeToChange, PuzzleNodeType.Diamond, currentQuantityOfTypes);
                    }
                    break;
            }
        }

        private void AddTypeAndMaterial(InteractableObject nodeToChange, PuzzleNodeType nodeType, 
            Dictionary<PuzzleNodeType, int> currentQuantityOfTypes)
        {
            nodeToChange.NodeType = nodeType;
            nodeToChange.GetComponent<Renderer>().material = Reference.GetMaterial(nodeType);
            currentQuantityOfTypes[nodeType]++;
        }
        
        private void AddTypeAndMaterial(InteractableObject nodeToChange, PuzzleNodeType nodeType)
        {
            nodeToChange.NodeType = nodeType;
            nodeToChange.GetComponent<Renderer>().material = Reference.GetMaterial(nodeType);
        }

        private void SetDefaultTypeAndMaterialToPuzzleList(List<InteractableObject> puzzles, PuzzleNodeType nodeType)
        {
            for (int i = 0; i < puzzles.Count; i++)
            {
                AddTypeAndMaterial(puzzles[i], nodeType);
            }
        }
        
        private void SetDifferentTypeAndMaterialToPuzzleList(List<InteractableObject> puzzles, PuzzleNodeType currentType, 
            PuzzleNodeType changeableType)
        {
            var rand = Random.Range(0, puzzles.Count);
            for (int i = 0; i < puzzles.Count; i++)
            {
                if (i != rand)
                {
                    AddTypeAndMaterial(puzzles[i], currentType);
                }
            }
            
            AddTypeAndMaterial(puzzles[rand], changeableType);
        }

        private int[] CreateRandList(int rand)
        {
            var randList = new int[Constants.QUANTITY_OF_ELEMENTS];

            randList[0] = rand;
            for (int i = 1; i < randList.Length; i++)
            {
                randList[i] = randList[i - 1];
                randList[i]++;

                if (randList[i] == Constants.QUANTITY_OF_ELEMENTS)
                {
                    randList[i] = 0;
                }
            }

            return randList;
        }

        public List<InteractableObject> GetPuzzleNodes()
        {
            return _puzzleNodes;
        }

        #endregion
    }
}