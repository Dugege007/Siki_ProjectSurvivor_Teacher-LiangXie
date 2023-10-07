using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:3647a374-b82c-4ba2-927a-91ce57493014
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button StartGameBtn;
		[SerializeField]
		public UnityEngine.UI.Button CoinUpgradeBtn;
		[SerializeField]
		public UnityEngine.UI.Button AchievementBtn;
		[SerializeField]
		public UnityEngine.UI.Button ResetUpgradeBtn;
		[SerializeField]
		public CoinUpgradePanel CoinUpgradePanel;
		[SerializeField]
		public AchievementPanel AchievementPanel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			StartGameBtn = null;
			CoinUpgradeBtn = null;
			AchievementBtn = null;
			ResetUpgradeBtn = null;
			CoinUpgradePanel = null;
			AchievementPanel = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
