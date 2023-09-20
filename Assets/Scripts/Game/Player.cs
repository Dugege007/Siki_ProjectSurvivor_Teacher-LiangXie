using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{
		private void Start()
		{
			// QFramework 形式的打印
			"Hello QFramework!".LogInfo();
		}
	}
}
