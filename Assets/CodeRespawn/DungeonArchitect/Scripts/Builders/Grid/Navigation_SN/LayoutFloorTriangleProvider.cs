//$ Copyright 2015-22, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//
using UnityEngine;
using System.Collections.Generic;
using DungeonArchitect.Utils;
using STE = SharpNav.Geometry.TriangleEnumerable;
using SVector3 = SharpNav.Geometry.Vector3;
using Triangle3 = SharpNav.Geometry.Triangle3;
using DungeonArchitect.Builders.Grid;
using DungeonArchitect.Builders.GridFlow;

namespace DungeonArchitect.Navigation {
	public class LayoutFloorTriangleProvider : NavigationTriangleProvider {
		public Dungeon dungeon;

		// public override void AddNavTriangles(List<Triangle3> triangles) {
		// 	if (dungeon == null) {
		// 		Debug.LogWarning("LayoutFloorTriangleProvider: Dungeon is not assigned");
		// 		return;
		// 	}
  //
  // 	
		// 	var model = dungeon.ActiveModel as GridDungeonModel;
		// 	if (model == null) {
		// 		Debug.LogWarning("LayoutFloorTriangleProvider: Dungeon model is invalid. Rebuild the dungeon");
		// 		return;
		// 	}
  //
		// 	var config = model.Config;
		// 	var verts = new SVector3[4];
		// 	for (int i = 0; i < verts.Length; i++) {
		// 		verts[i] = new SVector3();
		// 	}
  //
		// 	foreach (var cell in model.Cells) {
		// 		//if (cell.CellType == CellType.Unknown) continue;
  //
		// 		var bounds = cell.Bounds;
		// 		var location = MathUtils.GridToWorld(config.GridCellSize, bounds.Location);
		// 		var size = MathUtils.GridToWorld(config.GridCellSize, bounds.Size);
  //
		// 		verts[0].Set (location.x, location.y, location.z);
		// 		verts[1].Set (location.x + size.x, location.y, location.z);
		// 		verts[2].Set (location.x + size.x, location.y, location.z + size.z);
		// 		verts[3].Set (location.x, location.y, location.z + size.z);
  //
		// 		triangles.Add (new Triangle3(
		// 			verts[0],
		// 			verts[1],
		// 			verts[2]));
  //
		// 		triangles.Add (new Triangle3(
		// 			verts[2],
		// 			verts[3],
		// 			verts[0]));
		// 	}
		// }

		public override void AddNavTriangles(List<Triangle3> triangles) 
		{
			var verts = new SVector3[4];
			for (int i = 0; i < verts.Length; i++) {
				verts[i] = new SVector3();
			}
			// Vector3 gridCellSize = Vector3.one;
			//
			// GridFlowDungeonModel gfdm = dungeon.ActiveModel as GridFlowDungeonModel;
			// int count = 0;
			// foreach (var cell in gfdm.Tilemap.Cells)
			// {
		 //        Vector3 worldPos = new Vector3(cell.TileCoord.x,cell.TileCoord.y,0);
			// 	Debug.Log($"Mapping Cell tile coords: {cell.TileCoord} to world position: {worldPos}");
			// 	
			// 	SetVerts(verts, worldPos, gridCellSize);
			// 	AddTriangles(triangles, verts);
		 //
			// 	count++;
			// 	if (count >= 30)
			// 	{
			// 		return;
			// 	}
			// }
			//
			

			


			Vector3 location = new Vector3(20,0,20);
			Vector3 size = new Vector3(15,0,15);
				
			verts[0].Set (location.x, location.y, location.z);
			verts[1].Set (location.x + size.x, location.y, location.z);
			verts[2].Set (location.x + size.x, location.y, location.z + size.z);
			verts[3].Set (location.x, location.y, location.z + size.z);
	
			triangles.Add (new Triangle3(
				verts[0],
				verts[1],
				verts[2]));
	
			triangles.Add (new Triangle3(
				verts[2],
				verts[3],
				verts[0]));
		

		}

		private static void AddTriangles(List<Triangle3> triangles, SVector3[] verts)
		{
			triangles.Add(new Triangle3(
				verts[0],
				verts[1],
				verts[2]));

			triangles.Add(new Triangle3(
				verts[2],
				verts[3],
				verts[0]));
		}

		private static void SetVerts(SVector3[] verts, Vector3 worldPos, Vector3 gridCellSize)
		{
			verts[0].Set(worldPos.x,worldPos.y,worldPos.z);
			verts[1].Set(worldPos.x + gridCellSize.x, worldPos.y, worldPos.z);
			verts[2].Set(worldPos.x + gridCellSize.x, worldPos.y + gridCellSize.y, worldPos.z );
			verts[3].Set(worldPos.x, worldPos.y + gridCellSize.y, worldPos.z );
		}
	}
}
