/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AchievementPanel
	{
		[SerializeField] public UnityEngine.UI.Button AchievementItemTempleteBtn;
		[SerializeField] public UnityEngine.UI.Text UnlockText;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public RectTransform AchievementItemRoot;
		[SerializeField] public UnityEngine.UI.Button CloseBtn;

		public void Clear()
		{
			AchievementItemTempleteBtn = null;
			UnlockText = null;
			Icon = null;
			AchievementItemRoot = null;
			CloseBtn = null;
		}

		public override string ComponentName
		{
			get { return "AchievementPanel";}
		}
	}
}
