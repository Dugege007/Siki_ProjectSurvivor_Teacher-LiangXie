/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class UnlockedIconPanel
	{
		[SerializeField] public RectTransform UnlockedIconRoot;
		[SerializeField] public UnityEngine.UI.Image UnlockedIconTemplate;
		[SerializeField] public UnityEngine.UI.Text LevelInfoText;

		public void Clear()
		{
			UnlockedIconRoot = null;
			UnlockedIconTemplate = null;
			LevelInfoText = null;
		}

		public override string ComponentName
		{
			get { return "UnlockedIconPanel";}
		}
	}
}
