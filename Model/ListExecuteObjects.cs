using System.Collections.Generic;

namespace Realm
{
    public class ListExecuteObjects
    {
        #region Fields

        private List<IExecute> _executeObjects = new List<IExecute>();

        #endregion


        #region Properties

        public int Count
        {
            get => _executeObjects.Count;
        }

        public IExecute this [int index]
        {
            get => _executeObjects[index];
        }

        #endregion


        #region ClassLifeCycles

        public ListExecuteObjects(List<InteractableObject> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                AddExecuteObject(nodes[i]);
            }
        }

        #endregion


        #region Methods

        public void AddExecuteObject(IExecute interactableObject)
        {
            _executeObjects.Add(interactableObject);
        }

        #endregion
    }
}