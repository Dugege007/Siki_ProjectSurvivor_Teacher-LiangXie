/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AchievementController
	{
		[SerializeField] public UnityEngine.UI.Image AchievementItem;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Title;
		[SerializeField] public UnityEngine.UI.Text Description;

		public void Clear()
		{
			AchievementItem = null;
			Icon = null;
			Title = null;
			Description = null;
		}

		public override string ComponentName
		{
			get { return "AchievementController";}
		}
	}
}
