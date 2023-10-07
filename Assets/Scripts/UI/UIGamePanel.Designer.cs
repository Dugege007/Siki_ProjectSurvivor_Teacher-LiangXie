using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:51e19c61-4f1d-4f19-9568-46562ddeb4d8
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text LevelText;
		[SerializeField]
		public UnityEngine.UI.Text TimeText;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountText;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public UnlockedIconPanel UnlockedIconPanel;
		[SerializeField]
		public ExpUpgradePanel ExpUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Image ExpValue;
		[SerializeField]
		public UnityEngine.UI.Button ClearEnemyBtn;
		[SerializeField]
		public UnityEngine.UI.Button ExpUpTestBtn;
		[SerializeField]
		public UnityEngine.UI.Image ScreenColor;
		[SerializeField]
		public TreasureChestPanel TreasureChestPanel;
		[SerializeField]
		public AchievementController AchievementController;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			LevelText = null;
			TimeText = null;
			EnemyCountText = null;
			CoinText = null;
			UnlockedIconPanel = null;
			ExpUpgradePanel = null;
			ExpValue = null;
			ClearEnemyBtn = null;
			ExpUpTestBtn = null;
			ScreenColor = null;
			TreasureChestPanel = null;
			AchievementController = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
