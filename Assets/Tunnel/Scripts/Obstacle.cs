
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AppAdvisory.TunnelAndTwist
{
	[System.Serializable]
	public class Obstacle 
	{
		[SerializeField] List<bool> cubes;

		public Obstacle(Obstacle lastObstacle, int num)
		{
			cubes = new List<bool>(new bool[] { true, true, true, true, true, true, true, true  });

			if(lastObstacle == null)
				return;

			int totalToDisabled = 0;

			if (num < 10)
			{
				totalToDisabled = 0;
			}
			else
			{
				if (num > Constant.step9) 
				{
					totalToDisabled = Utils.RandomRange(3, 5);
				} 
				else if (num > Constant.step8) 
				{
					totalToDisabled = Utils.RandomRange(2, 5);
				}  
				else if (num > Constant.step7) 
				{
					totalToDisabled = Utils.RandomRange(1, 5);
				} 
				else if (num > Constant.step6)
				{
					totalToDisabled = Utils.RandomRange(0, 5);
				}  
				else if (num > Constant.step5)
				{
					totalToDisabled = Utils.RandomRange(2, 4);
				}  
				else if (num > Constant.step4) 
				{
					totalToDisabled = Utils.RandomRange(2, 4);
				}  
				else if (num > Constant.step3)
				{
					totalToDisabled = Utils.RandomRange(0, 4);
				}   
				else if (num > Constant.step2)
				{
					totalToDisabled = Utils.RandomRange(1, 3);
				}   
				else if (num > Constant.step1)
				{
					totalToDisabled = Utils.RandomRange(0, 3);
				}   
				else
				{
					totalToDisabled = Utils.RandomRange(0, 2);
				}
			}

			DisableSome(totalToDisabled);

			var obstacle = lastObstacle;

			var obstacleNext = this;

			for (int i = 0; i < cubes.Count; i++) 
			{
				if (obstacle.IsActivate(i))
				{
					int tempRight = i + 1;
					if (tempRight >= obstacleNext.GetSize())
						tempRight = 0;

					int tempLeft = i - 1;
					if (tempLeft < 0)
						tempLeft = obstacleNext.GetSize() - 1;

					var oFront = obstacleNext.IsActivate (i);

					var oLeft = obstacleNext.IsActivate(tempLeft);

					var oRight = obstacleNext.IsActivate(tempRight);



					if (!(oFront || oLeft || oRight)) 
					{

						int rand = Utils.RandomRange(0, 3);

						if (rand != 0 && rand != 1) {
							obstacleNext.Activate (i);
							obstacleNext.Activate (tempLeft);
						} else if (rand == 0) {
							obstacleNext.Activate (i);
							obstacleNext.Activate (tempRight);
						} else if (rand == 1) {
							obstacleNext.Activate (tempLeft);
							obstacleNext.Activate (tempRight);
						}

					}
				}
			}

		}

		void DisableSome(int num)
		{
			List<int> temp = new List<int>(new int[] { 0,1,2,3,4,5,6,7 });

			temp.Shuffle();

			for(int i = 0; i < num; i++)
			{
				cubes[temp[i]] = false;
			}

		}

		public List<bool> GetList()
		{
			return cubes;
		}

		public int GetSize()
		{
			return cubes.Count;
		}

		public void Activate(int n)
		{
			cubes [n] = true;
		}

		public void Desactivate(int n)
		{
			cubes [n] = false;
		}

		public bool IsActivate(int n)
		{
			return cubes [n];
		}
	}
}