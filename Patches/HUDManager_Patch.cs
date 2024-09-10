using HarmonyLib;
using Unity.Services.Core.Threading.Internal;

namespace CorrectDeathPenalty.Patches
{
	[HarmonyPatch(typeof(HUDManager))]
	public class HUDManager_Patch
	{
		[HarmonyPatch(nameof(HUDManager.ApplyPenalty))]
		[HarmonyPostfix]
		private static void CorrectApplyPenalty(HUDManager __instance, int playersDead, int bodiesInsured)
		{
			const int insuredPenalty = 8;
			const int deadPenalty = 20;
			const int uninsuredPenalty = deadPenalty - insuredPenalty;

			__instance.statsUIElements.penaltyAddition.text = string.Format(
				"{0} casualties: -{1}%\n({2} bodies recovered)",
				playersDead,
				playersDead * deadPenalty - bodiesInsured * uninsuredPenalty,
				bodiesInsured
			);
		}
	}
}
