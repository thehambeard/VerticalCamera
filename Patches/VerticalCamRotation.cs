using Kingmaker.UI.ServiceWindow.LocalMap;
using Kingmaker.View;
using System.Runtime.CompilerServices;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;
using Kingmaker;
using static VerticalCamera.Main;
using BagOfTricks;

namespace VerticalCamera.Patches
{
	
	public class VCam 
	{
		static KeyCode cameraTurnLeft = KeyCode.RightBracket;
		static KeyCode cameraTurnRight = KeyCode.LeftBracket;
		static float cameraTurnRate = 1f;
		static KeyCode cameraReset = KeyCode.Backslash;
		static float cameraRotation = 0.0f;
		static float defaultRotation = -400f;

		//IL_0007: ldsfld float32 VerticalCamera.Patches.Test::vrot
		[HarmonyPatch(typeof(CameraRig), "SetRotation")]
		public static class CameraRig_SetRoation_Patch
		{
			static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
			{
				var codes = new List<CodeInstruction>(instructions);
				if (true) //fix
				{
					int found = -1;
					for (int i = 0; i < codes.Count; i++)
					{
						if(codes[i].opcode == OpCodes.Ldc_R4)
                        {
							found = i;
							break;
                        }
					}
					codes[found].opcode = OpCodes.Ldsfld;
					codes[found].operand = AccessTools.Field(typeof(VCam), nameof(cameraRotation));
					
					return codes.AsEnumerable();
				}
				return codes.AsEnumerable();
			}
		}

		[HarmonyPatch(typeof(CameraRig), "TickScroll")]
		static class CameraKey_Patch
		{
			static bool Prefix(ref float ___m_ScrollSpeed)
			{
				var yRot = Game.Instance.UI.GetCameraRig().transform.rotation.y;
				if (true)
				{
					if (Input.GetKey(cameraTurnLeft))
					{
						Mod.Debug(cameraRotation);
						cameraRotation -= cameraTurnRate;
						if (cameraRotation < -180)
						{
							cameraRotation += 360;
						}
					}
					else if (Input.GetKey(cameraTurnRight))
					{
						Mod.Debug(cameraRotation);
						cameraRotation += cameraTurnRate;
						if (cameraRotation >= 180)
						{
							cameraRotation -= 360;
						}
					}
					else if (Input.GetKey(cameraReset))
					{
						Mod.Debug(cameraRotation);
						cameraRotation = 0;
					}
					else
					{
						return true;
					}
                    Game.Instance.UI.GetCameraRig().SetRotation(yRot);
					return true;
				}
				return true;
			}
		}

		//private static LocalMap lmap;
		//[HarmonyLib.HarmonyPatch(typeof(LocalMap), "OnShow")]
		//static class LocalMap_OnShow_Patch
		//{
		//	// Token: 0x06000273 RID: 627 RVA: 0x000403A6 File Offset: 0x0003E5A6
		//	private static void Prefix(LocalMap __instance)
		//	{
		//		lmap = __instance;
		//	}
		//}

		//// Token: 0x02000086 RID: 134
		//[HarmonyLib.HarmonyPatch(typeof(LocalMap), "OnHide")]
		//static class LocalMap_OnHide_Patch
		//{
		//	// Token: 0x06000274 RID: 628 RVA: 0x000403AF File Offset: 0x0003E5AF
		//	private static void Postfix()
		//	{
		//		lmap = null;
		//	}
		//}
		//[HarmonyLib.HarmonyPatch(typeof(CameraRig), "TickScroll")]
		//static class CamerRig_TickScroll_Patch
		//{
		//	static bool Prefix(ref float ___m_ScrollSpeed)
		//	{

		//	}
		//}

		//[HarmonyLib.HarmonyPatch(typeof(Foo), "Test")]
		//class Patch
		//{
		//	[HarmonyLib.HarmonyReversePatch]
		//	[MethodImpl(MethodImplOptions.NoInlining)]
		//	public static void Test(Foo instance, string s)
		//	{
		//		Console.WriteLine($"Patch.Test({instance}, {s})");
		//	}
		//}

		//[HarmonyLib.HarmonyPatch(typeof(CameraRig), "SetRotation")]
		//static class CameraRig_SetRotation_Patch
		//{
		//	static bool Prefix(ref float cameraRotation)
		//	{
		//		if (true)
		//		{
		//			cameraRotation += settings.cameraRotation;
		//			Main.rotationChanged = true;
		//			if (Main.localMap)
		//			{
		//				// If the local map is open, call the Set method to redraw things
		//				Traverse.Create(Main.localMap).Method("Set").GetValue();
		//			}
		//			return true;
		//		}
		//		return true;
		//	}
		//}
	}
}
