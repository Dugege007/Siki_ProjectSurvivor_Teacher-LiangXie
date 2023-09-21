using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:8232b2cb-c5a9-4b1f-9a70-3b18feb06f0a
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountText;
		[SerializeField]
		public UnityEngine.UI.Text ExpText;
		[SerializeField]
		public UnityEngine.UI.Text LevelText;
		[SerializeField]
		public UnityEngine.UI.Text TimeText;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public RectTransform UpgradeRoot;
		[SerializeField]
		public UnityEngine.UI.Button UpgradeBtn;
		[SerializeField]
		public UnityEngine.UI.Button SimpleDurationUpgradeBtn;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			EnemyCountText = null;
			ExpText = null;
			LevelText = null;
			TimeText = null;
			CoinText = null;
			UpgradeRoot = null;
			UpgradeBtn = null;
			SimpleDurationUpgradeBtn = null;
			
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
