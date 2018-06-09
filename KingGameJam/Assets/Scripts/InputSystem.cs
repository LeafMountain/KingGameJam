// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //using XInputDotNetPure;
// using Unity.Entities;
// using Unity.Transforms2D;
// using Unity.Transforms;
// using Unity.Mathematics;

// public class InputSystem : ComponentSystem
// {

// 	public struct Data
// 	{
// 		public PlayerInput Input;
// 	}

//     protected override void OnUpdate()
//     {
//         foreach (var entity in GetEntities<Data>())
// 		{
// 			float leftX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
// 			leftX += Input.GetAxis("Horizontal");
// 			float leftY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
// 			leftY += Input.GetAxis("Vertical");

// 			float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
// 			float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;			

// 			entity.Input.leftStick.x = leftX;
// 			entity.Input.leftStick.y = leftY;
// 			entity.Input.leftStick = math.normalize(entity.Input.leftStick);

// 			entity.Input.rightStick.x = rightX;
// 			entity.Input.rightStick.y = rightY;	
// 			entity.Input.rightStick = math.normalize(entity.Input.rightStick);					
// 		}
//     }
// }
