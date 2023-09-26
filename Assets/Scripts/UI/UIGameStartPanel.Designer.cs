using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:cba32d21-23e8-4691-bf6b-e8f25de6deb4
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button StartGameBtn;
		[SerializeField]
		public UnityEngine.UI.Button CoinUpgradeBtn;
		[SerializeField]
		public RectTransform CoinUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Button CoinPercentUpgradeBtn;
		[SerializeField]
		public UnityEngine.UI.Button ExpPercentUpgradeBtn;
		[SerializeField]
		public UnityEngine.UI.Button MaxHPUpgradeBtn;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public UnityEngine.UI.Button CloseBtn;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			StartGameBtn = null;
			CoinUpgradeBtn = null;
			CoinUpgradePanel = null;
			CoinPercentUpgradeBtn = null;
			ExpPercentUpgradeBtn = null;
			MaxHPUpgradeBtn = null;
			CoinText = null;
			CloseBtn = null;
			
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
