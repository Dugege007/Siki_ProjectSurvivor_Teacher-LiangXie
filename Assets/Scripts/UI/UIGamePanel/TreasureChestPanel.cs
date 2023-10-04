/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
	public partial class TreasureChestPanel : UIElement
	{
		private void Awake()
		{
			OKBtn.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				this.Hide();
			});
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}