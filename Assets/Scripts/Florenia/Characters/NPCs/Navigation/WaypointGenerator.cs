using System;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Builders.Grid;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Utils;
using Florenia.Managers;
using UnityEngine;

namespace Florenia.Characters.NPCs.Navigation
{
	public class WaypointGenerator : MonoBehaviour {
		public GameObject waypointTemplate;
		public GameObject waypointParent;
		public Vector3 waypointOffset = Vector3.up;
		private bool mode2D = true;


		  // private void OnEnable() 
		  // {
			 //  DungeonManager.Instance.OnBuilt += BuildWaypoints;
			 //  DungeonManager.Instance.OnDestroyed += DestroyAllWaypoints;
		  // }
		  //
		  // private void OnDisable() 
		  // {
			 //  DungeonManager.Instance.OnBuilt -= BuildWaypoints;
			 //  DungeonManager.Instance.OnDestroyed -= DestroyAllWaypoints;
		  // }

		public void BuildWaypoints(DungeonModel model, LevelMarkerList markers)
        {
            // Destroy all existing waypoints
            DestroyAllWaypoints();

            if (model is GridDungeonModel || model is GridFlowDungeonModel)
            {
                BuildGridWaypoints(model as GridDungeonModel, markers);
            }
            else
            {
                Debug.LogWarning("Waypoint generator does not support model of type: " + model.GetType());
                return;
            }
        }

		private void BuildGridWaypoints(GridDungeonModel gridModel, LevelMarkerList markers)
        {
            // mode2D = gridModel.Config.Mode2D;

			// Destroy all existing waypoints
			DestroyAllWaypoints();

			var cellToWaypoint = new Dictionary<int, Waypoint>();

			int idCounter = 1;

            var wall2DPositions = new HashSet<IntVector>();
            if (mode2D)
            {
                foreach (var marker in markers)
                {
                    if (marker.SocketType == GridDungeonMarkerNames.Wall2D)
                    {
                        wall2DPositions.Add(marker.gridPosition);
                    }
                }
            }
           
            
			// Create a waypoint on each cell
	        foreach (var cell in gridModel.Cells)
	        {
                if (mode2D)
                {
                    if (wall2DPositions.Contains(cell.Bounds.Location))
                    {
                        // Don't want to create a waypoint on a wall tile
                        continue;
                    }
                }
	            var worldPos = MathUtils.GridToWorld(gridModel.Config.GridCellSize, cell.CenterF);
				worldPos += waypointOffset;
				if (mode2D) {
					worldPos = FlipYZ(worldPos);
				}
				var waypointObject = Instantiate(waypointTemplate, worldPos, Quaternion.identity) as GameObject;
				waypointObject.transform.parent = waypointParent.transform;

				var waypoint = waypointObject.GetComponent<Waypoint>();
				waypoint.id = idCounter++;
				cellToWaypoint.Add (cell.Id, waypoint);
			}

			// Connect adjacent waypoints
			foreach (var cellId in cellToWaypoint.Keys) {
				var waypoint = cellToWaypoint[cellId];
	            var cell = gridModel.GetCell(cellId);
				var adjacentWaypoints = new List<Waypoint>();
                var visited = new HashSet<int>();
				foreach (var adjacentCellId in cell.AdjacentCells) {
                    if (visited.Contains(GetHash(cellId, adjacentCellId))) continue;

	                var adjacentCell = gridModel.GetCell(adjacentCellId);
					// add only if there is a direct path to it (through a door or stair or open space)
	                bool directPath = HasDirectPath(gridModel, cell, adjacentCell);
					if (directPath) {
						if (cellToWaypoint.ContainsKey(adjacentCellId)) {
							var adjacentWaypoint = cellToWaypoint[adjacentCellId];
                            adjacentWaypoints.Add(adjacentWaypoint);
                            visited.Add(GetHash(cellId, adjacentCellId));
                            visited.Add(GetHash(adjacentCellId, cellId));
						}
					}
				}
				waypoint.AdjacentWaypoints = adjacentWaypoints.ToArray();
			}
		}

		private int GetHash(int a, int b)
        {
            return a << 16 | b;
        }

		private bool HasDirectPath(GridDungeonModel gridModel, Cell cellA, Cell cellB)
	    {
			bool directPath = true;
			if (cellA.CellType == CellType.Room || cellB.CellType == CellType.Room) {
	            directPath = gridModel.DoorManager.ContainsDoorBetweenCells(cellA.Id, cellB.Id);
			}
			else {
				// Check if we have a fence separating them if they have different heights
				if (cellA.Bounds.Location.y != cellB.Bounds.Location.y) {
	                directPath = gridModel.ContainsStair(cellA.Id, cellB.Id);
				}
			}
			return directPath;
		}

		private void DestroyAllWaypoints() {
			var oldWaypoints = GameObject.FindObjectsOfType<Waypoint>();
			foreach (var waypoint in oldWaypoints) {
				if (Application.isPlaying) {
					Destroy(waypoint.gameObject);
				} else {
                    DestroyImmediate(waypoint.gameObject);
				}
			}
		}

		private Vector3 FlipYZ(Vector3 v) {
			return new Vector3(v.x, v.z, v.y);
		}
	}
}
