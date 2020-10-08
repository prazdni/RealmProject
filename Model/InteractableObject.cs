using UnityEngine;

namespace Realm
{
    public abstract class InteractableObject : MonoBehaviour, IExecute
    {
        public PuzzleNodeType NodeType; 
        
        public abstract void Execute();

        public abstract void GameRestart();
    }
}