using System;
using System.Collections;
using System.Collections.Generic;
using DungeonArchitect;
using UnityEngine;

public class ActionDungeonEventListener : DungeonEventListener
{
    public Action OnPostDungeonLayoutBuildA;
    public Action OnPostDungeonBuildA;

    /// <summary>
    /// Called after the layout is built in memory, but before the markers are emitted
    /// </summary>
    /// <param name="model">The dungeon model</param>
    public override void OnPostDungeonLayoutBuild(Dungeon dungeon, DungeonModel model)
    {
        OnPostDungeonLayoutBuildA?.Invoke();
    }

    /// <summary>
    /// Called after all the markers have been emitted for the level (but before the theming engine is run on those markers)
    /// This gives you an opportunity to modify the markers 
    /// </summary>
    /// <param name="dungeon"></param>
    /// <param name="model"></param>
    public override void OnDungeonMarkersEmitted(Dungeon dungeon, DungeonModel model, LevelMarkerList markers) { }

    /// <summary>
    /// Called before the dungeon is built
    /// </summary>
    /// <param name="model">The dungeon model</param>
    public override void OnPreDungeonBuild(Dungeon dungeon, DungeonModel model) { }

    /// <summary>
    /// Called after the dungeon is completely built
    /// </summary>
    /// <param name="model">The dungeon model</param>
    public override void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model)
    {
        OnPostDungeonBuildA?.Invoke();
    }

    /// <summary>
    /// Called after the dungeon is destroyed
    /// </summary>
    /// <param name="model">The dungeon model</param>
    public override void OnPreDungeonDestroy(Dungeon dungeon) { }

    /// <summary>
    /// Called after the dungeon is destroyed
    /// </summary>
    /// <param name="model">The dungeon model</param>
    public override void OnDungeonDestroyed(Dungeon dungeon) {}
}
