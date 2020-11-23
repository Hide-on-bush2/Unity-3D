using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void GameOverEvent(bool win);
    public static event GameOverEvent GameOverChange;
    public delegate void StartCatchingEvent(int id, GameObject hero);
    public static event StartCatchingEvent StartCatchingChange;
    public delegate void StopCatchingEvent(int id);
    public static event StopCatchingEvent StopCatchingChange;
    public delegate void SwitchEvent(int id);
    public static event SwitchEvent PatrolSwitchChange;
    public delegate void ContinuePatrolEvent(int id);
    public static event ContinuePatrolEvent ContinuePatrolChange;
    public delegate void CollectRockEvent(int id);
    public static event CollectRockEvent CollectRockChange;

    public void HeroCaughted()
    {
        if(GameOverChange != null)
        {
            GameOverChange(false);
        }
    }

    public void CollectAllRocks()
    {
        if (GameOverChange != null)
        {
            GameOverChange(true);
        }
    }

    public void CatchingHero(int id, GameObject hero)
    {
        if (StartCatchingChange != null)
        {
            StartCatchingChange(id, hero);
        }
    }

    public void StopCatching(int id)
    {
        if (StopCatchingChange != null)
        {
            StopCatchingChange(id);
        }
    }

    public void PatrolSwicth(int id)
    {
        if(PatrolSwitchChange != null)
        {
            PatrolSwitchChange(id);
        }
    }

    public void ContinuePatrol(int id)
    {
        if(ContinuePatrolChange != null)
        {
            ContinuePatrolChange(id);
        }
    }

    public void CollectRock(int id)
    {
        if(CollectRockChange != null)
        {
            
            CollectRockChange(id);
        }
    }
}
