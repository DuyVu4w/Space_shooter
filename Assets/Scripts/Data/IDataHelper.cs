using UnityEngine;
namespace Shooter.Data;

public static interface IDataHelper
{
    public static int GetCurrentLevel();

    public static void SetCurrentLevel(int level);

}