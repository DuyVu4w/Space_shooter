using UnityEngine;
namespace Shooter.Data {

    public interface IDataHelper
    {
        public int GetCurrentLevel();

        public void SetCurrentLevel(int level);

    }
}