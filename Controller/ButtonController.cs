using UnityEngine;

namespace Realm
{
    public class ButtonController
    {
        #region ClassLifeCycles

        public ButtonController(PuzzleNodesCreator nodesCreator)
        {
            var regularGameButton = Object.FindObjectOfType<RandomRegularGameButton>();
            var easyGameButton = Object.FindObjectOfType<RandomEasyGameButton>();

            var listOfNodes = nodesCreator.GetPuzzleNodes();
            
            for (int i = 0; i < listOfNodes.Count; i++)
            {
                regularGameButton.OActionClick += listOfNodes[i].GameRestart;
                easyGameButton.OActionClick += listOfNodes[i].GameRestart;
            }
            
            regularGameButton.OActionClick += nodesCreator.CreateRegularLevel;
            easyGameButton.OActionClick += nodesCreator.CreateEasyLevel;
        }

        #endregion
    }
}